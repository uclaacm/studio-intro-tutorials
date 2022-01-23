using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class dialogScript : MonoBehaviour
{
    public Text DialogText;
    public GameObject ActionSelector;
    public GameObject MoveSelector;
    public List<Text> actions;
    public List<Text> moves;

    public void dialogToggle(bool on)
    {
        DialogText.enabled = on;
    }

    public void highlightAction(int selection)
    {
        for(int i = 0; i < actions.Count; i++) { 
            if(i == selection)
            {
                actions[i].color = Color.blue;
            }
            else
            {
                actions[i].color = Color.black;
            }
        }
    }

    // Change the text color of the selected move to blue.
    public void highlightMove(int currentMove)
    {
        for(int i = 0; i < moves.Count; i++) { 
            if(i == currentMove)
            {
                moves[i].color = Color.blue;
            }
            else
            {
                moves[i].color = Color.black;
            }
        }
    }

    // Toggles Action Select UI.
    public void actionToggle(bool on){
        ActionSelector.SetActive(on);
    }
    
    // Toggles Move Select UI. 
    public void moveToggle(bool on){
        MoveSelector.SetActive(on);
    }

    // Sets the placeholder move text to the moves of the player's pokemon. 
    public void setMoves(List<string> movesList){
        for(int i = 0; i < movesList.Count; i++){
            moves[i].text = movesList[i];
        }
    }
}
