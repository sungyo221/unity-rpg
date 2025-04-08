using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StoreItemSlot : ItemManager
{    
    Player player;
    public Image shopItemImage;
    public TextMeshProUGUI itemNameText;
    public TextMeshProUGUI itemPrice;
    public TextMeshProUGUI ItemContentText;

    private void Start()
    {
        Debug.Log($"StoreItemSlot:{gameObject.name}");        
    }

    public void BuyItem()
    {
        ItemInfo itemInfo = GetItemInfo(itemType);
        player = GameManager.GetInstance().player;
        if(player.money >= itemInfo.price)
        {
            GameManager.GetInstance().inventory.AddInventory(itemInfo);
            GameManager.GetInstance().guiManager.AddInventoryImage();
            player.money -= itemInfo.price;
        }
        else if(player.money < itemInfo.price)
        {
            Debug.Log(itemInfo.price);
            Debug.Log($"{player.gameObject.name}:{player.money}");
            Debug.Log("µ·ÀÌ ºÎÁ·");
        }
    }
}
