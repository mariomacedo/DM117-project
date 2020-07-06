using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class UnityAdControle : MonoBehaviour
{

    public static bool showAds = true;
    public static string gameId = "3653573";
    public static bool testMode = true;

    public static DateTime? proxTempoReward = null;

    public static ObstaculoComp obstaculo;

    /// <summary>
    /// Método para mostrar ad com recompensa
    /// </summary>
    public static void ShowRewardAd()
    {
#if UNITY_ADS

    proxTempoReward = DateTime.Now.AddSeconds(15);
    if(Advertisement.IsReady())
    {
        MenuPauseComp.pausado = true;
        Time.timeScale = 0f;

        // Outra forma de criar o showOptions e setar o callback
        var opcoes = new ShowOptions
        {
            resultCallback = TratarMostrarResultado
        };

        Advertisement.Show(opcoes);
    }
#endif
    }

    /// <summary>
    /// Método para tratar o resultado com reward
    /// </summary>
#if UNITY_ADS
    public static void TratarMostrarResultado(ShowResult result) 
    {
        switch (result)
        {
            case ShowResult.Finished:
                // Anuncio mostrado Continue o jogo
                obstaculo.Continue();
            break;

            case ShowResult.Skipped:
                Debug.Log("Ad pulado. Faz Nada");
            break;

            case ShowResult.Failed:
                Debug.LogError("Erro no ad. Faz nada");
            break;
            
        }

        // Saia do modo pausado
        MenuPauseComp.pausado = false;
        Time.timeScale = 1f;
    }
#endif

    public static void ShowAd()
    {
#if UNITY_ADS

        // Opcoes para o ad
        ShowOptions opcoes = new ShowOptions();

        opcoes.resultCallback = Unpause;

        if (Advertisement.IsReady())
        {
            Advertisement.Show(opcoes);
        }

        // Comentado por conta da versão do unity Ad
        // MenuPauseComp.pausado = true;
        // Time.timeScale = 0;
#endif
    }

    public static void Unpause(ShowResult result)
    {
        // Quando o anuncio acabar sai do modo pausado
        MenuPauseComp.pausado = false;
        Time.timeScale = 1f;
    }

    public static void InitializeAds()
    {
        Advertisement.Initialize(gameId, testMode);
    }


    // Start is called before the first frame update
    void Start()
    {
        showAds = true;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
