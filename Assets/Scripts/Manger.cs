using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Meet"))
        {
            Destroy(other.gameObject);
        }
    }
}

