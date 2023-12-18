using Meta.WitAi;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class CharacterMovement : MonoBehaviour
{
    private float verticalSpeed;

    [SerializeField] private float HP = 100.0f;
    [SerializeField] private float Hunger = 100.0f;
    [SerializeField] private float Drink = 100.0f;

    [SerializeField] private float degradationInterval = 60.0f;
    [SerializeField] private float degradationTimer = 0.0f;

    [SerializeField] private float currentSpeed = 5f;
    [SerializeField] private float normalSpeed = 5f;
    [SerializeField] private float waterSpeed = 1.5f;
    [SerializeField] private float jumpForce = 0.5f;
    [SerializeField] private float gravity = 9.8f;

    [SerializeField] private CharacterController characterController;
    [SerializeField] private ActionBasedContinuousMoveProvider continuousMoveProvider;

    void Start()
    {
        continuousMoveProvider = GetComponent<ActionBasedContinuousMoveProvider>();
        characterController = GetComponent<CharacterController>();
        float currentSpeed = normalSpeed;
        degradationTimer = degradationInterval; 

    }

    void Update()
    {
        if (Time.time >= degradationTimer)
        {
            Hunger -= Hunger * 0.01f;
            Drink -= Drink * 0.01f;
            Hunger = Mathf.Max(0.0f, Hunger);
            Drink = Mathf.Max(0.0f, Drink);

            if (Hunger + Drink < 50)
            {
                HP -= HP * 0.01f;
                degradationTimer = Time.time + 30.0f; 
            }
            else if (Hunger + Drink < 75)
            {
                HP -= HP * 0.01f;
                degradationTimer = Time.time + 60.0f; 
            }
            else if (Hunger + Drink < 25)
            {
                HP -= HP * 0.01f;
                degradationTimer = Time.time + 15.0f; 
            }
            else if (Hunger + Drink < 5)
            {
                HP -= HP * 0.01f;
                degradationTimer = Time.time + 5.0f; 
            }

            if (HP <= 0)
            {
                RestartGame();
            }
        }
    }

    void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
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

    public float get_HP()
    {
        return this.HP;
    }

    public float get_Hunger() {
        return this.Hunger;
    }
    public float get_Drink() { 
        return this.Drink; 
    }

    public void set_Drink(float drink)
    {
        this.Drink = drink;
    }

    public void set_Hunger(float Hunger)
    {
        this.Hunger = Hunger;
    }

    public void set_HP(float hp)
    {
        this.HP = hp;
    }
}