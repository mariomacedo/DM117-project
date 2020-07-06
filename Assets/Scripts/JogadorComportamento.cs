using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(Rigidbody))]
public class JogadorComportamento : MonoBehaviour
{
    /// <summary>
    /// Reference for RigidBody component
    /// </summary>
    private Rigidbody rb;

    [Tooltip("Speed that ball side dodges")]
    [Range(0, 5)]
    public float velocidadeEsquiva = 5.0f;

    [Tooltip(" Speed that ball moves forward")]
    [Range(0, 5)]
    public float velocidadeRolamento = 5.0f;

    [Tooltip("Particle system da explosion")]
    public GameObject explosao;

    public AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        // Get access to component Rigidbody attached to this GO
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    /// <summary>
    /// OnCollisionEnter is called when this collider/rigidbody has begun
    /// touching another rigidbody/collider.
    /// </summary>
    /// <param name="collision">The Collision data associated with this collision.</param>
    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.name != "Chao")
        {
            audioSource.Play();
        }
    }

    /// <summary>
    /// Método para identificar se objetos foram tocados
    /// </summary>
    private static void TocarObjetos()
    {
        Ray toqueRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(toqueRay, out hit))
        {
            hit.transform.SendMessage("ObjetoTocado", SendMessageOptions.DontRequireReceiver);
        }
    }

    public void ObjetoTocado()
    {
        if (explosao != null)
        {
            var particulas = Instantiate(explosao, transform.position, Quaternion.identity);
            Destroy(particulas, 1.0f);
        }
        Destroy(this.gameObject);
    }


    // Update is called once per frame
    void Update()
    {
        var velocidadeHorizontal = Input.GetAxis("Horizontal") * velocidadeEsquiva;

        // Se o jogo está pausado, não faça nada
        if (MenuPauseComp.pausado)
        {
            return;
        }

        var forcaMovimento = new Vector3(velocidadeHorizontal, 0, velocidadeRolamento);

        // deltaTime : tempo gasto no frame anterior (algo perto de 1/60fps)
        // Usamos esse valor para garantir que o jogador se desloque com a mesma velocidade, desconsiderando o hardware
        forcaMovimento *= (Time.deltaTime * 60);
        rb.AddForce(forcaMovimento);
    }

}
