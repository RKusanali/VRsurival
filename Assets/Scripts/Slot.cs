using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;

public class Slot : MonoBehaviour
{
    public GameObject ItemInSlot;
    [SerializeField] private RawImage SlotImage;
    [SerializeField] private int numberItems;
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private Inventory inventory;
    private String current_item;
    Color originalColor;
    
    void Start()
    {
        SlotImage = GetComponentInChildren<RawImage>();
        text = GetComponentInChildren<TextMeshProUGUI>(true);
        text.text = string.Empty;
        originalColor = SlotImage.color;
        numberItems = 0;
        current_item = string.Empty;
    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject obj = other.gameObject;
        if(!(obj.GetComponent<Sword>()
            || obj.GetComponent<Shield>()
            || obj.GetComponent<AxeHitBox>()
            || obj.GetComponent<Wood>()
            || obj.GetComponent<Stone>()
            || obj.GetComponent<Boat>()))
        {
            return;
        }

        if (!StaticsVar.CheckGrabRight()) return;

        if (ItemInSlot == null)
        {
            InsertItem(obj);
            inventory.update_list(obj);
        }
        else if(inventory.As_item(obj) == false)
        {
            MergeItems(obj);
        }   
    }

    void InsertItem(GameObject obj)
    {
        obj.GetComponent<Rigidbody>().isKinematic = false;
        obj.transform.SetParent(gameObject.transform, true);
        obj.transform.localPosition = Vector3.zero;
        obj.transform.localEulerAngles = obj.GetComponent<Item>().slotRotation;
        obj.GetComponent<Item>().inSlot = true;
        obj.GetComponent<Item>().currentSlot = this;
        ItemInSlot = obj;
        numberItems = (int)1;
        text.text = numberItems.ToString();
        obj.SetActive(false);

        string s = string.Empty;
        s = obj.GetComponent<Sword>() ? "Sword" : s;
        s = obj.GetComponent<Shield>() ? "Shield" : s;
        s = obj.GetComponent<AxeHitBox>() ? "Axe" : s;
        s = obj.GetComponent<Wood>() ? "Wood" : s;
        s = obj.GetComponent<Stone>() ? "Stone" : s;
        s = obj.GetComponent<Boat>() ?  "Boat" : s;
        current_item = s;
        Texture img = (Texture)Resources.Load<Texture>(s);
        SlotImage.texture = img;
    }

    void MergeItems(GameObject item)
    {
        if (item == null) return;

        string s = string.Empty;
        s = item.GetComponent<Sword>() ? "Sword" : s;
        s = item.GetComponent<Shield>() ? "Shield" : s;
        s = item.GetComponent<AxeHitBox>() ? "Axe" : s;
        s = item.GetComponent<Wood>() ? "Wood" : s;
        s = item.GetComponent<Stone>() ? "Stone" : s;
        s = item.GetComponent<Boat>() ? "Boat" : s;

        if (s == string.Empty || s != current_item) return;

        numberItems++;
        text.text = numberItems.ToString();
        Destroy(item);
    }

    bool IsItem(GameObject obj)
    {
        return obj.GetComponent<Item>();
    } 

    public void Remove(int number = 1)
    {
        if(numberItems >= number)
        {
            numberItems -= number;
            text.text = numberItems.ToString();
            if (numberItems <= 0)
            {              
                SlotImage.color = originalColor;
                inventory.delete_item(ItemInSlot);
                Destroy(ItemInSlot);
                ItemInSlot = null;
                current_item = string.Empty;
            }
        }
    }

    public void ResetColor()
    {
        SlotImage.color = originalColor;
    }

    public int getnumberItems()
    {
        return numberItems;
    }

    public void outItem()
    {
        if(ItemInSlot != null)
        {
            if (numberItems >= 0)
            {
                numberItems -= 1;
                text.text = numberItems.ToString();              

                GameObject newItem = Instantiate(this.ItemInSlot, this.transform.position, Quaternion.identity);
                XRGrabInteractable newItemGrab = newItem.GetComponent<XRGrabInteractable>();
                if (newItemGrab != null)
                {
                    XRInteractionManager interactionManager = FindObjectOfType<XRInteractionManager>();
                    newItemGrab.interactionManager = interactionManager;
                }

                if (numberItems <= 0)
                {                   
                    inventory.delete_item(ItemInSlot);
                    SlotImage.color = originalColor;
                    SlotImage.texture = null;
                    Destroy(this.ItemInSlot);
                    ItemInSlot = null;
                }
            }
        }
    }
}
