using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public GameObject inventory;
    public GameObject anchor;
    bool UIActive;

    // Start is called before the first frame update
    void Start()
    {
        inventory.SetActive(false);
        UIActive = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (StaticsVar.CheckSecondaryLeft())
        {
            UIActive = true;
            inventory.SetActive(true);
        }

        /*
        if (UIActive)
        {
            inventory.transform.position = anchor.transform.position;
            inventory.transform.eulerAngles = new Vector3(anchor.transform.eulerAngles.x + 15, anchor.transform.eulerAngles.y, 0);
        }
        */
    }
}
