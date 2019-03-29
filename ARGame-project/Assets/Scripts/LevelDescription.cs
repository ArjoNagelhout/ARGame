using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelDescription : MonoBehaviour
{
    public Text title;
    public Text subTitle;

    public Animator animator;

    public void ShowDescription(string newTitle, string newSubtitle)
    {
        title.text = newTitle;
        subTitle.text = newSubtitle;

        animator.SetTrigger("showDescription");

    }
}
    