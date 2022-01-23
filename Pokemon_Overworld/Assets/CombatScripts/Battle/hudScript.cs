using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class hudScript : MonoBehaviour
{
    public Text nameText;
    public hpScript hp;

    public void setHud(Pokemon pokemon)
    {
        nameText.text = pokemon.basePokemon.name;
        hp.maxHp = pokemon.basePokemon.maxHp;
    }
}
