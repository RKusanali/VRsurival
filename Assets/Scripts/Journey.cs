using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;
public class Journey : MonoBehaviour
{
    public Material[] dayMaterials;
    public Material sadDay;
    public float secondsPerJourney = 60f;
    public float sadDayProbability = 0.50f;

    public GameObject background;
    private Renderer meshRenderer;

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip day;
    [SerializeField] private AudioClip night;

    void Start()
    {
        meshRenderer = background.GetComponent<Renderer>();
        meshRenderer.enabled = true;
        StartCoroutine(ChangeMaterialOverTime());
        audioSource = FindAnyObjectByType<AudioSource>();

        audioSource.clip = day;
        audioSource.Play();
    }

    IEnumerator ChangeMaterialOverTime()
    {
        float elapsedTime = 0f;
        int last_materialIndex = -1;
        int materialIndex = 0;
        bool sadding = false;
        float journeyProgress = 0.0f;

        while (true)
        {
            journeyProgress = elapsedTime / secondsPerJourney;
            last_materialIndex = materialIndex;
            materialIndex = Mathf.FloorToInt(journeyProgress * dayMaterials.Length) % dayMaterials.Length;

            if (materialIndex == 2 && Random.value < sadDayProbability && !sadding)
            {
                meshRenderer.material = sadDay;
                sadding = true;
            }
            else
            {
                meshRenderer.material = dayMaterials[materialIndex];

                if(last_materialIndex != materialIndex)
                {
                    if (materialIndex >= dayMaterials.Length - 1 && last_materialIndex <= dayMaterials.Length - 1)
                    {
                        audioSource.clip = night;
                        audioSource.Play();
                    }
                    else if(materialIndex <= dayMaterials.Length - 1 && last_materialIndex >= dayMaterials.Length - 1)
                    {
                        audioSource.clip = day;
                        audioSource.Play();
                    }                   
                }                 
            }

            if (materialIndex >= dayMaterials.Length - 1) sadding = false;

            RenderSettings.skybox = meshRenderer.material;

            yield return new WaitForSeconds(1f);
            elapsedTime += 1f;
        }
    }
}

