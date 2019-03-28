using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementPopup : MonoBehaviour
{
    public Animator animator;

    public void OpenPopup()
    {
        animator.SetBool("showPopup", true);
    }

    public void ClosePopup()
    {
        animator.SetBool("showPopup", false);
    }
}
