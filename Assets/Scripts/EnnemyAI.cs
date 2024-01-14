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
    [SerializeField] private float seuil = 10.0f;
    [SerializeField] Animation animator;
    private float originalspeed;

    private void Start()
    {
        animator = this.GetComponent<Animation>();
        if (animator) animator.Play("walk");
        originalspeed = agent.speed;
    }

    void Update()
    {
        float d = UnityEngine.Vector2.Distance(new UnityEngine.Vector2(this.transform.position.x, this.transform.position.z), new UnityEngine.Vector2(player.position.x, player.position.z));
        Debug.Log(this.transform.position + " - " + d + " < " + seuil + " --> " + (d < seuil) + " ==> " + agent.speed);
        if (animator) animator.Play("walk");
        if (player != null && d < seuil)
        {
            agent.speed += Mathf.Min((5.0f / d)/10.0f, 0.005f);
            agent.speed = Mathf.Min(agent.speed, 6.0f);
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
            agent.speed = originalspeed;
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
