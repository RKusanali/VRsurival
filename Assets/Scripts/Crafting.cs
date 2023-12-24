using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Crafting : MonoBehaviour
{
  
    [SerializeField] GameObject Player;
    [SerializeField] Inventory inventory;
    [SerializeField] Transform Anchor;
    Color originalColor;
    Color NullColor;

    public Button[] buttons;

    void Start()
    {
        inventory = Player.GetComponent<Inventory>();
        originalColor = Color.white;
        NullColor = Color.grey;
    }

    void Update()
    {
        if (buttons[0])
        {
            if (!sword_requirement())
            {
                ColorBlock colorBlock = buttons[0].colors;
                RawImage rawImage = buttons[0].GetComponentInChildren<RawImage>();
                colorBlock.normalColor = NullColor;
                colorBlock.highlightedColor = NullColor;
                colorBlock.pressedColor = NullColor;
                rawImage.color = NullColor;
            }
            else
            {
                ColorBlock colorBlock = buttons[0].colors;
                RawImage rawImage = buttons[0].GetComponentInChildren<RawImage>();
                colorBlock.normalColor = originalColor;
                colorBlock.highlightedColor = originalColor;
                colorBlock.pressedColor = originalColor;
                rawImage.color = originalColor;
            }
        }

        if (buttons[1])
        {
            if (!shield_requirement())
            {
                ColorBlock colorBlock = buttons[1].colors;
                RawImage rawImage = buttons[1].GetComponentInChildren<RawImage>();
                colorBlock.normalColor = NullColor;
                colorBlock.highlightedColor = NullColor;
                colorBlock.pressedColor = NullColor;
                rawImage.color = NullColor;
            }
            else
            {
                ColorBlock colorBlock = buttons[1].colors;
                RawImage rawImage = buttons[1].GetComponentInChildren<RawImage>();
                colorBlock.normalColor = originalColor;
                colorBlock.highlightedColor = originalColor;
                colorBlock.pressedColor = originalColor;
                rawImage.color = originalColor;
            }
        }

        if (buttons[2])
        {
            if (!axe_requirement())
            {
                ColorBlock colorBlock = buttons[2].colors;
                RawImage rawImage = buttons[2].GetComponentInChildren<RawImage>();
                colorBlock.normalColor = NullColor;
                colorBlock.highlightedColor = NullColor;
                colorBlock.pressedColor = NullColor;
                rawImage.color = NullColor;
            }
            else
            {
                ColorBlock colorBlock = buttons[2].colors;
                RawImage rawImage = buttons[2].GetComponentInChildren<RawImage>();
                colorBlock.normalColor = originalColor;
                colorBlock.highlightedColor = originalColor;
                colorBlock.pressedColor = originalColor;
                rawImage.color = originalColor;
            }
        }

        if (buttons[3])
        {
            if (!boat_requirement())
            {
                ColorBlock colorBlock = buttons[3].colors;
                RawImage rawImage = buttons[3].GetComponentInChildren<RawImage>();
                colorBlock.normalColor = NullColor;
                colorBlock.highlightedColor = NullColor;
                colorBlock.pressedColor = NullColor;
                rawImage.color = NullColor;
            }
            else
            {
                ColorBlock colorBlock = buttons[3].colors;
                RawImage rawImage = buttons[3].GetComponentInChildren<RawImage>();
                colorBlock.normalColor = originalColor;
                colorBlock.highlightedColor = originalColor;
                colorBlock.pressedColor = originalColor;
                rawImage.color = originalColor;
            }
        }
    }

    public bool sword_requirement()
    {
        if (inventory != null)
        {
            if(inventory.check("Wood", 3) && inventory.check("Stone", 1))
            {
                return true;
            }
        }
        return false;
    }

    public bool shield_requirement()
    {
        if (inventory != null)
        {
            if (inventory.check("Wood", 3) && inventory.check("Stone", 10))
            {
                return true;
            }
        }
        return false;
    }
    public bool axe_requirement()
    {
        if (inventory != null)
        {
            if (inventory.check("Wood", 3) && inventory.check("Stone", 5))
            {
                return true;
            }
        }
        return false;
    }

    public bool boat_requirement()
    {
        if (inventory != null)
        {
            if (inventory.check("Wood", 100))
            {
                return true;
            }
        }
        return false;
    }

    public void sword_craft()
    {
        if (inventory != null)
        {
            if (sword_requirement())
            {
                inventory.take("wood", 3);
                inventory.take("stone", 1);
                GameObject prefab = Resources.Load<GameObject>("Sword");
                inventory.SetSpawn(prefab);
            }
        }
    }

    public void shield_craft()
    {
        if (inventory != null)
        {
            if (shield_requirement())
            {
                inventory.take("wood", 3);
                inventory.take("stone", 10);
                GameObject prefab = Resources.Load<GameObject>("Shield");
                inventory.SetSpawn(prefab);
            }
        }
    }

    public void axe_craft()
    {
        if (inventory != null)
        {
            if (axe_requirement())
            {
                inventory.take("wood", 3);
                inventory.take("stone", 5);
                GameObject prefab = Resources.Load<GameObject>("Axe");
                inventory.SetSpawn(prefab);
            }
        }
    }


    public void boat_craft()
    {
        if (inventory != null)
        {
            if (boat_requirement())
            {
                inventory.take("wood", 100);
                GameObject prefab = Resources.Load<GameObject>("Boat");
                inventory.SetSpawn(prefab);
                StartCoroutine(EndTime());
            }
        }
    }

    IEnumerator EndTime()
    {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene("Ending");
    }
}
