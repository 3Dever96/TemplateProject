using UnityEngine;

public class TreasureChest : InteractionObject
{
    public bool isOpen;
    public ItemObject item;

    public override void OnInteract()
    {
        if (!isOpen)
        {
            isOpen = true;
            Inventory.instance.AddItem(item);
            print("Found " + item.itemName);
        }
        else
        {
            print("It's empty...  Someone must have found this chest already.  Oh, yeah!  It was me.");
        }
    }

    protected override void Update()
    {
        
    }
}
