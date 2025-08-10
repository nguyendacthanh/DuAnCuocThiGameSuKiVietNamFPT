using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public Button[] levelButtons;

    void Start()
    {
        // Lấy giá trị unlockedLevel từ ProgressManager
        int unlockedLevel = ProgressManager.Instance.GetValue("unlockedLevel", 1);

        // Kiểm tra và hiển thị các button màn chơi dựa trên unlockedLevel
        for (int i = 0; i < levelButtons.Length; i++)
        {
            levelButtons[i].interactable = (i < unlockedLevel);  // Button cho màn chơi sẽ được mở nếu level đã mở
        }
    }

    public void OnLevelButtonClicked(int level)
    {
        int unlockedLevel = ProgressManager.Instance.GetValue("unlockedLevel", 1);

        if (level <= unlockedLevel)
        {
            string sceneName = "Level" + level;
            UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
        }
        else
        {
            Debug.Log("Màn chơi này chưa mở!");
        }
    }
}