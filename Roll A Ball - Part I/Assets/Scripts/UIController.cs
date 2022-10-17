using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;  // Extra import for TextMeshPro

public class UIController : MonoBehaviour
{
    /// 
    /// This script controls the UI display.
    /// 

    [SerializeField] GameManager gm;  // Our game manager reference
    [SerializeField] TMP_Text txt;  // The text element that we are modifying

    void Update()
    {
        /// 
        /// So we are displaying the score of the player as well as the time that they have been on the level.
        /// All we have to do is set the text of the text display to the string that we need. 
        ///
        string display = "Score: " + gm.GetScore() + "\nTime: " + gm.GetTime();
        txt.text = display;
    }
}
