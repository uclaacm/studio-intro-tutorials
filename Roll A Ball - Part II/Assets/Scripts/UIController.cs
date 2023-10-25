using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; 

public class UIController : MonoBehaviour
{
    [SerializeField] GameManager gm;
    [SerializeField] TMP_Text scoreText;

    // Update is called once per frame
    void Update()
    {
        scoreText.text = "Score: " + gm.GetScore();
    }
}
