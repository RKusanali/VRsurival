using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class allumage : MonoBehaviour
{
    [SerializeField] FireCooking fireCooking;
    [SerializeField] ParticleSystem part;
    private int CountdownEvent = 0;

    private Journey j;

    void Start()
    {
        j = GameObject.FindObjectsOfType<Journey>()[0];
        fireCooking.setBright(false);
        part.Stop();
    }

    private void Update()
    {
        if (CountdownEvent >= 2 && !part.isPaused && !j.isRainning())
        {
            part.Play();
            fireCooking.setBright(true);
        }
    }

    public void cut()
    {
        fireCooking.setBright(false);
        part.Stop();
    }

    public void lunch()
    {
        fireCooking.setBright(true);
        part.Play();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") == true && !j.isRainning())
        {
            lunch();
        }
    }

}
