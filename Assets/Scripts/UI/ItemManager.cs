using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class ItemInfo
{
    public enum ITEM_EFFECT {Null, Heal, Helmet, Armor, Pant, Shoes, Weapon, Shield, Quest, Max}
    public ITEM_EFFECT itemEffect;
    public string name;
    public string content;
    public Sprite itemImg;
    public int value;
    public GameObject itemPrefab;
    public int price;

    public ItemInfo(ITEM_EFFECT _itemEffect, string _name, string _content, string _itemImg, int _value, string _objectName, int _price)
    {
        itemEffect = _itemEffect;
        name = _name;
        content = _content;
        itemImg = Resources.Load<Sprite>("ItemImage/" + _itemImg);
        value = _value;
        itemPrefab = Resources.Load("Prefabs/Item/" + _objectName) as GameObject;
        price = _price;
    }
}

public class ItemManager : MonoBehaviour
{
    [SerializeField]
    public enum ITEM_TYPE {Null, Heal, Helmet, Armor, Pant, Shoes, Weapon, Shield, Quest}
    public ITEM_TYPE itemType;
    public List<ItemInfo> listItemInfo;    

    public void Initialize()
    {
        listItemInfo = new List<ItemInfo>((int)ItemInfo.ITEM_EFFECT.Max);
        listItemInfo.Add(new ItemInfo(ItemInfo.ITEM_EFFECT.Null, "", "", "", 0, "", 0));
        listItemInfo.Add(new ItemInfo(ItemInfo.ITEM_EFFECT.Heal, "HP����", "HP�� ȸ���Ѵ�.", "HPPotion", 30, "HPPotion", 300));
        listItemInfo.Add(new ItemInfo(ItemInfo.ITEM_EFFECT.Helmet, "����", "������ �÷��ش�.", "Helmet", 2, "Helmet", 500));
        listItemInfo.Add(new ItemInfo(ItemInfo.ITEM_EFFECT.Armor, "����", "������ �÷��ش�.", "Armor", 5, "Armor", 1000));
        listItemInfo.Add(new ItemInfo(ItemInfo.ITEM_EFFECT.Pant, "����", "������ �÷��ش�.", "Pant", 4, "Pant", 800));
        listItemInfo.Add(new ItemInfo(ItemInfo.ITEM_EFFECT.Shoes, "�Ź�", "������ �÷��ش�.", "Shoes", 3, "Shoes", 600));
        listItemInfo.Add(new ItemInfo(ItemInfo.ITEM_EFFECT.Weapon, "����", "���ݷ��� �÷��ش�.", "Weapon", 30, "Weapon", 1500));
        listItemInfo.Add(new ItemInfo(ItemInfo.ITEM_EFFECT.Shield, "����", "������ �÷��ش�.", "Shield", 10, "Shield", 2000));
        listItemInfo.Add(new ItemInfo(ItemInfo.ITEM_EFFECT.Quest, "����Ʈ ������", "����Ʈ Ŭ��� ���� ������.", "Quest", 1, "Quest", 0));
    }

    private void Start()
    {
        Initialize();
    }

    public ItemInfo GetItemInfo(int idx)
    {
        return listItemInfo[idx];
    }

    public ItemInfo GetItemInfo(ITEM_TYPE type)
    {
        return listItemInfo[(int)type];
    }
}
