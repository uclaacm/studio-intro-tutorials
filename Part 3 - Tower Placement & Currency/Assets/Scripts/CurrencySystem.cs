using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrencySystem : MonoBehaviour
{
    
    public int money; //how much money we have/wallet

    public int startingMoney; //what the name suggests
    
    // Start is called before the first frame update
    void Start()
    {
        money = startingMoney;
        Debug.Log(money);
    }

    public void Gain(int amt){
        money += amt;
        Debug.Log(money);
    }

    public bool Use(int amt){
        if(money < amt){
            return false;
        }
        money -= amt;
        Debug.Log(money);
        return true;
    }

    // to be polished later
    public void Use_Test()
    {
        Debug.Log(Use(5));
    }
}
