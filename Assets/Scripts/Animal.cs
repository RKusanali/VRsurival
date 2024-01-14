using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.XR.Interaction.Toolkit;

public class Animal : MonoBehaviour
{
    [SerializeField] private float HP = 100.0f;
    public NavMeshAgent agent;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Weapon"))
        {
            this.HP -= (other.GetComponent<Sword>() ? 15.0f : 25.0f);
            if (this.HP < 0)
            {
                Destroy(this.gameObject);
                GameObject item = Resources.Load<GameObject>("KFC");
                Vector3 pos = new Vector3(this.transform.position.x, this.transform.position.y + 1.0f, this.transform.position.z);
                GameObject NEW = Instantiate(item, pos, Quaternion.identity);
                NEW.GetComponent<Rigidbody>().isKinematic = false;
                NEW.transform.SetParent(null);
                NEW.GetComponent<Item>().inSlot = false;
                NEW.GetComponent<Item>().currentSlot = null;
                XRGrabInteractable grabInteractable = NEW.GetComponent<XRGrabInteractable>();
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
            }
            else
            {
                UnityEngine.Vector3 v = new UnityEngine.Vector3(this.transform.position.x - Random.Range(0.0f, 1.0f), this.transform.position.y, this.transform.position.z - Random.Range(0.0f, 1.0f));
                this.transform.position = v;
                if(this.GetComponent<EnnemyAI>() != null)
                {
                    this.GetComponent<EnnemyAI>().setAggressive(Random.Range(0.0f,1.0f) > 0.75f);
                }
            }
        }
    }

    void Update()
    {
        if (!this.GetComponent<EnnemyAI>() && agent)
        {
            float b = (Random.Range(-1.0f, 1.0f) > 0.0f ? 1.0F : -1.0f);
            UnityEngine.Vector3 aleatoire = new UnityEngine.Vector3(this.transform.position.x + agent.speed * b * Random.Range(0.0f, 5.0f), this.transform.position.y, this.transform.position.z + agent.speed * b * Random.Range(0.0f, 4.0f));
            agent.SetDestination(aleatoire);
            this.transform.position = new UnityEngine.Vector3(this.transform.position.x + agent.speed * b * Random.Range(0.0f, 01.1f), this.transform.position.y, this.transform.position.z + agent.speed * b * Random.Range(0.0f, 01.1f));
        }
    }
}
