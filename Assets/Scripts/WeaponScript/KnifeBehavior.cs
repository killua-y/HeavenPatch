using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeBehavior : MonoBehaviour
{

    public float rotationAmount = 1f;
    public int damageAmount = 10;

    private void Start()
    {

    }

    void Update()
    {
        transform.Rotate(Vector3.back, rotationAmount);


    }

    private void OnTriggerEnter(Collider other)
    {
        
    }


}

