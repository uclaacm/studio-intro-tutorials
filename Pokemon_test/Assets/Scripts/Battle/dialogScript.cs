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

    /*public IEnumerator typeText(string s)
    {
        DialogText.text = "";
        foreach (char c in s.ToCharArray())
        {
            DialogText.text += c;
            yield return new WaitForSeconds(.1f);
        }
    }*/

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

    public void actionToggle(bool on){
        ActionSelector.SetActive(on);
    }
    
    public void moveToggle(bool on){
        MoveSelector.SetActive(on);
    }

    public void setMoves(List<string> movesList){
        for(int i = 0; i < movesList.Count; i++){
            moves[i].text = movesList[i];
        }
    }
}
