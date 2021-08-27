using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hint : MonoBehaviour
{
    private float hint_alpha = 1; //初始化时让UI显示
    public float alphaSpeed = 2f; //渐隐渐现的速度
    private CanvasGroup canvasGroup;

    private void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    private void Update()
    {
        if (canvasGroup == null)
        {
            return;
        }
        if (hint_alpha != canvasGroup.alpha)
        {
            canvasGroup.alpha = Mathf.Lerp(canvasGroup.alpha, hint_alpha, alphaSpeed * Time.deltaTime);
            if (Mathf.Abs(hint_alpha - canvasGroup.alpha) <= 0.01f)
            {
                canvasGroup.alpha = hint_alpha;
            }
        }


    }

    public void Hint_FadeIn_Event()
    {
        hint_alpha = 1;

        //canvasGroup.blocksRaycasts = true; //可以和该对象交互
    }

    public void Hint_FadeOut_Event()
    {
        hint_alpha = 0;
    }
}
