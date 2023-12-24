using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.AI;
using Vector3 = UnityEngine.Vector3;

public class EnnemyAI : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform player;
    [SerializeField] private bool agressive;
    [SerializeField] GameObject item;
    [SerializeField] private float Dgt = 15.0f;
    [SerializeField] private float seuil = 100.0f;

    void Update()
    {
        float d = UnityEngine.Vector3.Distance(this.transform.position, player.position);
        if (player != null && d < seuil)
        {           
            agent.speed = 5.0f / d;
            if(agressive)
            {
                agent.SetDestination(player.position);
            }
            else
            {
                agent.SetDestination(-1*player.position);
                setAggressive(Random.Range(0.0f, 1.0f) > 0.75f);
            }
        }
        else
        {
            agent.speed = 5.0f;
            UnityEngine.Vector3 aleatoire = new UnityEngine.Vector3(Random.Range(-5.0f, 5.0f), this.transform.position.y, Random.Range(-4.0f, 4.0f));
            agent.SetDestination(aleatoire);
        }
    }

    public bool isAggressive()
    {
        return agressive;
    }

    public void setAggressive(bool b)
    {
        agressive = b;
    }

    public float DGT()
    {
        return Dgt;
    }

    public void set_speed(float f)
    {
        agent.speed = f;
    }

    public float get_speed() { return agent.speed; }

    public void set_pos(UnityEngine.Vector3 v)
    {
        this.transform.position = v;
    }

    public float x()
    {
        return this.transform.position.x;
    }

    public float y()
    {
        return this.transform.position.y;
    }

    public float z()
    {
        return this.transform.position.z;
    }
}
