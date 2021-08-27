using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MouseHoverButton : MonoBehaviour
{

    private void Start()
    {
        transform.GetChild(0).GetComponent<Text>().color = Color.black;
        //创建事件触发器
        EventTrigger eventTri = this.gameObject.AddComponent<EventTrigger>();

        //加入鼠标进入事件
        EventTrigger.Entry entryPointerEnter = new EventTrigger.Entry();
        entryPointerEnter.eventID = EventTriggerType.PointerEnter;
        entryPointerEnter.callback = new EventTrigger.TriggerEvent();
        UnityAction<BaseEventData> pointerEnter = new UnityAction<BaseEventData>(OnPointerEnterTarget);
        entryPointerEnter.callback.AddListener(pointerEnter);
        eventTri.triggers.Add(entryPointerEnter);

        //加入鼠标离开事件
        EventTrigger.Entry entryPointerExit = new EventTrigger.Entry();
        entryPointerExit.eventID = EventTriggerType.PointerExit;
        entryPointerExit.callback = new EventTrigger.TriggerEvent();
        UnityAction<BaseEventData> pointerExit = new UnityAction<BaseEventData>(OnPointerExitTarget);
        entryPointerExit.callback.AddListener(pointerExit);
        eventTri.triggers.Add(entryPointerExit);

    }

    void OnPointerEnterTarget(BaseEventData baseEventData)
    {
        //鼠标进入时实现的功能
        transform.GetChild(0).GetComponent<Text>().color = Color.blue;
    }

    void OnPointerExitTarget(BaseEventData baseEventData)
    {
        //鼠标离开时实现的功能
        transform.GetChild(0).GetComponent<Text>().color = Color.black;
    }

}
