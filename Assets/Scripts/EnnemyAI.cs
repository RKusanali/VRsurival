using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.AI;

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
            }
        }
        else
        {
            agent.speed = 5.0f;
            UnityEngine.Vector3 aleatoire = new UnityEngine.Vector3(Random.Range(-5.0f, 5.0f), this.transform.position.y, Random.Range(-4.0f, 4.0f));
            agent.SetDestination(aleatoire);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") == true) {
            float current_hp = other.gameObject.GetComponent<CharacterMovement>().get_HP();
            other.gameObject.GetComponent<CharacterMovement>().set_HP(current_hp - Dgt);
        }
    }
}
