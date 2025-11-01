using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance { get; private set; }
    [SerializeField] private ItemDatabase database; 

    private Dictionary<string, ItemDefinition> ItemDictionary = new Dictionary<string, ItemDefinition>();
    private Dictionary<string, int> ItemAmounts = new Dictionary<string, int>();

    private void Awake()
    {
        if (Instance != null && Instance != this) 
        { 
            Destroy(gameObject); return; 
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        BuildDictionary();
    }

    private void BuildDictionary()
    {
        foreach (var item in database.items)
        {
            if (item == null || string.IsNullOrEmpty(item.Id)) continue;
            if (ItemDictionary.ContainsKey(item.Id)) 
            {
               Debug.LogWarning($"重复的物品ID: {item.Id}");
            }
            else 
            {
                ItemDictionary[item.Id] = item;
                ItemAmounts[item.Id] = 0;
            }
        }
    }

    public ItemDefinition GetDefinitionByID(string id)
    {
        if (ItemDictionary == null) BuildDictionary();
        return ItemDictionary.TryGetValue(id, out var def) ? def : null;
    }

    public bool AddItemById(string id, int amount = 1)
    {
        if (ItemAmounts.ContainsKey(id))
        {
            ItemAmounts[id] = ItemAmounts[id] + amount;
        }

        return ItemAmounts.ContainsKey(id);
    }

    public bool RemoveItemById(string id, int amount = 1)
    {
        if (ItemAmounts.ContainsKey(id))
        {
            ItemAmounts[id] = Mathf.Max(ItemAmounts[id] - amount, 0);
        }

        return ItemAmounts.ContainsKey(id);
    }

}
