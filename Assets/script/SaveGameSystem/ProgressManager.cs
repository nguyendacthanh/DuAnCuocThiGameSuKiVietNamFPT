using UnityEngine;
using System.Collections.Generic;
using System.IO;

public class ProgressManager : MonoBehaviour
{
    public static ProgressManager Instance;

    private Dictionary<string, int> userData = new Dictionary<string, int>();
    public string currentUser => PlayerPrefs.GetString("currentUser", "");

    private string savePath => Application.persistentDataPath + "/" + currentUser + "_progress.txt";

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            LoadUserData();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void LoadUserData()
    {
        userData.Clear();

        if (string.IsNullOrEmpty(currentUser) || !File.Exists(savePath)) return;

        using (FileStream fs = new FileStream(savePath, FileMode.Open, FileAccess.Read))
        using (StreamReader reader = new StreamReader(fs))
        {
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                string[] parts = line.Split('=');
                if (parts.Length == 2 && int.TryParse(parts[1], out int value))
                {
                    userData[parts[0]] = value;
                }
            }
        }
    }

    public void SaveUserData()
    {
        if (string.IsNullOrEmpty(currentUser)) return;

        using (FileStream fs = new FileStream(savePath, FileMode.Create, FileAccess.Write))
        using (StreamWriter writer = new StreamWriter(fs))
        {
            foreach (var entry in userData)
            {
                writer.WriteLine($"{entry.Key}={entry.Value}");
            }
        }
    }

    public void SetValue(string key, int value)
    {
        if (string.IsNullOrEmpty(currentUser)) return;

        userData[key] = value;
        SaveUserData();
    }

    public int GetValue(string key, int defaultValue = 0)
    {
        return userData.ContainsKey(key) ? userData[key] : defaultValue;
    }

    public bool IsLoggedIn()
    {
        return !string.IsNullOrEmpty(currentUser);
    }
    
    
    
    //new add
    public void SaveGeneralStats(List<GeneralData> generals)
    {
        if (string.IsNullOrEmpty(currentUser)) return;

        using (FileStream fs = new FileStream(savePath, FileMode.Create, FileAccess.Write))
        using (StreamWriter writer = new StreamWriter(fs))
        {
            foreach (var entry in userData)
            {
                writer.WriteLine($"{entry.Key}={entry.Value}");
            }

            foreach (var g in generals)
            {
                writer.WriteLine($"{g.generalName}_atk={g.baseAtk}");
                writer.WriteLine($"{g.generalName}_def={g.baseDef}");
                writer.WriteLine($"{g.generalName}_hp={g.baseHp}");
                writer.WriteLine($"{g.generalName}_charge={g.baseCharge}");
                writer.WriteLine($"{g.generalName}_speed={g.baseSpeed}");
                writer.WriteLine($"{g.generalName}_mass={g.baseMass}");
                writer.WriteLine($"{g.generalName}_quantity={g.quantity}");
            }
        }
    }

    public void LoadGeneralStats(List<GeneralData> generals)
    {
        if (!File.Exists(savePath)) return;

        using (FileStream fs = new FileStream(savePath, FileMode.Open, FileAccess.Read))
        using (StreamReader reader = new StreamReader(fs))
        {
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                string[] parts = line.Split('=');
                if (parts.Length != 2) continue;

                string key = parts[0];
                int value = int.Parse(parts[1]);

                foreach (var g in generals)
                {
                    if (key.StartsWith(g.generalName + "_"))
                    {
                        string stat = key.Replace(g.generalName + "_", "");

                        switch (stat)
                        {
                            case "atk": g.baseAtk = value; break;
                            case "def": g.baseDef = value; break;
                            case "hp": g.baseHp = value; break;
                            case "charge": g.baseCharge = value; break;
                            case "speed": g.baseSpeed = value; break;
                            case "mass": g.baseMass = value; break;
                            case "quantity": g.quantity = value; break;
                        }
                    }
                }
            }
        }
    }


}
