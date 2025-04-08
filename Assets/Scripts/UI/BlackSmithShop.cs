using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BlackSmithShop : MonoBehaviour
{
    public List<ItemInfo> itemInfo;
    public Player player;
    public ItemManager itemManager;
    public float senseRange = 3f;

    private void Start()
    {
        AddItem(itemManager.GetItemInfo(1));
        AddItem(itemManager.GetItemInfo(2));
        AddItem(itemManager.GetItemInfo(3));
        AddItem(itemManager.GetItemInfo(4));
        AddItem(itemManager.GetItemInfo(5));
        AddItem(itemManager.GetItemInfo(6));
        AddItem(itemManager.GetItemInfo(7));        
    }    

    public void PlayerDetectedStore()
    {
        float distance = Vector3.Distance(transform.position, player.transform.position);
        if (senseRange >= distance)
        {            
            GameManager.GetInstance().guiManager.TryOpenStore();
        }
    }

    public void AddItem(ItemInfo item)
    {
        itemInfo.Add(item);
    }
}
