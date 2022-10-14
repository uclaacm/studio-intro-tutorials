using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    float score = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

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
        return (int) Time.timeSinceLevelLoad;
    }
}
