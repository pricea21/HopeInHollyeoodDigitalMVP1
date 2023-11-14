using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    //[SerializeField] private TMPro.TextMeshProUGUI timeText;
    public TextMeshProUGUI timer;
     
    private void FixedUpdate()
    {
        timer.text = GameManager.instance.timeRemaining.ToString();
        timer.text = $"{(int)GameManager.instance.timeRemaining / 60}:{(int)GameManager.instance.timeRemaining % 60:D2}";
    }
}
