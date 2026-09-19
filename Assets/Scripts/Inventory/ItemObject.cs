using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Database/New Item")]
public class ItemObject : ScriptableObject
{
    public string itemName;
    public string itemDescription;
    public int itemIndex;
    public ItemType itemType;
    public int itemCost;
}
