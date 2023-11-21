using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.UI;

public class StaticsVar : MonoBehaviour
{
    public enum XRButton
    {
        GOne, GTwo, GThree, GFour,
        DOne, DTwo, DThree, DFour
    }

    public static bool CheckButtonPressG(XRButton button, XRNode node)
    {
        InputDevice device = InputDevices.GetDeviceAtXRNode(node);

        bool buttonPressed = false;
        switch (button)
        {
            case XRButton.GOne:
                device.TryGetFeatureValue(CommonUsages.primaryButton, out buttonPressed);
                break;

            case XRButton.GTwo:
                device.TryGetFeatureValue(CommonUsages.secondaryButton, out buttonPressed);
                break;

            case XRButton.GThree:
                device.TryGetFeatureValue(CommonUsages.gripButton, out buttonPressed);
                break;

            case XRButton.GFour:
                device.TryGetFeatureValue(CommonUsages.triggerButton, out buttonPressed);
                break;


            default:
                Debug.LogError("Bouton Gauche XR non géré : " + button.ToString());
                break;
        }

        return buttonPressed;
    }

    public static bool CheckButtonPressD(XRButton button, XRNode node)
    {
        InputDevice device = InputDevices.GetDeviceAtXRNode(node);

        bool buttonPressed = false;
        switch (button)
        {
            case XRButton.DOne:
                device.TryGetFeatureValue(CommonUsages.primaryButton, out buttonPressed);
                break;

            case XRButton.DTwo:
                device.TryGetFeatureValue(CommonUsages.secondaryButton, out buttonPressed);
                break;

            case XRButton.DThree:
                device.TryGetFeatureValue(CommonUsages.gripButton, out buttonPressed);
                break;

            case XRButton.DFour:
                device.TryGetFeatureValue(CommonUsages.triggerButton, out buttonPressed);
                break;


            default:
                Debug.LogError("Bouton Droite XR non géré : " + button.ToString());
                break;
        }

        return buttonPressed;
    }

    public static bool CheckButtonPress(XRButton button, XRNode node)
    {
        return CheckButtonPressG(button, node) || CheckButtonPressD(button, node);
    }

    public static bool CheckPrimaryLeft()
    {
        return CheckButtonPressG(XRButton.GOne, XRNode.LeftHand);
    }

    public static bool CheckSecondaryLeft()
    {
        return CheckButtonPressG(XRButton.GTwo, XRNode.LeftHand);
    }

    public static bool CheckGrabLeft()
    {
        return CheckButtonPressG(XRButton.GThree, XRNode.LeftHand);
    }

    public static bool CheckTriggerLeft()
    {
        return CheckButtonPressG(XRButton.GFour, XRNode.LeftHand);
    }

    public static bool CheckPrimaryRight()
    {
        return CheckButtonPressD(XRButton.DOne, XRNode.RightHand);
    }

    public static bool CheckSecondaryRight()
    {
        return CheckButtonPressD(XRButton.DTwo, XRNode.RightHand);
    }

    public static bool CheckGrabRight()
    {
        return CheckButtonPressD(XRButton.DThree, XRNode.RightHand);
    }

    public static bool CheckTriggerRight()
    {
        return CheckButtonPressD(XRButton.DFour, XRNode.RightHand);
    }

}
