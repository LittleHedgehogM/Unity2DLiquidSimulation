using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemDatabase", menuName = "Game/Item Database", order = 1)]
public class ItemDatabase : ScriptableObject
{
    [SerializeField] public List<ItemDefinition> items = new List<ItemDefinition>();

    //private Dictionary<string, ItemDefinition> _lookup;

}