using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Equiment : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public enum SLOT_TYPE { Helmet, Armor, Pant, Shoes, Weapon, Shield}
    public SLOT_TYPE slotType;
    public GameObject equipmentItemObj;
    public GameObject startClothesObj;
    public ItemManager.ITEM_TYPE itemType;
    public Image equipImage;
    public bool slotPointer = false;
    
    string equipName;

    private void Start()
    {
        if(equipImage == null)
        {            
            equipImage = GetComponentInChildren<Image>();
        }
    }

    private void Update()
    {
        if(equipImage.sprite == null)
        {
            Color color = equipImage.color;            
            if (color.a == 1)
            {                
                color.a = 0.1f;
            }
            equipImage.color = color;
            NullEquipmentSlotImage();
        }
    }

    public void NullEquipmentSlotImage()
    {
        switch (slotType)
        {
            case SLOT_TYPE.Helmet:
                equipName = "HelmetIcon";
                equipmentItemObj.SetActive(false);
                Debug.Log("ddd");
                break;
            case SLOT_TYPE.Armor:
                equipName = "ArmorIcon";
                equipmentItemObj.SetActive(false);
                startClothesObj.SetActive(true);
                break;
            case SLOT_TYPE.Pant:
                equipName = "PantIcon";
                equipmentItemObj.SetActive(false);
                startClothesObj.SetActive(true);
                break;
            case SLOT_TYPE.Shoes:
                equipName = "ShoseIcon";
                equipmentItemObj.SetActive(false);
                startClothesObj.SetActive(true);
                break;
            case SLOT_TYPE.Weapon:
                equipName = "WeaponIcon";
                equipmentItemObj.SetActive(false);
                break;
            case SLOT_TYPE.Shield:
                equipName = "ShieldIcon";
                equipmentItemObj.SetActive(false);
                break;
        }
        equipImage.sprite = Resources.Load<Sprite>("ItemImage/EquipmentPanel/" + equipName);        
    }

    public void AddEquipmentSlotImage(ItemInfo itemInfo)
    {
        switch (slotType)
        {
            case SLOT_TYPE.Weapon:
                if (itemInfo.itemEffect == ItemInfo.ITEM_EFFECT.Weapon)
                {
                    GameManager.GetInstance().soundManager.audioSources[3].Play();
                    itemType = (ItemManager.ITEM_TYPE)itemInfo.itemEffect;
                    equipmentItemObj.SetActive(true);                    
                    equipImage.sprite = itemInfo.itemImg;
                    equipImage.color = Color.white;
                }
                break;
            case SLOT_TYPE.Shield:
                if (itemInfo.itemEffect == ItemInfo.ITEM_EFFECT.Shield)
                {
                    GameManager.GetInstance().soundManager.audioSources[3].Play();
                    itemType = (ItemManager.ITEM_TYPE)itemInfo.itemEffect;
                    equipmentItemObj.SetActive(true);
                    equipImage.sprite = itemInfo.itemImg;
                    equipImage.color = Color.white;
                }
                break;
            case SLOT_TYPE.Helmet:
                if (itemInfo.itemEffect == ItemInfo.ITEM_EFFECT.Helmet)
                {
                    GameManager.GetInstance().soundManager.audioSources[3].Play();
                    itemType = (ItemManager.ITEM_TYPE)itemInfo.itemEffect;
                    equipmentItemObj.SetActive(true);
                    equipImage.sprite = itemInfo.itemImg;
                    equipImage.color = Color.white;
                }
                break;
            case SLOT_TYPE.Armor:
                if (itemInfo.itemEffect == ItemInfo.ITEM_EFFECT.Armor)
                {
                    GameManager.GetInstance().soundManager.audioSources[3].Play();
                    itemType = (ItemManager.ITEM_TYPE)itemInfo.itemEffect;
                    equipmentItemObj.SetActive(true);
                    startClothesObj.SetActive(false);
                    equipImage.sprite = itemInfo.itemImg;
                    equipImage.color = Color.white;
                }
                break;
            case SLOT_TYPE.Pant:
                if (itemInfo.itemEffect == ItemInfo.ITEM_EFFECT.Pant)
                {
                    GameManager.GetInstance().soundManager.audioSources[3].Play();
                    itemType = (ItemManager.ITEM_TYPE)itemInfo.itemEffect;
                    equipmentItemObj.SetActive(true);
                    startClothesObj.SetActive(false);
                    equipImage.sprite = itemInfo.itemImg;
                    equipImage.color = Color.white;
                }
                break;
            case SLOT_TYPE.Shoes:
                if (itemInfo.itemEffect == ItemInfo.ITEM_EFFECT.Shoes)
                {
                    GameManager.GetInstance().soundManager.audioSources[3].Play();
                    itemType = (ItemManager.ITEM_TYPE)itemInfo.itemEffect;
                    equipmentItemObj.SetActive(true);
                    startClothesObj.SetActive(false);
                    equipImage.sprite = itemInfo.itemImg;
                    equipImage.color = Color.white;
                }
                break;
        }
    }

    public void RemoveEquipmentSlotImage()
    {
        GameManager.GetInstance().soundManager.audioSources[3].Play();
        itemType = ItemManager.ITEM_TYPE.Null;
        equipImage.sprite = null;        
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        ItemInfo itemInfo = GameManager.GetInstance().itemManager.GetItemInfo(itemType);
        if(eventData.button == PointerEventData.InputButton.Right && slotPointer)
        {
            RemoveEquipmentSlotImage();            
            GameManager.GetInstance().equipmentManager.RemoveEquipment(itemInfo);
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
}
