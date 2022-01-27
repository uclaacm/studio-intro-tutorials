using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/InventoryItem", order = 1)]
public class InventoryItem : ScriptableObject
{
    [SerializeField] string idName;
    [SerializeField] string displayName;
    [SerializeField] [TextArea] string tooltip;
    [SerializeField] Sprite icon;
    

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
}
