using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Pokemon", menuName = "ScriptableObjects/Pokemon")]
public class BasePokemon : ScriptableObject
{
    public string Name;
    public int maxHp;
    public List<string> moves;
    public Sprite front_sprite;
    public Sprite back_sprite;
}
