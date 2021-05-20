using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Timer : MonoBehaviour
{
    public Text timerText;
    private int timeGame = 0;
    
    public void SetTimerText()
    {
        timerText.text = GetTimerText();
        StartCoroutine(TimerText());
    }

    string GetTimerText()
    {
        string timerTXT = "00:00";
        if(timeGame > 0)
        {
            int seconds = timeGame % 60;
            int minutes = (timeGame - seconds)/60;
            timerTXT = $"{(minutes < 10 ? "0" : "")}{minutes}:{(seconds < 10 ? "0" : "")}{seconds}";
        }

        return timerTXT;
    }

    IEnumerator TimerText()
    {
        yield return new WaitForSeconds(1);
        timeGame++;
        SetTimerText();
    }
}
