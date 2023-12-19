using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] private float elapsedTime;

    void Update()
    {
        elapsedTime += Time.deltaTime;
        int seconds = Mathf.FloorToInt(elapsedTime % 60);
        int minutes = Mathf.FloorToInt(elapsedTime / 60);
        int hours = Mathf.FloorToInt(elapsedTime / 3600);
        timerText.text = string.Format("{0:0}:{1:00}:{2:00}", hours ,minutes, seconds);
    }
}
