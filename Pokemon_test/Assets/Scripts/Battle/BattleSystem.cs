using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleSystem : MonoBehaviour
{
    public dialogScript dialogText;
    public Text fightText;
    public Text bagText;
    public hudScript playerHud;
    public curr_pokemon player;
    public hudScript enemyHud;
    public curr_pokemon enemy;

    private Phases phase = Phases.SetUp;
    private int selected = 0;

    private enum Phases {SetUp, ActionSelect, AttackSelect, ItemSelect, Attacks};

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Setup());
    }

    public IEnumerator Setup()
    {
        player.SetUp();
        enemy.SetUp();
        playerHud.setHud(player.pokemon);
        enemyHud.setHud(enemy.pokemon);
        //yield return dialogText.typeText("A wild " + (string)(enemy.basePokemon.name) +  " appeared");
        dialogText.DialogText.text = "A wild " + (string)(enemy.basePokemon.name) + " appeared";
        yield return new WaitForSeconds(1f);
        phase = Phases.ActionSelect;
        selected = 0;
        //yield return dialogText.typeText("What will you do?");
        dialogText.DialogText.text = "What will you do?";
    }


    // Update is called once per frame
    void Update()
    {
       switch (phase)
        {
            case Phases.ActionSelect:
                if(Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
                {
                    selected = (selected + 1) % 2;
                }
                else if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
                {
                    selected = -1 * (selected - 1) % 2;
                }
                else if (Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.Q))
                {
                    phase = Phases.AttackSelect;
                    dialogText.dialogToggle(false);
                }
                Debug.Log(selected);
                dialogText.highlightAction(selected);
                break;
        }
    }

    public void takeDamage(curr_pokemon poke, hudScript hud, int damage)
    {
        poke.pokemon.hp -= damage > poke.pokemon.hp ? poke.pokemon.hp : damage;
        hud.hp.setHP(poke.pokemon.hp);
        if(poke.pokemon.hp == 0)
        {
            //handle feinting
        }

    }
}
