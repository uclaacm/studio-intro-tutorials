
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

    //vairables for shop
    [SerializeField] bool isPurchased = false;
    [SerializeField] int price = 0;

    public enum Actions
    {
        use,
        drop
    }
    [SerializeField] List<Actions> availableActions;
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

    public List<Actions> getAvailableActions()
    {
        return availableActions;
    }

    public int getHealPower()
    {
        return healPower;
    }

    //functions for shop
    public bool getPurchased()
    {
        return isPurchased;
    }

    public void purchase()
    {
        isPurchased = true;
    }

    public void reset()
    {
        isPurchased = false;
    }

    public int getPrice()
    {
        return price;
    }
}

