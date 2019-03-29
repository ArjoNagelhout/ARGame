using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RepositionButton : MonoBehaviour
{
    public Animator animator;
    public Button repositionButton;

    public void ShowButton()
    {
        animator.SetBool("showRepositionButton", true);
        repositionButton.interactable = true;
    }

    public void HideButton()
    {
        animator.SetBool("showRepositionButton", false);
        repositionButton.interactable = false;
    }
}
