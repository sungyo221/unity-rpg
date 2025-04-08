using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuickSlot : MonoBehaviour
{
    public ItemManager.ITEM_TYPE itemType;
    public Image quickSlotImage;

    private void Start()
    {
        quickSlotImage = GetComponent<Image>();
    }

    private void Update()
    {
        if(quickSlotImage.sprite.name != "HPPotion")
        {
            itemType = ItemManager.ITEM_TYPE.Null;
        }
    }
}
