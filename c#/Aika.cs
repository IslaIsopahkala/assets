using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Aika : MonoBehaviour
{
    public float timeRemanining = 0;
    public bool timeIsRunning = false;
    public TMP_Text timeText;
    void Start()
    {
        timeIsRunning = true;
    }

    void Update()
    {
        if (timeIsRunning)
        {
            if (timeRemanining >= 0)
            {
                timeRemanining += Time.deltaTime;
                DisplayTime(timeRemanining);
            }

            if (timeRemanining >= 59) // minuutti on ihan joo hyv�, voi v�hent��
            {
                timeIsRunning = false;
                SceneManager.LoadScene(2);
            }
        }
    }

    void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1;
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        timeText.text = string.Format("{0:00} : {1:00}", minutes, seconds);
    }
}
