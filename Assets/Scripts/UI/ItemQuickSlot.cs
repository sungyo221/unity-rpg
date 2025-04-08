using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemQuickSlot : MonoBehaviour
{
    public List<ItemInfo> itemInfo;

    public void AddItem(ItemInfo item)
    {
        if(item.itemEffect == ItemInfo.ITEM_EFFECT.Heal)
        {
            itemInfo.Add(item);            
        }
    }

    public void RemoveItem(ItemInfo item)
    {
        itemInfo.Remove(item);
    }

    public void ItemEffect(ItemInfo item)
    {
        GameManager.GetInstance().soundManager.audioSources[2].Play();
        GameManager.GetInstance().player.curHealth += item.value;
    }
}
