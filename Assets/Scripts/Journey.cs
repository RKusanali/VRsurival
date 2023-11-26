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

    void Start()
    {
        meshRenderer = background.GetComponent<Renderer>();
        meshRenderer.enabled = true;
        StartCoroutine(ChangeMaterialOverTime());
    }

    IEnumerator ChangeMaterialOverTime()
    {
        float elapsedTime = 0f;
        int last_materialIndex = 0;

        while (true)
        {
            float journeyProgress = elapsedTime / secondsPerJourney;
            int materialIndex = Mathf.FloorToInt(journeyProgress * dayMaterials.Length) % dayMaterials.Length;
            last_materialIndex = materialIndex;

            Debug.Log("Time: " + elapsedTime);
            Debug.Log("Progress Time:" + journeyProgress);
            Debug.Log("Material Index: " + materialIndex);

            if (materialIndex == 2 && Random.value < sadDayProbability && last_materialIndex != -1)
            {
                Debug.Log("Setting sadDay material");
                meshRenderer.material = sadDay;
                last_materialIndex = -1;
            }
            else
            {
                Debug.Log("Setting regular material");
                meshRenderer.material = dayMaterials[materialIndex];
                last_materialIndex = materialIndex;
            }

            RenderSettings.skybox = meshRenderer.material;

            yield return new WaitForSeconds(1f);
            elapsedTime += 1f;
        }
    }
}

