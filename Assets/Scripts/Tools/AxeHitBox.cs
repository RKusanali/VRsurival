 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeHitBox : Tool
{
    Collider hitbox;
    public float swing_speed = 0;
    public float swing_speed_limit = 6f;
    public float dgt = 5.0f;
    public int durability = 30;
    Vector3 oldPos;    
    
    private void Start()
    {
        hitbox = GetComponent<Collider>();
        oldPos = transform.position;
    }    

    private void Update()
    {
        float dist = Vector3.Distance(oldPos, transform.position);
        swing_speed = dist / Time.deltaTime;
        oldPos = transform.position;

        if(durability <= 0 )
        {
            Destroy(this);
        }
    }
	
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Tree"))
        {
            if(swing_speed >= swing_speed_limit) 
               other.GetComponent<Tree>().Hit();
        }
    }
}
