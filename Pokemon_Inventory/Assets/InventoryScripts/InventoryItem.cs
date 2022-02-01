using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO: There's a way to easily create ScriptableObjects by writing something here...
public class InventoryItem : ScriptableObject
{
    [SerializeField] string idName;
    [SerializeField] string displayName;
    [SerializeField] [TextArea] string tooltip;
    [SerializeField] Sprite icon;
    [SerializeField] int healPower;
    

    // PUBLIC 
    public string GetIDName()
    {
        return idName;
    }

    public string GetDisplayName()
    {
        return displayName;
    }

    public Sprite GetIcon()
    {
        return icon;
    }
    
    public string GetToolTip()
    {
        return tooltip;
    }

    public int getHealPower()
    {
        return healPower;
    }
}
