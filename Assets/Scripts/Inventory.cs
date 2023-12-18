using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using UnityEngine.XR.Interaction.Toolkit;

public class Inventory : MonoBehaviour
{
    public GameObject inventory;
    public GameObject anchor;
    [SerializeField] private float cooldownTime = 2.0f; 
    [SerializeField] private float nextActivationTime = 0.0f;
    bool UIActive;

    [SerializeField] private Slot[] slots;
    private List<string> liste;

    public Transform handTransform; 
    public GameObject itemPrefab;  
    private GameObject ghostItem;
    private bool release;

    // Start is called before the first frame update
    void Start()
    {
        inventory.SetActive(false);
        UIActive = false;
        itemPrefab = null; 
        ghostItem = null;
        release = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (StaticsVar.CheckSecondaryLeft())
        {
            UIActive = !UIActive;
            nextActivationTime = Time.time + cooldownTime;
        }
        
        if (UIActive)
        {
            inventory.SetActive(true);
            inventory.transform.position = anchor.transform.position;
            inventory.transform.eulerAngles = new Vector3(anchor.transform.eulerAngles.x + 15, anchor.transform.eulerAngles.y, 0);
        }
        else
        {
            inventory.SetActive(false);
        }

        if (ghostItem != null)
        {
            ghostItem.transform.position = handTransform.position;
        }

        if (StaticsVar.CheckGrabRight() && ghostItem != null)
        {
            ghostItem.SetActive(true);
        }
        else if(ghostItem != null)
        {
            ghostItem.SetActive(false);
            release = true;
        }

        if (itemPrefab != null && release)
        {
            SpawnItem();
            itemPrefab = null;
            release = false;
        }
    }

    public bool check(string s, int number = 1)
    {
        foreach (Slot slot in slots)
        {
            Debug.Log(System.Type.GetType(s));
            if (slot.ItemInSlot != null && slot.ItemInSlot.GetComponent(System.Type.GetType(s)) != null && slot.getnumberItems() >= number)
            {
                Debug.Log("Slot trouvé : " + slot.gameObject.name);
                return true;
            }
        }
        return false;
    }

    public void take(string s, int number = 1)
    {
        foreach (Slot slot in slots)
        {
            if (slot.ItemInSlot != null && slot.ItemInSlot.GetComponent(System.Type.GetType(s)) != null && slot.getnumberItems() >= number)
            {
                Debug.Log("Slot trouvé : " + slot.gameObject.name + "remove " + number + "items");
                slot.Remove(number);
            }
        }
    }

    public void SetSpawn(GameObject obj)
    { 
        itemPrefab = obj; 
    }

    public void PreSpawn(GameObject obj)
    {
        ghostItem = Instantiate(obj, handTransform.position, Quaternion.identity);
        ghostItem.GetComponent<Renderer>().material.color = new Color(1f, 1f, 1f, 0.5f);
        ghostItem.SetActive(false);
    }

    public void SpawnItem()
    {
        Transform t = handTransform;
        GameObject newItem = Instantiate(itemPrefab, t.position, Quaternion.identity);
        XRGrabInteractable newItemGrab = newItem.GetComponent<XRGrabInteractable>();
        if(newItemGrab != null)
        {
            XRInteractionManager interactionManager = FindObjectOfType<XRInteractionManager>();
            newItemGrab.interactionManager = interactionManager;
        }
        Destroy(ghostItem);
    }

    public void update_list(GameObject obj)
    {
        string s = string.Empty;
        s = obj.GetComponent<Sword>() ? "Sword" : s;
        s = obj.GetComponent<Shield>() ? "Shield" : s;
        s = obj.GetComponent<AxeHitBox>() ? "Axe" : s;
        s = obj.GetComponent<Wood>() ? "Wood" : s;
        s = obj.GetComponent<Stone>() ? "Stone" : s;
        s = obj.GetComponent<Boat>() ? "Boat" : s;
        liste.Add(s);
    }

    public void delete_item(GameObject obj) 
    {
        string s = string.Empty;
        s = obj.GetComponent<Sword>() ? "Sword" : s;
        s = obj.GetComponent<Shield>() ? "Shield" : s;
        s = obj.GetComponent<AxeHitBox>() ? "Axe" : s;
        s = obj.GetComponent<Wood>() ? "Wood" : s;
        s = obj.GetComponent<Stone>() ? "Stone" : s;
        s = obj.GetComponent<Boat>() ? "Boat" : s;
        if (liste.Contains(s))
        {
            liste.Remove(s);
        }
    }

    public bool As_item(GameObject obj)
    {
        string s = string.Empty;
        s = obj.GetComponent<Sword>() ? "Sword" : s;
        s = obj.GetComponent<Shield>() ? "Shield" : s;
        s = obj.GetComponent<AxeHitBox>() ? "Axe" : s;
        s = obj.GetComponent<Wood>() ? "Wood" : s;
        s = obj.GetComponent<Stone>() ? "Stone" : s;
        s = obj.GetComponent<Boat>() ? "Boat" : s;
        return (liste.Contains(s) && s != string.Empty);
    }
}
