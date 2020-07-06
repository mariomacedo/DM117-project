using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControladorJogo : MonoBehaviour
{
    [Tooltip("Referencia para o TileBasico")]
    public Transform tile;

    [Tooltip("Referencia para o obstaculo")]
    public Transform obstaculo;


    [Tooltip("Ponto para colocar o TileBasicoInicial")]
    public Vector3 pontoInicial = new Vector3(0, 0, -5);

    [Tooltip("Quantidade de Tiles Iniciais")]
    public int numSpawnIni;


    [Tooltip("Numeros de tiles sem obstaculos")]
    public int numTilesSemOBS = 4;

    /// <summary>
    /// Local para spawn do proximo Tile
    /// </summary>
    private Vector3 proxTilePos;

    /// <summary>
    /// Rotação do próximo Tile
    /// </summary>
    private Quaternion proxTileRot;

    // Start is called before the first frame update
    void Start()
    {
#if UNITY_ADS
        UnityAdControle.InitializeAds();
#endif
        proxTilePos = pontoInicial;
        proxTileRot = Quaternion.identity;

        for (int i = 0; i < numSpawnIni; i++)
        {
            SpawnProxTile(i >= numTilesSemOBS);
        }
    }

    public void SpawnProxTile(bool spawnObstaculos)
    {
        var novoTile = Instantiate(tile, proxTilePos, proxTileRot);
        var proxTile = novoTile.Find("PontoSpawn");
        proxTilePos = proxTile.position;
        proxTileRot = proxTile.rotation;

        if (!spawnObstaculos)
        {
            return;
        }

        // Podemos criar obstaculos

        var pontosObstaculo = new List<GameObject>();

        // Varrer GOs filhos buscando os pontos de spawn
        foreach (Transform filho in novoTile)
        {
            if (filho.CompareTag("obsSpawn"))
            {
                pontosObstaculo.Add(filho.gameObject);
            }
        }

        if (pontosObstaculo.Count > 0)
        {
            // Escolhe um obstaculo para renderizar
            var pontoSpawn = pontosObstaculo[Random.Range(0, pontosObstaculo.Count)];

            // Guarda pos do obj selecionado
            var obsSpawnPos = pontoSpawn.transform.position;

            // Cria novo obstaculo
            var novoObs = Instantiate(obstaculo, obsSpawnPos, Quaternion.identity);

            // Seta obstaculo no TileBasico.PontoSpawn
            novoObs.SetParent(pontoSpawn.transform);
        }

    }

    // Update is called once per frame
    void Update()
    {

    }
}
