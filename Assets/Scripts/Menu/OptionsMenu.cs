using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class OptionsMenu : MonoBehaviour
{
    public Dropdown sizeDropdown;
    public Text sizetext;
    public Slider obstacleSlider;
    public Text obstacletext;
    public void SetSize()
    {
        sizetext.text = "SIZE: " + (int) (sizeDropdown.value+10);
        PlayerPrefs.SetInt("size", (int) (sizeDropdown.value+10));
    }
    public void SetObstacles()
    {
        obstacletext.text = "OBSTACLES: " + (int)(obstacleSlider.value * Mathf.Pow((sizeDropdown.value + 8), 2));
        PlayerPrefs.SetInt("obstacles", (int)(obstacleSlider.value * Mathf.Pow((sizeDropdown.value + 8), 2)));
    }
}
