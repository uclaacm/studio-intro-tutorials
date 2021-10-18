using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimerController : MonoBehaviour
{
    // Time to count down from
    [SerializeField] private int countdownTime;

    // When we started counting down
    private float startTime;

    // UI component for time
    [SerializeField] private TextMeshProUGUI countText;

    // UI Game Over component
    private GameOver gameOver;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        startTime = Time.time;
        Debug.Assert(countText != null, "TextController missing TextMeshProUGUI component.");
        gameOver = (GameOver)GameObject.FindObjectOfType(typeof(GameOver));
    }

    void Update()
    {
    	float timeElapsed = Time.time - startTime;

    	int timeRemaining;
    	if (timeElapsed < countdownTime)
    	{
    		timeRemaining = (int) (countdownTime - timeElapsed);
    	}
    	else
    	{
    		timeRemaining = 0; // Time remaining cannot go negative
            gameOver.DisplayFinalScore(ScoreController.score);
    	}

    	countText.text = string.Format("{0:D2}:{1:D2}", timeRemaining / 60, timeRemaining % 60);
    }
}
