using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    int score;
    // Start is called before the first frame update
    void Start()
    {
        score = 0;
    }

    public int GetScore() { 
        return score; 
    }

    public void AddScore(int amt)
    {
        score += amt;
    }
}
