using System.Collections.Generic;
using UnityEngine;

public class ClassCity : MonoBehaviour
{
    public string cityName;
    public int cityHp;
    public bool isPlayerCity;

    public BuildingClass[] buildingSlots = new BuildingClass[3];

    public void BuildAtSlot(int index, BuildingClass prefab)
    {
        if (index < 0 || index >= buildingSlots.Length) return;
        if (buildingSlots[index] != null) return;

        BuildingClass b = Instantiate(prefab, transform);
        b.BuildingAbility(this);
        buildingSlots[index] = b;
    }

    public void RemoveAtSlot(int index)
    {
        if (index < 0 || index >= buildingSlots.Length) return;
        if (buildingSlots[index] == null) return;

        Destroy(buildingSlots[index].gameObject);
        buildingSlots[index] = null;
    }
}
