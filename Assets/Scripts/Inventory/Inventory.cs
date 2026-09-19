using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory instance;

    [SerializeField] public Dictionary<ItemType, List<ItemObject>> items = new Dictionary<ItemType, List<ItemObject>>();

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            if (instance != this)
            {
                Destroy(gameObject);
            }
        }

        DontDestroyOnLoad(gameObject);
    }

    public void AddItem(ItemObject newItem)
    {
        if (!items.ContainsKey(ItemType.All))
        {
            items.Add(ItemType.All, new List<ItemObject>());
        }

        if (!items.ContainsKey(newItem.itemType))
        {
            items.Add(newItem.itemType, new List<ItemObject>());
        }

        items[ItemType.All].Add(newItem);
        items[ItemType.All].Sort(SortItems);

        items[newItem.itemType].Add(newItem);
        items[newItem.itemType].Sort(SortItems);
    }

    public void RemoveItem(ItemObject item)
    {
        if (!items.ContainsKey(item.itemType)) return;

        if (!items[item.itemType].Contains(item)) return;

        items[item.itemType].Remove(item);
    }

    public int SortItems(ItemObject a, ItemObject b)
    {
        if (a.itemIndex < b.itemIndex || (int)a.itemType < (int)b.itemType)
        {
            return -1;
        }

        if (a.itemIndex > b.itemIndex || (int)a.itemType > (int)b.itemType)
        {
            return 1;
        }

        return 0;
    }
}
