using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AccountManager : MonoBehaviour
{
    public TMP_InputField usernameInput;
    public TMP_InputField passwordInput;
    public TMP_Text messageText;

    private string accountFilePath;
    private string currentUser = "";

    void Start()
    {
        // Đường dẫn lưu file tài khoản
        accountFilePath = Application.persistentDataPath + "/accounts.txt";

        // Kiểm tra nếu file không tồn tại thì tạo
        if (!File.Exists(accountFilePath))
        {
            using (FileStream fs = File.Create(accountFilePath)) { }
        }
    }

    public void OnRegister()
    {
        string username = usernameInput.text.Trim();
        string password = passwordInput.text.Trim();

        // Kiểm tra dữ liệu hợp lệ
        if (username == "" || password == "")
        {
            messageText.text = "Tài khoản / mật khẩu không hợp lệ!";
            return;
        }

        // Kiểm tra tài khoản đã tồn tại chưa
        if (IsAccountExist(username))
        {
            messageText.text = "Tài khoản đã tồn tại!";
            return;
        }

        // Lưu tài khoản vào file accounts.txt
        using (FileStream fs = new FileStream(accountFilePath, FileMode.Append, FileAccess.Write))
        using (StreamWriter writer = new StreamWriter(fs))
        {
            writer.WriteLine(username + "|" + password);  // Lưu tài khoản và mật khẩu
        }

        // Tạo file progress.txt chứa thông tin tiến độ người chơi
        string savePath = GetUserSavePath(username);
        using (FileStream fs = new FileStream(savePath, FileMode.Create, FileAccess.Write))
        using (StreamWriter writer = new StreamWriter(fs))
        {
            // Lưu tiến độ người chơi và tướng mặc định
            writer.WriteLine("unlockedLevel=1");  // Màn đầu tiên
            writer.WriteLine("cost=0");  // Chi phí ban đầu

            // Lưu thông tin tướng mặc định
            writer.WriteLine("general1_atk=10");
            writer.WriteLine("general1_def=5");
            writer.WriteLine("general1_hp=100");
            writer.WriteLine("general1_quantity=1");

            writer.WriteLine("general2_atk=12");
            writer.WriteLine("general2_def=6");
            writer.WriteLine("general2_hp=120");
            writer.WriteLine("general2_quantity=1");
        }

        messageText.text = "Đăng ký thành công!";
    }

    // Kiểm tra tài khoản đã tồn tại hay chưa
    private bool IsAccountExist(string username)
    {
        using (FileStream fs = new FileStream(accountFilePath, FileMode.Open, FileAccess.Read))
        using (StreamReader reader = new StreamReader(fs))
        {
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                if (line.StartsWith(username + "|"))
                    return true;
            }
        }
        return false;
    }
    public void OnLogin()
    {
        string username = usernameInput.text.Trim();
        string password = passwordInput.text.Trim();

        using (FileStream fs = new FileStream(accountFilePath, FileMode.Open, FileAccess.Read))
        using (StreamReader reader = new StreamReader(fs))
        {
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                string[] parts = line.Split('|');
                if (parts.Length == 2 && parts[0] == username && parts[1] == password)
                {
                    currentUser = username;
                    PlayerPrefs.SetString("currentUser", currentUser);

                    // Load dữ liệu người dùng và tướng
                    ProgressManager.Instance.LoadUserData();
                    ProgressManager.Instance.LoadGeneralStats(GeneralManager.Instance.generals);

                    messageText.text = "Đăng nhập thành công!";
                    return;
                }
            }
        }

        messageText.text = "Sai tài khoản hoặc mật khẩu!";
    }


    // Đường dẫn file lưu tiến độ của mỗi tài khoản
    private string GetUserSavePath(string username)
    {
        return Application.persistentDataPath + "/" + username + "_progress.txt";
    }
    
}

