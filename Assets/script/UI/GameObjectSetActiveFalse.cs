using UnityEngine;
using UnityEngine.UI;

public class GameObjectSetActiveFalse : MonoBehaviour
{
    public GameObject targetFalse;
    

    public void setActiveFalse()
    {
        targetFalse.SetActive(false);
    }
}
