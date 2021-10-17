using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimerController : MonoBehaviour
{
    // Time remaining in seconds
    [SerializeField] private int countdownTime;
    // UI component for time
    public TextMeshProUGUI countText;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(CountdownTimer());
    }

   IEnumerator CountdownTimer()
   {
       while(countdownTime > 0)
       {
           yield return new WaitForSeconds(1f);
           countText.text = string.Format("{0:D2}:{1:D2}", countdownTime / 60, countdownTime % 60);
           countdownTime--;
       }
       
       // Should display game over (restart scren)
   }
}
