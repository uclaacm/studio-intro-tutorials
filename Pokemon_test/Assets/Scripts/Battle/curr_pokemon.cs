using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class curr_pokemon : MonoBehaviour
{
    public bool isPlayer;
    public BasePokemon basePokemon;
    public Pokemon pokemon;
    public void SetUp()
    {
        pokemon = new Pokemon(basePokemon);   
        if (isPlayer)
        {
            this.GetComponent<Image>().sprite = basePokemon.back_sprite;
        }
        else
        {
            this.GetComponent<Image>().sprite = basePokemon.front_sprite;
        }
    }
}
