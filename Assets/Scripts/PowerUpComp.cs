using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerUpComp : MonoBehaviour
{
    public Text powerUpText;

    // Update is called once per frame
    void Update()
    {
        powerUpText.text = "PowerUp: " + ObstaculoComp.powerUp.ToString();
    }
}
