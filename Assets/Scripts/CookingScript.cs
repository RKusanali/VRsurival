using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookingScript : MonoBehaviour
{
    [SerializeField] private GameObject prefabCru;
    [SerializeField] private GameObject prefabCuit;

    private void Awake()
    {
        prefabCru = transform.Find("Cru").gameObject;
        prefabCuit = transform.Find("Cuit").gameObject;

        if (prefabCru == null || prefabCuit == null)
        {
            Debug.LogError("Les prefabs pas trouvés.");
        }

        prefabCuit.SetActive(false);
        prefabCru.SetActive(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Meet"))
        {
            prefabCru.SetActive(false);
            prefabCuit.SetActive(true);
        }
    }
}
