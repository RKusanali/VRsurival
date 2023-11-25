using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class CharacterMovement : MonoBehaviour
{
    private float verticalSpeed;

    [SerializeField] private float currentSpeed = 5f;
    [SerializeField] private float normalSpeed = 5f;
    [SerializeField] private float waterSpeed = 1.5f;
    [SerializeField] private float jumpForce = 0.5f;
    [SerializeField] private float gravity = 9.8f;

    [SerializeField] private CharacterController characterController;
    [SerializeField] private ActionBasedContinuousMoveProvider continuousMoveProvider;

    private void Awake()
    {
        continuousMoveProvider = GetComponent<ActionBasedContinuousMoveProvider>();
        characterController = GetComponent<CharacterController>();
        float currentSpeed = normalSpeed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Water"))
        {
            continuousMoveProvider.moveSpeed = waterSpeed;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Water"))
        {
            continuousMoveProvider.moveSpeed = normalSpeed;
        }
    }
}