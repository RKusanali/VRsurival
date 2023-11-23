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
            currentSpeed = waterSpeed;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Water"))
        {
            currentSpeed = normalSpeed;
        }
    }
    private void Update()
    {
        if (StaticsVar.CheckSecondaryRight())
        {
            verticalSpeed = characterController.isGrounded ? Mathf.Sqrt(jumpForce * -2f * gravity) : 0f;
        }

        verticalSpeed += gravity * Time.deltaTime;

        Vector3 velocity = new Vector3(
            Input.GetAxis("Horizontal") * currentSpeed * Time.deltaTime,
            verticalSpeed * Time.deltaTime,
            Input.GetAxis("Vertical") * currentSpeed * Time.deltaTime
        );

        characterController.Move(velocity);
    }
}