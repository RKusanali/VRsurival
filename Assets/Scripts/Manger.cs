using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manger : MonoBehaviour
{
    [SerializeField] private CharacterMovement characterMovement;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Meet"))
        {
            Destroy(other.gameObject);
            characterMovement.set_Hunger(characterMovement.get_Hunger() + 25.0f);
        }

        if (other.GetComponent<Drink>())
        {
            if (other.GetComponent<Drink>().isDrinkable())
            {
                other.GetComponent<Drink>().asDrink();
                characterMovement.set_Drink(characterMovement.get_Drink() + 15.0f);
            }
        }
    }
}

