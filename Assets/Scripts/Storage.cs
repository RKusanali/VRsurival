using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Storage : MonoBehaviour
{
    [SerializeField] private Inventory inventory;
    public GameObject spawnArea;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Stone>())
        {
            inventory.set_stone();
        }

        else if (other.gameObject.GetComponent<Wood>())
        {
            inventory.set_wood();
        }

        if (other.CompareTag("Player"))
        {
            EnnemyAI[] components = FindObjectsOfType<EnnemyAI>();
            for (int i = 0; i < components.Length; i++)
            {
                components[i].setAggressive(false);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<Stone>())
        {
            inventory.set_stone(-1);
        }

        else if (other.gameObject.GetComponent<Wood>())
        {
            inventory.set_wood(-1);
        }
    }

    public float maxDistanceToRemove = 5f;
    public void RemoveStone(int count)
    {
        Stone[] components = FindObjectsOfType<Stone>();

        for (int i = 0; i < components.Length; i++)
        {
            float distance = Vector3.Distance(transform.position, components[i].transform.position);

            if (distance <= maxDistanceToRemove)
            {
                Destroy(components[i].gameObject);
                count--;

                if (count <= 0)
                {
                    break; 
                }
            }
        }
    }

    public void RemoveWood(int count)
    {
        Wood[] components = FindObjectsOfType<Wood>();

        for (int i = 0; i < components.Length; i++)
        {
            Bounds bounds = spawnArea.GetComponent<Collider>().bounds;
            bool x = (components[i].transform.position.x <= bounds.max.x && components[i].transform.position.x >= bounds.min.x ? true : false);
            bool y = (components[i].transform.position.y <= bounds.max.y && components[i].transform.position.y >= bounds.min.y ? true : false);
            bool z = (components[i].transform.position.z <= bounds.max.z && components[i].transform.position.z >= bounds.min.z ? true : false);

        if (x && y && z)
            {
                Destroy(components[i].gameObject);
                count--;

                if (count <= 0)
                {
                    break;
                }
            }
        }
    }
}
