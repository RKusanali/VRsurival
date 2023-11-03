using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class HandsAnimation : MonoBehaviour
{
    [SerializeField] private InputActionReference gripReference;
    [SerializeField] private InputActionReference triggerReference;


    [SerializeField] private Animator handAnimator;

    private GameObject sphere;
    public float sphereRadius = 0.15f;
    private AudioSource audioSource;
    public AudioClip audioClip;
    private float epsilon = 0.1f;

    private void Start()
    {
        // Créez la sphère
        sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        sphere.transform.position = this.transform.position;
        sphere.transform.localScale = new Vector3(sphereRadius * 2, sphereRadius * 2, sphereRadius * 2);
        sphere.SetActive(false);


        audioSource = sphere.AddComponent<AudioSource>();
        audioSource.clip = audioClip;
        audioSource.enabled = false;

    }

    // Update is called once per frame
    void Update()
    {
        float gripValue = gripReference.action.ReadValue<float>();
        handAnimator.SetFloat("Grip", gripValue);

        float triggerValue = triggerReference.action.ReadValue<float>();
        handAnimator.SetFloat("Trigger", triggerValue);

        if ((triggerValue > 1.0f - epsilon) & (gripValue > 1.0f - epsilon))
        {
            sphere.SetActive(true);
            audioSource.enabled = true;
            audioSource.Play();
        }
        else
        {
            sphere.SetActive(false);
            audioSource.Stop();
            audioSource.enabled = false;
        }

    }
}
