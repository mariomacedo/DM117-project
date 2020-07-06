using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControleCamera : MonoBehaviour
{

    [Tooltip("Camera target")]
    public Transform target;

    [Tooltip("Camera offset - target related")]
    public Vector3 offset = new Vector3(0, 3, -6);

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            // Update camera position
            transform.position = target.position + offset;

            // Update camera rotation directed to target
            transform.LookAt(target);
        }

    }
}
