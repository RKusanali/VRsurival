using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyTrigger : MonoBehaviour
{
    [SerializeField] private GameObject[] targets;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            foreach (GameObject target in targets)
            {
                target.GetComponent<TargetDummy>().ActivateDummy();
            }
        }
    }
}
