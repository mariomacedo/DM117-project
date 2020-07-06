using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ObstaculoComp : MonoBehaviour
{
    [Tooltip("Particle system da explosion")]
    public GameObject explosao;

    [Tooltip("Quanto tempo antes de reiniciar o jogo")]
    public float tempoEspera = 2.0f;

    [Tooltip("Acesso para o componente MeshRenderer")]
    MeshRenderer mr = new MeshRenderer();

    [Tooltip("Acesso para o componente boxCollider")]
    BoxCollider bc = new BoxCollider();

    /// <summary>
    /// Variável referência para o jogador
    /// </summar>
    private GameObject jogador;

    public static double score = 0;
    public static int powerUp = 0;
    private static int powerUpCount = 0;

    private void OnCollisionEnter(Collision collision)
    {
        // Verifica se eh o jogador
        if (collision.gameObject.GetComponent<JogadorComportamento>())
        {
            MenuPauseComp.pausado = true;
            // Esconde o jogador ao invés de destruir
            collision.gameObject.SetActive(false);
            // Destroy(collision.gameObject);
            jogador = collision.gameObject;
            // Chama a função ResetaJogo depois de um tempo
            Invoke("ResetaJogo", tempoEspera);
        }

    }

    private static void ClicarObjetos(Vector2 screen)
    {
        Ray toqueRay = Camera.main.ScreenPointToRay(screen);
        RaycastHit hit;
        if (Physics.Raycast(toqueRay, out hit))
        {
            hit.transform.SendMessage("ObstaculoTocado", SendMessageOptions.DontRequireReceiver);
        }
    }

    public void ObstaculoTocado()
    {
        if (powerUp > 0)
        {
            if (explosao != null)
            {
                var particulas = Instantiate(explosao, transform.position, Quaternion.identity);
                Destroy(particulas, 1.0f);
            }

            mr.enabled = false;
            bc.enabled = false;
            Destroy(this.gameObject);
            powerUp--;
        }
    }


    /// <summary>
    /// Reinicia Jogo
    /// </summary>
    private void ResetaJogo()
    {
        var gameOverMenu = GetGameOverMenu();
        gameOverMenu.SetActive(true);

        var botoes = gameOverMenu.transform.GetComponentsInChildren<Button>();
        Button botaoContinue = null;

        foreach (var botao in botoes)
        {
            if (botao.gameObject.name.Equals("BotaoContinuar"))
            {
                botaoContinue = botao;
                break;
            }
        }

        if (botaoContinue)
        {

#if UNITY_ADS

        StartCoroutine(ShowContinue(botaoContinue));
        // botaoContinue.onClick.AddListener(UnityAdControle.ShowRewardAd);
        // UnityAdControle.obstaculo = this;
#else
            botaoContinue.gameObject.SetActive(false);
#endif

        }

        // SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public IEnumerator ShowContinue(Button botaoContinue)
    {
        var bntText = botaoContinue.GetComponentInChildren<Text>();
        while (true)
        {
            if (UnityAdControle.proxTempoReward.HasValue && (DateTime.Now < UnityAdControle.proxTempoReward.Value))
            {
                botaoContinue.interactable = false;

                TimeSpan restante = UnityAdControle.proxTempoReward.Value - DateTime.Now;

                var contagemRegressiva = string.Format("{0:D2}:{1:D2}", restante.Minutes, restante.Seconds);

                bntText.text = contagemRegressiva;

                yield return new WaitForSeconds(1f);
            }
            else
            {
                botaoContinue.interactable = true;
                botaoContinue.onClick.AddListener(UnityAdControle.ShowRewardAd);
                UnityAdControle.obstaculo = this;
                bntText.text = "Continue (Ad)";
                break;
            }
        }
    }

    /// <summary>
    /// Reinicia Jogo
    /// </summary>
    public void Continue()
    {
        var go = GetGameOverMenu();
        go.SetActive(false);
        jogador.SetActive(true);

        ObstaculoTocado();
    }

    GameObject GetGameOverMenu()
    {
        return GameObject.Find("Canvas").transform.Find("MenuGameOver").gameObject;
    }


    // Start is called before the first frame update
    void Start()
    {
        mr = GetComponent<MeshRenderer>();
        bc = GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ClicarObjetos(Input.mousePosition);
        }

        if (!MenuPauseComp.pausado)
        {
            score = score + 0.1;
            powerUpCount++;

            if (powerUpCount > 5000)
            {
                powerUp++;
                powerUpCount = 0;
            }

        }

    }
}
