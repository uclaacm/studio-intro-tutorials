using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonInfo : MonoBehaviour
{

    public int ItemID;
    public Text PirceTxt;
    public Text QuantityTxt;
    public GameObject ShopManager;

    void Update()
    {
        PirceTxt.text = "Price: $" + ShopManager.GetComponent<ShopManager>().shopItems[1, ItemID].ToString();
        QuantityTxt.text = "Quantity: " + ShopManager.GetComponent<ShopManager>().shopItems[2, ItemID].ToString();
    }
}
