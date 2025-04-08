using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Slot : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public ItemManager.ITEM_TYPE itemType;
    public Image itemImage;
    public GameObject itemToolTip;
    public TextMeshProUGUI itemValue;
    public bool slotPointer;
    public Inventory inventory;

    private void Update()
    {
        if(itemImage.sprite == null)
        {
            itemType = ItemManager.ITEM_TYPE.Null;
        }

        StartCoroutine(OpenToolTip());
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        ItemInfo itemInfo = GameManager.GetInstance().itemManager.GetItemInfo(itemType);
        if (eventData.button == PointerEventData.InputButton.Right && slotPointer)
        {
            GameManager.GetInstance().equipmentManager.AddEquipment(itemInfo);
            GameManager.GetInstance().equipmentManager.ItemEffect(itemInfo);
            GameManager.GetInstance().inventory.RemoveInventory(itemInfo);
            GameManager.GetInstance().quickSlot.AddItem(itemInfo);
            GameManager.GetInstance().guiManager.AddQuickSlotImage();
            itemImage.sprite = null;
            Color color = itemImage.color;
            color.a = 0f;
            itemImage.color = color;
            GameManager.GetInstance().guiManager.UpdateInventoryImage();
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        slotPointer = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        slotPointer = false;
    }

    public void TryOpenToolTip()
    {
        if(slotPointer)
        {
            itemToolTip.SetActive(true);
        }
        else
        {
            itemToolTip.SetActive(false);
        }
    }

    IEnumerator OpenToolTip()
    {
        if (slotPointer)
        {
            yield return new WaitForSeconds(3f);

            TryOpenToolTip();
        }
    }
}
