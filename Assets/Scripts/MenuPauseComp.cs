using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPauseComp : MonoBehaviour
{

    public static bool pausado;

    public GameObject menuPausePanel;

    /// <summary>
    /// Método para reiniciar a scene
    /// </summary>
    public void Restart()
    {
        Pause(false);
        ObstaculoComp.score = 0;
        ObstaculoComp.powerUp = 0;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    /// <summary>
    /// Método para pausar a scene
    /// </summary>
    public void Pause(bool isPausado)
    {
        pausado = isPausado;

        // Se o jogo estiver pausado, timescale recebe 0
        Time.timeScale = pausado ? 0 : 1;

        // Toggle menuPause
        menuPausePanel.SetActive(pausado);
    }

    /// <summary>
    /// Método para carregar uma scene
    /// </summary>
    public void CarregaScene(string nomeScene)
    {
        SceneManager.LoadScene(nomeScene);
    }


    // Start is called before the first frame update
    void Start()
    {
        // pausado = false;
#if !UNITY_ADS
        Pause(false);
#endif
    }

    // Update is called once per frame
    void Update()
    {

    }
}
