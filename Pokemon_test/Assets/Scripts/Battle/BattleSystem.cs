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
    private int currentMove = 0;

    private enum Phases {SetUp, ActionSelect, MoveSelect, ItemSelect, Attacks};

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

        dialogText.setMoves(player.basePokemon.moves);

        //yield return dialogText.typeText("A wild " + (string)(enemy.basePokemon.name) +  " appeared");
        dialogText.DialogText.text = "A wild " + (string)(enemy.basePokemon.name) + " appeared";
        yield return new WaitForSeconds(1f);
        phase = Phases.ActionSelect;
        selected = 0;
        //yield return dialogText.typeText("What will you do?");
        dialogText.DialogText.text = "What will you do?";
    }

    IEnumerator ExecuteMoves(){
        string move = player.basePokemon.moves[currentMove];
        dialogText.DialogText.text = $"{player.basePokemon.name} used {move}.";
        yield return new WaitForSeconds(1f);
        
        bool isFainted = takeDamage(enemy, enemyHud, move);
        if(isFainted){
            dialogText.DialogText.text = $"{enemy.basePokemon.name} fainted.";
        }
        else{
            int rand = Random.Range(0, enemy.basePokemon.moves.Count);
            string enemyMove = enemy.basePokemon.moves[rand];
            dialogText.DialogText.text = $"{enemy.basePokemon.name} used {enemyMove}.";
            yield return new WaitForSeconds(1f);
        
            bool isPlayerFainted = takeDamage(player, playerHud, enemyMove);
            if(isPlayerFainted){
                dialogText.DialogText.text = $"{player.basePokemon.name} fainted.";
            }
            else{
                phase = Phases.ActionSelect;
                dialogText.dialogToggle(true);
                dialogText.actionToggle(true);
                dialogText.moveToggle(false);
            }
        }
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
                else if ((Input.GetKeyDown(KeyCode.Space) && selected == 0) || Input.GetKeyDown(KeyCode.Q))
                {
                    phase = Phases.MoveSelect;
                    dialogText.dialogToggle(false);
                    dialogText.actionToggle(false);
                    dialogText.moveToggle(true);
                }
                // Debug.Log(selected);
                dialogText.highlightAction(selected);
                break;
            case Phases.MoveSelect:
                if(Input.GetKeyDown(KeyCode.Escape)){
                    phase = Phases.ActionSelect;
                     dialogText.dialogToggle(true);
                    dialogText.actionToggle(true);
                    dialogText.moveToggle(false);
                }
                else if(Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D)){
                    if(currentMove < 1){
                        currentMove++;
                    }
                }
                else if(Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A)){
                    if(currentMove > 0){
                        currentMove--;
                    }
                }
                else if(Input.GetKeyDown(KeyCode.Space)){
                    phase = Phases.Attacks;
                    dialogText.dialogToggle(true);
                    dialogText.moveToggle(false);
                    StartCoroutine(ExecuteMoves());
                }
                dialogText.highlightMove(currentMove);
                break;
            case Phases.Attacks:
                break;
        }
    }

    public bool takeDamage(curr_pokemon poke, hudScript hud, string move)
    {
        int damage = 0;
        if(move == "Hydro Pump"){
            damage = 50;
        }
        if(move == "Fire Blast"){
            damage = 30;
        }
        // poke.pokemon.hp -= damage > poke.pokemon.hp ? poke.pokemon.hp : damage;
        // hud.hp.setHP(poke.pokemon.hp);
        poke.pokemon.hp -= damage;
        if(poke.pokemon.hp <= 0)
        {
            poke.pokemon.hp = 0;
            hud.hp.setHP(poke.pokemon.hp);
            return true;
        }
        hud.hp.setHP(poke.pokemon.hp);
        return false;

    }
}
