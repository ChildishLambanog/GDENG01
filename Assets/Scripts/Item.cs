using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "Item", menuName = "Item/baseItem")]

public class Item : ScriptableObject
{
    new public string name = "Default Item";
    public Sprite icon = null;
    public string itemDescription = "Used for crafting";

    public virtual string GetItemDescription()
    {
        return itemDescription;
    }

}
