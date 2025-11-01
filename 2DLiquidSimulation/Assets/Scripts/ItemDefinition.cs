using UnityEngine;

[CreateAssetMenu(fileName = "Item_", menuName = "Game/Item Definition", order = 0)]
public class ItemDefinition : ScriptableObject
{

    [Header("基本信息")]
    [SerializeField] public string Id;     // 显示名称
    [SerializeField] public Sprite icon;            // 图标
    [SerializeField][TextArea] public string description;

}