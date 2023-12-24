using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Generator : MonoBehaviour
{
    public List<GameObject> animalPrefabs;
    public List<GameObject> objectPrefabs;
    public GameObject spawnArea;
    public float minSpawnTime = 5f;
    public float maxSpawnTime = 10f;

    private void Start()
    {
        StartCoroutine(SpawnObjects());
    }

    IEnumerator SpawnObjects()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(minSpawnTime, maxSpawnTime));

            if (spawnArea != null)
            {

                Vector3 randomSpawnPoint = GetRandomPointInBounds(spawnArea.GetComponent<Collider>().bounds);

                GameObject randomAnimal = GetRandomPrefab(animalPrefabs);
                GameObject randomObject = GetRandomPrefab(objectPrefabs);

                GameObject animal = Instantiate(randomAnimal, randomSpawnPoint, Quaternion.identity);
                GameObject spawnedObject = Instantiate(randomObject, randomSpawnPoint, Quaternion.identity);

                XRGrabInteractable grabInteractable = spawnedObject.GetComponent<XRGrabInteractable>();
                if (grabInteractable != null)
                {
                    XRInteractionManager interactionManager = FindObjectOfType<XRInteractionManager>();
                    if (interactionManager != null)
                    {
                        grabInteractable.interactionManager = interactionManager;
                    }
                    else
                    {
                        Debug.LogError("XR Interaction Manager not found in the scene.");
                    }
                }

                EnnemyAI _e = animal.GetComponent<EnnemyAI>();
                if (_e)
                {
                    XROrigin xr = FindAnyObjectByType<XROrigin>();
                    if (xr != null)
                    {
                        _e.player = xr.transform;
                    }
                }

                if(animal.GetComponent<EnnemyAI>())
                {
                    animal.GetComponent<EnnemyAI>().setAggressive(Random.Range(0.0f, 1.0f) > 0.80f);
                }
            }
        }
    }

    Vector3 GetRandomPointInBounds(Bounds bounds)
    {
        float x = Random.Range(bounds.min.x, bounds.max.x);
        float y = Random.Range(bounds.min.y, bounds.max.y);
        float z = Random.Range(bounds.min.z, bounds.max.z);

        return new Vector3(x, y, z);
    }

    GameObject GetRandomPrefab(List<GameObject> prefabs)
    {
        if (prefabs.Count > 0)
        {
            int randomIndex = Random.Range(0, prefabs.Count);
            return prefabs[randomIndex];
        }
        else
        {
            Debug.LogError("Prefab list is empty.");
            return null;
        }
    }
}

