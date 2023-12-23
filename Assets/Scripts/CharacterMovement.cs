using Meta.WitAi;
using System;
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

    [SerializeField] private float degradationTimer = 0.0f;

    [SerializeField] private float currentSpeed = 5f;
    [SerializeField] private float normalSpeed = 5f;
    [SerializeField] private float waterSpeed = 1.5f;
    [SerializeField] private float jumpForce = 0.5f;
    [SerializeField] private float gravity = 9.8f;

    private CharacterController characterController;
    private ActionBasedContinuousMoveProvider continuousMoveProvider;

    [SerializeField] private Inventory menu;

    void Start()
    {
        continuousMoveProvider = GetComponent<ActionBasedContinuousMoveProvider>();
        characterController = GetComponent<CharacterController>();
        float currentSpeed = normalSpeed;
    }

    void Update()
    {
        if (Time.time >= degradationTimer)
        {
            Hunger -= Hunger * 0.000005f;
            Drink -= Drink * 0.000005f;
            Hunger = Mathf.Max(0.0f, Hunger);
            Drink = Mathf.Max(0.0f, Drink);
                     
            if (Hunger + Drink < 5)
            {
                degradationTimer = Time.time + 5.0f;
                set_HP(HP - (HP * 0.000005f));
            }
            else if (Hunger + Drink < 25)
            {
                degradationTimer = Time.time + 15.0f;
                set_HP(HP - (HP * 0.000003f));
            }
            else if (Hunger + Drink < 50)
            {
                degradationTimer = Time.time + 30.0f;
                set_HP(HP - (HP * 0.000001f));
            }
            else if (Hunger + Drink < 75)
            {
                degradationTimer = Time.time + 60.0f;
            }
            else
            {
                degradationTimer = Time.time + 75.0f;
                set_HP(Math.Min(HP, HP + (HP * 1/10f)));
            }


            menu.set_hp_color(HP);
            menu.set_hunger_color(Hunger);
            menu.set_water_color(Drink);

            if (HP <= 0)
            {
                RestartGame();
            }

            Debug.Log(HP + "," + Hunger + "," + Drink + " ||| " + Time.time + " <> " + degradationTimer);
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

        else if (other.gameObject.GetComponent<EnnemyAI>() && other.gameObject.GetComponent<EnnemyAI>().isAggressive())
        {
            float current_hp = get_HP();
            this.HP = current_hp - other.gameObject.GetComponent<EnnemyAI>().DGT();
            menu.set_hp_color(HP);
            UnityEngine.Vector3 v = new UnityEngine.Vector3(this.transform.position.x - UnityEngine.Random.Range(0.0f, 1.0f), this.transform.position.y, this.transform.position.z - UnityEngine.Random.Range(0.0f, 1.0f) );
            this.transform.position = v;
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