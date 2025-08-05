
using UnityEngine;

public class UIsetting
{
    public static void TurnOn(GameObject button)
    {
        button.SetActive(true);
    }

    public static void TurnOff(GameObject button)
    {
        button.SetActive(false);
    }
}
