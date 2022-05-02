using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Display : MonoBehaviour
{
    public Text time;
    public Text level;
    public int counter;

    private void Start()
    {
        counter = 0;
        counter = PlayerPrefs.GetInt("time");
        InvokeRepeating("addTime", 0, 1);
        level.text = "Level: " + PlayerPrefs.GetInt("level");
    }
    void addTime()
    {
        counter++;
        time.text = "Time: " + counter.ToString() + "s";
        PlayerPrefs.SetInt("time", counter);
    }
}
