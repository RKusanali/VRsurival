using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnnemyAI : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform player;
    void Update()
    {
        if (player != null)
        {
            agent.SetDestination(player.position);
        }
        else
        {
            Debug.LogWarning("Le joueur n'est pas d�fini. Assurez-vous de d�finir le joueur dans l'inspecteur.");
        }
    }
}
