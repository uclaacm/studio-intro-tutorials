using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class dialogScript : MonoBehaviour
{
    public Text DialogText;
    public GameObject ActionSelector;
    public List<Text> actions;

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

}
