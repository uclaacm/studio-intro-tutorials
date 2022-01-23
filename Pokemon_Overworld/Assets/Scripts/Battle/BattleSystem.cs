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

        // Adds player's pokemon's moves to the canvas. 
        dialogText.setMoves(player.basePokemon.moves);

        dialogText.DialogText.text = "A wild " + (string)(enemy.basePokemon.name) + " appeared";
        yield return new WaitForSeconds(1f);
        phase = Phases.ActionSelect;
        selected = 0;
        dialogText.DialogText.text = "What will you do?";
    }

    // Executes the attack phases for both the player and the enemy. It's currently
    // set up so that the player always attacks before the enemy
    IEnumerator ExecuteMoves(){

        // Gets the current move of the player's current pokemon that the player selected. And prints 
        // the pokemon and move.
        string move = player.basePokemon.moves[currentMove];
        dialogText.DialogText.text = $"{player.basePokemon.name} used {move}.";

        // Pausing for a second before move is executed
        yield return new WaitForSeconds(1f);
        
        // Calling takeDamage function on the enemy to make the enemy take damage based on the move that was selected
        bool isFainted = takeDamage(enemy, enemyHud, move);

        // Checking if the enemy fainted due to the player's attack and if so, display faint message
        if(isFainted){
            dialogText.DialogText.text = $"{enemy.basePokemon.name} fainted.";
            // EXIT SCENE
        }

        // If the enemy did not faint
        else{
            // Choosing a random move from the enemy's set of moves 
            int rand = Random.Range(0, enemy.basePokemon.moves.Count);
            string enemyMove = enemy.basePokemon.moves[rand];

            // Displaying move and pausing
            dialogText.DialogText.text = $"{enemy.basePokemon.name} used {enemyMove}.";
            yield return new WaitForSeconds(1f);

            // Damage player based on move
            bool isPlayerFainted = takeDamage(player, playerHud, enemyMove);

            if(isPlayerFainted){
                dialogText.DialogText.text = $"{player.basePokemon.name} fainted.";
                // EXIT SCENE
            }
            else{
                // If neither the player nor the enemy fainted, return to action select menu.
                phase = Phases.ActionSelect;
                dialogText.dialogToggle(true);
                dialogText.actionToggle(true);
                dialogText.moveToggle(false);
            }
        }
    }

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
                dialogText.highlightAction(selected);
                break;

            // Move select phase
            case Phases.MoveSelect:

                // If player wants to return to the action select page, they can press escape.
                if(Input.GetKeyDown(KeyCode.Escape)){
                    phase = Phases.ActionSelect;
                    dialogText.dialogToggle(true);
                    dialogText.actionToggle(true);
                    dialogText.moveToggle(false);
                }

                // Pressing the right arrow key or 'D' will set the currentMove value to '1' which indicates the second move.
                else if(Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D)){
                    if(currentMove < 1){
                        currentMove++;
                    }
                }

                // Pressing the left arrow key or 'A' will set the currentMove value to '0' which indicates the first move.
                else if(Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A)){
                    if(currentMove > 0){
                        currentMove--;
                    }
                }

                // Pressing space will execute the move that the player has chosen.
                else if(Input.GetKeyDown(KeyCode.Space)){
                    phase = Phases.Attacks;
                    dialogText.dialogToggle(true);
                    dialogText.moveToggle(false);
                    StartCoroutine(ExecuteMoves());
                }
                dialogText.highlightMove(currentMove);
                break;
        }
    }

    // Function to inflict damage onto pokemon based on moves.
    public bool takeDamage(curr_pokemon poke, hudScript hud, string move)
    {
        int damage = 0;
        
        // Hard coded damage values.
        if(move == "Hydro Pump"){
            damage = 50;
        }
        if(move == "Fire Blast"){
            damage = 30;
        }

        // Decrement pokemon's hp based on damage. 
        poke.pokemon.hp -= damage;

        // If pokemon has fainted.
        if(poke.pokemon.hp <= 0)
        {
            // Set pokemon's hp value to 0
            poke.pokemon.hp = 0;

            // Update pokemon's hp bar.
            hud.hp.setHP(poke.pokemon.hp);

            // Return true to say pokemon has fainted. 
            return true;
        }
        // Update pokemon's hp bar and return false to say pokemon hasn't fainted. 
        hud.hp.setHP(poke.pokemon.hp);
        return false;

    }
}
