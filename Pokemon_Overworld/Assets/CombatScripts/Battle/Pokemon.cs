using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pokemon
{
    public BasePokemon basePokemon;
    public int hp;

    public Pokemon(BasePokemon basePoke)
    {
        basePokemon = basePoke;
        hp = basePoke.maxHp;
    }
}
