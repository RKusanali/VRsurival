using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireCooking : MonoBehaviour
{
    private bool isCooked = false;
    private void OnTriggerEnter(Collider other)
    {
        if (!isCooked && other.CompareTag("Meet"))
        {
            StartCoroutine(CookingCoroutine(other.gameObject));
        }
    }

    private IEnumerator CookingCoroutine(GameObject collidedObject)
    {
        Transform cruTransform = collidedObject.transform.Find("Cru");
        Transform cuitTransform = collidedObject.transform.Find("Cuit");

        if (cruTransform != null && cuitTransform != null)
        {
            yield return new WaitForSeconds(5f);

            cruTransform.gameObject.SetActive(false);
            cuitTransform.gameObject.SetActive(true);

            isCooked = true;
        }
        else
        {
            Debug.LogError("Pas de cuisson");
        }
    }
}

