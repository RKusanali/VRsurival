using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mangeable : MonoBehaviour
{
    private bool mangeable;

    private void Start()
    {
        mangeable = false;
    }

    public void set_mangeable(bool b)
    {
        mangeable = b;
    }

    public bool ismangeable()
    {
        return mangeable;
    }
}
