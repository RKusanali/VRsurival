using DigitalRuby.RainMaker;
using System.Collections;
using System.Diagnostics.Tracing;
using UnityEngine;
using Random = UnityEngine.Random;
public class Journey : MonoBehaviour
{
    public Material[] dayMaterials;
    public Material sadDay;
    public float secondsPerJourney = 60f;
    public float sadDayProbability = 0.50f;
    [SerializeField] private GameObject rain_generator;

    private CharacterMovement characterMovement;
    [SerializeField] Camera controlcamera;

    public GameObject background;
    private GameObject _new;
    private Renderer meshRenderer;

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip day;
    [SerializeField] private AudioClip night;

    private bool ispluie;

    void Start()
    {
        meshRenderer = background.GetComponent<Renderer>();
        meshRenderer.enabled = true;
        StartCoroutine(ChangeMaterialOverTime());
        audioSource = FindAnyObjectByType<AudioSource>();

        audioSource.clip = day;
        audioSource.Play();

        characterMovement = (GameObject.FindObjectsOfType<CharacterMovement>()[0]);
        ispluie = false;
        _new = null;
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
            float r = Random.value;
            Debug.Log("Rain probability = " + materialIndex + " && " + r + "<" + sadDayProbability);

            if (!sadding)
            {
                if (materialIndex == 2 && r < sadDayProbability && !sadding)
                {
                    meshRenderer.material = sadDay;
                    sadding = true;
                    ispluie = true;
                    _new = Instantiate(rain_generator);
                    _new.GetComponent<RainScript>().Camera = controlcamera;
                    allumage[] allumageObjects = GameObject.FindObjectsOfType<allumage>();
                    foreach (allumage a in allumageObjects)
                    {
                        a.cut();
                    }

                    if (characterMovement)
                    {
                        characterMovement.changeSlowSpeed();
                    }
                    if(controlcamera)
                    {
                        controlcamera.fieldOfView = 30;
                        controlcamera.farClipPlane = 10;
                        RenderSettings.fog = true;
                        RenderSettings.fogMode = FogMode.Exponential;
                        RenderSettings.fogColor = Color.grey;
                        RenderSettings.fogDensity = 0.3f;
                    }
                }
                else
                {
                    meshRenderer.material = dayMaterials[materialIndex];

                    if (last_materialIndex != materialIndex)
                    {
                        if (materialIndex >= dayMaterials.Length - 1 && last_materialIndex <= dayMaterials.Length - 1)
                        {
                            audioSource.clip = night;
                            audioSource.Play();
                        }
                        else if (materialIndex <= dayMaterials.Length - 1 && last_materialIndex >= dayMaterials.Length - 1)
                        {
                            audioSource.clip = day;
                            audioSource.Play();
                        }
                    }
                }
            }
            else
            {
                if (materialIndex >= dayMaterials.Length - 1)
                {
                    sadding = false;
                    ispluie = false;
                    Destroy(_new);
                    _new = null;                  
                    if(characterMovement)
                    {
                        characterMovement.changeNormalspeed();
                    }
                    if (controlcamera)
                    {
                        controlcamera.fieldOfView = 60;
                        controlcamera.farClipPlane = 1000;
                    }
                    if (characterMovement)
                    {
                        if(characterMovement.get_HP() > 75.0f)
                        {
                            RenderSettings.fog = false;
                        }
                    }
                }
            }

            RenderSettings.skybox = meshRenderer.material;

            yield return new WaitForSeconds(1f);
            elapsedTime += 1f;
        }
    }

    public bool isRainning()
    {
        return ispluie;
    }
}

