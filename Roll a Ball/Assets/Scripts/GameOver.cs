using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
   public TextMeshProUGUI scoreText;
   public GameObject gameOverBG;
   public void DisplayFinalScore(int score)
   {
       gameOverBG.SetActive(true);
       scoreText.text = string.Format("Score: {0}", score.ToString());
       ScoreController.score = 0;
       Time.timeScale = 0;
   }
   
   public void Restart()
   {
       SceneManager.LoadScene("Level 1 - Basics");
   }
}
