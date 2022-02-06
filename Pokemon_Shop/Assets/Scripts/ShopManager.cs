using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    //create arrays to store the information of shopitems
    public int[,] shopItems = new int[6, 6];
    public float coins;
    public Text CoinsTXT;


    void Start()
    {
        //display available coins
        CoinsTXT.text = "Coins:" + coins.ToString();

        //initialize ID's
        shopItems[0, 0] = 1;
        shopItems[0, 1] = 2;
        shopItems[0, 2] = 3;
        shopItems[0, 3] = 4;
        shopItems[0, 4] = 5;

        //initialize price
        shopItems[1, 0] = 10;
        shopItems[1, 1] = 20;
        shopItems[1, 2] = 30;
        shopItems[1, 3] = 40;
        shopItems[1, 4] = 50;

        //initialize quantity
        shopItems[2, 0] = 0;
        shopItems[2, 1] = 0;
        shopItems[2, 2] = 0;
        shopItems[2, 3] = 0;
        shopItems[2, 4] = 0;

    }


    public void Buy()
    {
        GameObject ButtonRef = GameObject.FindGameObjectWithTag("Event").GetComponent<EventSystem>().currentSelectedGameObject;

        //check available coins
        if (coins >= shopItems[1, ButtonRef.GetComponent<ButtonInfo>().ItemID])
        {
            coins -= shopItems[1, ButtonRef.GetComponent<ButtonInfo>().ItemID];
            shopItems[2, ButtonRef.GetComponent<ButtonInfo>().ItemID]++;
            CoinsTXT.text = "Coins:" + coins.ToString();
            ButtonRef.GetComponent<ButtonInfo>().QuantityTxt.text = shopItems[2, ButtonRef.GetComponent<ButtonInfo>().ItemID].ToString();
           
        }


    }
}