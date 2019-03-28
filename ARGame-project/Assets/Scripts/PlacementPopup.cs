using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlacementPopup : MonoBehaviour
{
    public Animator animator;
    public Button placeButton;

    public void OpenPopup()
    {
        animator.SetBool("showPopup", true);
        placeButton.interactable = true;
    }

    public void ClosePopup()
    {
        animator.SetBool("showPopup", false);
        placeButton.interactable = false;
    }
}
