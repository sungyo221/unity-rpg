using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GUIManager : MonoBehaviour
{
    public Slot[] slot;
    public SlotToolTip[] slotToolTip;
    public QuickSlot[] quickSlots;

    public GameObject EquipmentUI;
    public GameObject inventoryBag;
    public GameObject storeUi;
    public GameObject storeSlot;
    public GameObject spawnStoreSlot;
    public GameObject skillUI;
    public GameObject escUI;

    public bool inventoryOpen = false;
    public bool storeOpen = false;
    public bool skillUIOpen = false;
    public bool escUIOpen = false;

    public Inventory inventory;
    public ItemQuickSlot itemQuickSlot;    
    public Player player;
    public BlackSmithShop store;
    public StoreItemSlot[] storeItemSlot;

    public TextMeshProUGUI[] playerStatus;
    public TextMeshProUGUI moneyCount;
    public TextMeshProUGUI level;     

    private void Start()
    {
        Debug.Log(gameObject.name);
        SpwanStoreSlot();
        level.text = "Lv " + player.level; 
    }

    private void Update()
    {
        TryOpenInventory();
        QuickSlotKeyDown();
        PlayerStatusText();
        AddStoreSlot();
        ESCButton();
        if(Input.GetKeyDown(KeyCode.F))
        {
            store.PlayerDetectedStore();
        }
        AddToolTip();
        TryOpenSkillUI();
    }

    void TryOpenInventory()
    {
        if(Input.GetKeyDown(KeyCode.I))
        {
            if (!inventoryOpen)
            {
                EquipmentUI.SetActive(true);
                inventoryBag.SetActive(true);
                inventoryOpen = true;
            }
            else
            {
                EquipmentUI.SetActive(false);
                inventoryBag.SetActive(false);
                inventoryOpen = false;
            }
        }        
    }

    public void TryOpenStore()
    {
        if (!storeOpen)
        {
            inventoryBag.SetActive(true);
            storeUi.SetActive(true);
            storeOpen = true;
        }
        else
        {
            inventoryBag.SetActive(false);
            storeUi.SetActive(false);
            storeOpen = false;
        }        
    }

    public void TryOpenSkillUI()
    {
        if(Input.GetKeyDown(KeyCode.K))
        {
            if(!skillUIOpen)
            {
                skillUI.SetActive(true);
                skillUIOpen = true;
            }
            else
            {
                skillUI.SetActive(false);
                skillUIOpen = false;
            }
        }
    }

    void PlayerStatusText()
    {
        if(inventoryOpen)
        {
            playerStatus[0].text = player.maxHealth.ToString();
            playerStatus[1].text = player.maxStamina.ToString();
            playerStatus[2].text = player.damage.ToString();
            playerStatus[3].text = player.defence.ToString();
            playerStatus[4].text = player.crit.ToString();
            moneyCount.text = ("소지금 : " + player.money.ToString());
        }
        if(storeOpen)
        {
            moneyCount.text = ("소지금 : " + player.money.ToString());
        }
    }

    public void AddInventoryImage()
    {
        for (int i = 0; i < slot.Length; i++)
        {
            if (inventory.itemInfo.Count > i)
            {
                slot[i].itemImage.sprite = inventory.itemInfo[i].itemImg;
                slot[i].itemType = (ItemManager.ITEM_TYPE)inventory.itemInfo[i].itemEffect;
                slot[i].itemImage.color = Color.white;
            }
        }        
    }
    
    public void RemoveInventoryImage()
    {
        for (int i = 0; i < slot.Length; i++)
        {           
            slot[i].itemImage.sprite = null;
            slot[i].itemImage.color = Color.clear;            
        }
        
    }

    public void UpdateInventoryImage()
    {
        RemoveInventoryImage();
        AddInventoryImage();
    }

    public void AddQuickSlotImage()
    {
        for(int i = 0; i < quickSlots.Length; i++)
        {
            if(itemQuickSlot.itemInfo.Count > i)
            {
                quickSlots[i].quickSlotImage.sprite = itemQuickSlot.itemInfo[i].itemImg;
                quickSlots[i].itemType = (ItemManager.ITEM_TYPE)itemQuickSlot.itemInfo[i].itemEffect;
            }            
        }
    }

    public void RemoveQuickSlotImage()
    {
        for(int i = 0; i < quickSlots.Length; i++)
        {
            string panelName = "Frame_1_a";
            quickSlots[i].quickSlotImage.sprite = Resources.Load<Sprite>("ItemImage/Panel/" + panelName);            
        }
    }

    public void QuickSlotKeyDown()
    {        
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            ItemInfo itemInfo = GameManager.GetInstance().itemManager.GetItemInfo(quickSlots[0].itemType);
            RemoveQuickSlotImage();
            itemQuickSlot.ItemEffect(itemInfo);
            itemQuickSlot.RemoveItem(itemInfo);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            ItemInfo itemInfo = GameManager.GetInstance().itemManager.GetItemInfo(quickSlots[1].itemType);
            RemoveQuickSlotImage();
            itemQuickSlot.ItemEffect(itemInfo);
            itemQuickSlot.RemoveItem(itemInfo);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            ItemInfo itemInfo = GameManager.GetInstance().itemManager.GetItemInfo(quickSlots[2].itemType);
            RemoveQuickSlotImage();
            itemQuickSlot.ItemEffect(itemInfo);
            itemQuickSlot.RemoveItem(itemInfo);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            ItemInfo itemInfo = GameManager.GetInstance().itemManager.GetItemInfo(quickSlots[3].itemType);
            RemoveQuickSlotImage();
            itemQuickSlot.ItemEffect(itemInfo);
            itemQuickSlot.RemoveItem(itemInfo);
        }
        UpdateQuickSlotImage();
    }

    public void UpdateQuickSlotImage()
    {
        RemoveQuickSlotImage();
        AddQuickSlotImage();
    }

    void SpwanStoreSlot()
    {
        storeItemSlot = new StoreItemSlot[7];
        for(int i = 0; i < storeItemSlot.Length; i++)
        {
            GameObject slotObject = Instantiate(storeSlot, spawnStoreSlot.transform);
            storeItemSlot[i] = slotObject.GetComponent<StoreItemSlot>();
        }
    }

    public void AddStoreSlot()
    {
        for (int i = 0; i < storeItemSlot.Length; i++)
        {
            if (store.itemInfo.Count > i)
            {
                storeItemSlot[i].shopItemImage.sprite = store.itemInfo[i].itemImg;
                storeItemSlot[i].itemNameText.text = store.itemInfo[i].name;
                storeItemSlot[i].itemPrice.text = ("" + store.itemInfo[i].price);
                storeItemSlot[i].ItemContentText.text = store.itemInfo[i].content;
                storeItemSlot[i].itemType = (ItemManager.ITEM_TYPE)store.itemInfo[i].itemEffect;
            }
        }
    }

    public void AddToolTip()
    {
        for (int i = 0; i < slotToolTip.Length; i++)
        {
            if (inventory.itemInfo.Count > i)
            {
                slotToolTip[i].itemName.text = inventory.itemInfo[i].name;
                slotToolTip[i].itemPrice.text = "가격 : " + inventory.itemInfo[i].price;
                slotToolTip[i].itemType.text = inventory.itemInfo[i].itemEffect.ToString();
                slotToolTip[i].itemContent.text = inventory.itemInfo[i].content;
            }
        }
    }

    public void ESCButton()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(!escUIOpen)
            {
                escUI.SetActive(true);
                escUIOpen = true;
            }
            else
            {
                escUI.SetActive(false);
                escUIOpen = false;
            }
        }
    }
}
