using TMPro;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class Slot : MonoBehaviour
{
    public GameObject ItemInSlot;
    [SerializeField] private Image SlotImage;
    [SerializeField] private int numberItems;
    [SerializeField] private TextMeshProUGUI text;
    Color originalColor;

    [SerializeField] private XRController controller;
    
    void Start()
    {
        SlotImage = GetComponentInChildren<Image>();
        text = GetComponentInChildren<TextMeshProUGUI>(true);
        text.text = string.Empty;
        originalColor = SlotImage.color;
        numberItems = 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject obj = other.gameObject;
        if (!IsItem(obj)) return;

        Item itemComponent = obj.GetComponent<Item>();
        if (itemComponent == null) return;

        if (StaticsVar.CheckTriggerRight() || StaticsVar.CheckTriggerLeft())
        {
            if(ItemInSlot == null)
            {
                InsertItem(obj);
            }
            else
            {
                MergeItems(obj);
            }
        }
    }

    void MergeItems(GameObject item)
    {
        numberItems++;
        text.text = numberItems.ToString();
        Destroy(item);
    }

    bool IsItem(GameObject obj)
    {
        return obj.GetComponent<Item>();
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
        SlotImage.color = Color.gray;
        numberItems = (int) 1;
        text.text = numberItems.ToString();
    }

    void OutItem()
    {
        if (ItemInSlot != null)
        {
            SpawnItemInGame(ItemInSlot);

            numberItems = Mathf.Max(0, numberItems - 1);

            text.text = numberItems.ToString();

            if (numberItems <= 0)
            {
                ItemInSlot = null;
                SlotImage.color = originalColor;
            }
        }
    }

    void SpawnItemInGame(GameObject item)
    {
        GameObject NEW = Instantiate(item, this.transform.position, Quaternion.identity);
        NEW.GetComponent<Rigidbody>().isKinematic = true;
        NEW.transform.SetParent(null);
        NEW.GetComponent<Item>().inSlot = false;
        NEW.GetComponent<Item>().currentSlot = null;
    }

    public void ResetColor()
    {
        SlotImage.color = originalColor;
    }
}
