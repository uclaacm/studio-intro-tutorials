using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    /// 
    /// So the intuition behind a GameManager is that all global variables are consolidated in an organized way.
    /// In our case, we only need to save the score and the time of the player, which could be stuck in any of
    /// the existing scripts but we use this as a template for organization such that your code is scalable. Do
    /// beware that you should only have one instance of the GameManager that every script would reference.
    /// 

    // Locally store our score
    float score = 0;

    public void AddScore(float amt)
    {
        score += amt;
    }

    public float GetScore()
    {
        return score;
    }

    public int GetTime()
    {
        ///
        /// Time.timeSinceLevelLoad will give the seconds elapsed since the current game scene has been loaded. 
        /// In other words, reloading the scene resets this number. We cast to integer (originally returns float)
        /// so it counts up by whole seconds.
        ///  

        return (int) Time.timeSinceLevelLoad;
    }
}
