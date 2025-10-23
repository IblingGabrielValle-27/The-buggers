using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamaraController : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform target;
    public Vector3 offset;
    public float speed = 0.125f;


    void LateUpdate()
    {
        if (target == null) return;
        Vector3 posicion = target.position + offset;
        Vector3 seguimientolento = Vector3.Lerp(transform.position, posicion, speed);
        transform.position = seguimientolento;
        transform.LookAt(target);

    }



}
