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
    [SerializeField] Inventory.Category category;
    

    // PUBLIC 
    public string GetIDName()
    {
        return idName;
    }

    public Sprite GetIcon()
    {
        return icon;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    
}
