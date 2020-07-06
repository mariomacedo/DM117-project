using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPrincipal : MonoBehaviour
{
    public void CarregaScene(string nomeScene)
    {
        ObstaculoComp.score = 0;
        ObstaculoComp.powerUp = 0;
        SceneManager.LoadScene(nomeScene);

#if UNITY_ADS
        if(UnityAdControle.showAds){
            UnityAdControle.ShowAd();
        }
#endif
    }
}
