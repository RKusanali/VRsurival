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
    [SerializeField] private Button slotButon;
    [SerializeField] private int numberItems;
    [SerializeField] private TextMeshProUGUI text;

    public int NumberItems() {  return numberItems; }

    public void UpdateItem(int i = 1) { 
        numberItems = Math.Max(numberItems + i, 0); 
    }

    public void setColor(Color c) {
        var colors = slotButon.colors;
        colors.normalColor = c;
        slotButon.colors = colors;
    }

    public void setText(float f)
    {
        text.text = "";
        text.text = f.ToString("000.00");
    }

    public void setText2(int f)
    {
        text.text = "";
        text.text = f.ToString();
    }
}
