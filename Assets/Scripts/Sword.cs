using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    public float dgt = 15.25f;
    public int durability = 20;

    private void Update()
    {
        if (durability > 0)
        {
            Destroy(this);
        }
    }
}
