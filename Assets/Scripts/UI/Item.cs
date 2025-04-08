using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public ItemManager.ITEM_TYPE itemType;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            ItemInfo itemInfo = GameManager.GetInstance().itemManager.GetItemInfo(itemType);
            GameManager.GetInstance().inventory.AddInventory(itemInfo);
            GameManager.GetInstance().guiManager.AddInventoryImage();
            Destroy(this.gameObject);
        }
    }
}
