using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
//using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

//completely new script frorm 3rd workshop

public class playerHealth : MonoBehaviour
{
    public int health;
    public int startingHealth = 100;
    public int enemyDamage; //how much damage the enemy does
    public Text healthText; //for displaying health
    public GameObject gameOverUI; //for when we die

    // Start is called before the first frame update
    void Start()
    {
        health = startingHealth;
        healthText.text = health.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        healthText.text = "Health: " + health;
       
        if (health <= 0) //check if the game is over
        {
            gameOverUI.SetActive(true);
            Time.timeScale = 0f;
        }
    }

    public void takeDamage()
    {
       health = health - enemyDamage;
       //Debug.Log(health);
    }
}
