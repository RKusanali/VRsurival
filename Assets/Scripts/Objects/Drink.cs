using Meta.WitAi;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drink : MonoBehaviour
{
    [SerializeField] private GameObject Drinkable;
    [SerializeField] private bool todrink;
    [SerializeField] private Material OkDrinkable;
    [SerializeField] private Material NonDrinkable;

    void Start()
    {
        Drinkable.SetActive(false);
        todrink = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Water"))
        {
            Drinkable.SetActive(true);
            Drinkable.GetComponent<Renderer>().material = NonDrinkable;
        }
    }

    public void set_drinkable()
    {
        Drinkable.SetActive(true);
        todrink = true;
        Drinkable.GetComponent<Renderer>().material = OkDrinkable;
    }

    public bool isDrinkable()
    {
        return todrink;
    }

    public void asDrink()
    {       
        todrink = false;
        Drinkable.GetComponent<Renderer>().material = null;
        Drinkable.SetActive(false);
    }
            
}
