using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreComp : MonoBehaviour
{

    public Text scoreText;

    // Update is called once per frame
    void Update()
    {
        scoreText.text = "Score: " + ((int)ObstaculoComp.score).ToString();
    }
}
