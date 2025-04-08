using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentManager : MonoBehaviour
{
    public List<ItemInfo> itemInfo;
    public AttackBox attackBox;
    public Equiment[] equiment;   
    public Player player;
    Equiment.SLOT_TYPE slotType;
    ItemInfo.ITEM_EFFECT itemEffect;
    ItemManager itemManager;

    public void AddEquipment(ItemInfo item)
    {
        if (item.itemEffect != ItemInfo.ITEM_EFFECT.Heal && item.itemEffect != ItemInfo.ITEM_EFFECT.Quest)
        {
            itemInfo.Add(item);
        }

        for (int i = 0; i < equiment.Length; i++)
        {
            equiment[i].AddEquipmentSlotImage(item);
        }        
    }

    public void RemoveEquipment(ItemInfo item)
    {
        GameManager.GetInstance().inventory.AddInventory(item);
        GameManager.GetInstance().guiManager.AddInventoryImage();
        player.defence -= item.value;
        itemInfo.Remove(item);        
    }

    public void ItemEffect(ItemInfo item)
    {
        if (item.itemEffect == ItemInfo.ITEM_EFFECT.Weapon)
        {
            player.damage += item.value;
        }
        if (item.itemEffect == ItemInfo.ITEM_EFFECT.Helmet ||
                    item.itemEffect == ItemInfo.ITEM_EFFECT.Armor ||
                    item.itemEffect == ItemInfo.ITEM_EFFECT.Pant ||
                    item.itemEffect == ItemInfo.ITEM_EFFECT.Shoes ||
                    item.itemEffect == ItemInfo.ITEM_EFFECT.Shield)
        {
            player.defence += item.value;
        }
    }
}
