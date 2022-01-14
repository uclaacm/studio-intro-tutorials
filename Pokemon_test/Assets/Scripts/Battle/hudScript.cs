using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class hudScript : MonoBehaviour
{
    public Text nameText;
    //public GameObject hpBar;
    public hpScript hp;

    public void setHud(Pokemon pokemon)
    {
        nameText.text = pokemon.basePokemon.name;
        //hpBar.GetComponent<hpScript>().maxHp = pokemon.basePokemon.maxHp;
        hp.maxHp = pokemon.basePokemon.maxHp;
    }
}
