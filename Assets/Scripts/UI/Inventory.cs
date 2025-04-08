using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<ItemInfo> itemInfo;

    public void AddInventory(ItemInfo item)
    {
        itemInfo.Add(item);
    }
    public void RemoveInventory(ItemInfo item)
    {
        itemInfo.Remove(item);
    }
}
