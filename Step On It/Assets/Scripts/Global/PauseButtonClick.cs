using UnityEngine;
using System.Collections;

public class PauseButtonClick : MonoBehaviour
{
    GameObject pauseImpl;
    CanvasGroup cg;
    private void Start()
    {
        pauseImpl = GameObject.Find("GUI/Canvas/PauseButton/PauseImplements");
        pauseImpl.SetActive(true);
        cg = GetComponentInChildren<CanvasGroup>();
        cg.alpha = 0;
        cg.interactable = false;
    }
    public void PauseClick()
   {
        cg.alpha = 1;
        cg.interactable = true;
        Time.timeScale = 0;
   }
}
