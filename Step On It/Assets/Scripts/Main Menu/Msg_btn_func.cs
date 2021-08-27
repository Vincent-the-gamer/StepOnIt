using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class Msg_btn_func : MonoBehaviour
{
    
    public void MouseEnter()
    {
        //重设按钮图片尺寸
        RectTransform rt = gameObject.GetComponent<RectTransform>();
        rt.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 145);
        rt.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 85);

    }

    public void MouseExit()
    {
        RectTransform rt = gameObject.GetComponent<RectTransform>();
        rt.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 125);
        rt.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 65);
    }

    public void OKClick()
    {
        //预处理
#if UNITY_EDITOR    //在编辑器模式下
        EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void CancelClick()
    {
        GameObject.Find("ExitGame_Message").SetActive(false);
    }
}
