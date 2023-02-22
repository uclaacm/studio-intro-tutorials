using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
//using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;  //(NEW)IMPORTANT TO ADD THIS!!
//using static System.Net.Mime.MediaTypeNames;


public class CurrencySystem : MonoBehaviour
{
    
    public static int money; //how much money we have/wallet
    public int startingMoney; //what the name suggests
    public Text moneyText; //(NEW) for displaying the amount of money we have

    // Start is called before the first frame update
    void Start()
    {
        //fill our wallet with the amount of money we start with
        money = startingMoney;
        moneyText.text = money.ToString(); //(NEW) for displaying the amount of money we have
    }
        // Update is called once per frame (NEW)
        void Update()
    {
        moneyText.text = "Wallet: $" + money; //(NEW) for displaying the amount of money we have
    }

    public void Gain(int amt){
        //add money to the wallet
        money += amt;
        Debug.Log(money);
    }

    public bool Use(int amt){
        //if we don't have enough money:
        if(money < amt){
            return false;
        }
        money -= amt;
        return true;
        //  can't do anything so end here, we're broke
        //otherwise we take the money out of the wallet
        //we're so rich
    }
}
