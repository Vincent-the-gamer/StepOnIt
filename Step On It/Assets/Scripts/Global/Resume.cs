using UnityEngine;
using System.Collections;

public class Resume : MonoBehaviour
{
    CanvasGroup cg;
    private void Start()
    {
        cg = GetComponentInParent<CanvasGroup>();
    }
    public void ResumeClick()
    {
        cg.alpha = 0;
        cg.interactable = false;
        Time.timeScale = 1;
    }
}
