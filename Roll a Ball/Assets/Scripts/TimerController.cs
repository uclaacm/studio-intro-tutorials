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

    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;
        Debug.Assert(countText != null, "TextController missing TextMeshProUGUI component.");
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
    	}

    	countText.text = string.Format("{0:D2}:{1:D2}", timeRemaining / 60, timeRemaining % 60);
    }
}
