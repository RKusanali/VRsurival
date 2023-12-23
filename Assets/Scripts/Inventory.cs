using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.HID;
using UnityEngine.SceneManagement;
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

    [SerializeField] private Storage storage;

    [SerializeField] private Slot slotWood;
    [SerializeField] private Slot slotStone;
    [SerializeField] private Slot slotHP;
    [SerializeField] private Slot slotHunger;
    [SerializeField] private Slot slotWater;

    public UnityEngine.Color danger;
    public UnityEngine.Color mid;
    public UnityEngine.Color good;
    

    public Transform handTransform; 
    public GameObject itemPrefab;  

    // Start is called before the first frame update
    void Start()
    {
        inventory.SetActive(false);
        UIActive = false;
        itemPrefab = null; 

        slotWood.setText2(slotWood.NumberItems());
        slotStone.setText2(slotStone.NumberItems());
        slotHP.setText(100.0f);
        slotHunger.setText(100.0f);
        slotWater.setText(100.0f);
    }

    // Update is called once per frame
    void Update()
    {
        if (StaticsVar.CheckPrimaryLeft() && !UIActive)
        {
            UIActive = true;
            nextActivationTime = Time.time + cooldownTime;
        }
        else if(UIActive && StaticsVar.CheckSecondaryLeft())
        {
            UIActive = false;
            nextActivationTime = Time.time + cooldownTime;
        }
        
        if (UIActive && !inventory.activeSelf)
        {
            inventory.SetActive(true);
            inventory.transform.position = anchor.transform.position;
            inventory.transform.eulerAngles = new Vector3(anchor.transform.eulerAngles.x + 15, anchor.transform.eulerAngles.y, 0);
        }
        else if (!UIActive)
        {
            inventory.SetActive(false);
        }

        if (itemPrefab != null)
        {
            SpawnItem();
            itemPrefab = null;
        }
    }

    public bool check(string s, int number = 1)
    {
        if (s.Equals("Stone")){
            if(slotStone != null)
            {
                return (slotStone.NumberItems() > number);
            }
        }
        else if (s.Equals("Wood"))
        {
            if (slotWood != null)
            {
                return (slotWood.NumberItems() > number);
            }
        }
        return false;
    }

    public void take(string s, int number = 1)
    {
        bool ok = check(s, number);
        if (ok)
        {
            if (s.Equals("Stone"))
            {
                if (slotStone != null)
                {
                    slotStone.UpdateItem(-1*number);
                    storage.RemoveStone(number);
                }
            }
            else if (s.Equals("Wood"))
            {
                if (slotWood != null)
                {
                    slotWood.UpdateItem(-1*number);
                    storage.RemoveWood(number);
                }
            }
        }
    }

    public void SetSpawn(GameObject obj)
    { 
        itemPrefab = obj; 
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

    }

    public void set_hp_color(float n)
    {
        if(n > 66)
        {
            slotHP.setColor(this.good);
        }
        else if(n > 33)
        {
            slotHP.setColor(this.mid);
        }
        else
        {
            slotHP.setColor(this.danger);
        }
        slotHP.setText(n);
    }

    public void set_hunger_color(float n)
    {
        if (n > 66)
        {
            slotHunger.setColor(this.good);
        }
        else if (n > 33)
        {
            slotHunger.setColor(this.mid);
        }
        else
        {
            slotHunger.setColor(this.danger);
        }
        slotHunger.setText(n);
    }

    public void set_water_color(float n)
    {
        if (n > 66)
        {
            slotWater.setColor(this.good);
        }
        else if (n > 33)
        {
            slotWater.setColor(this.mid);
        }
        else
        {
            slotWater.setColor(this.danger);
        }
        slotWater.setText(n);
    }

    public void set_wood(int number = 1)
    {
        slotWood.setText2(slotWood.NumberItems() + number);
        slotWood.UpdateItem(number);
    }

    public void set_stone(int number = 1)
    {
        slotStone.setText2(slotStone.NumberItems() + number);
        slotStone.UpdateItem(number);
    }
}
