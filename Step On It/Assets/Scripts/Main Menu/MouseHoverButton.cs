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
        //Create Event Trigger
        EventTrigger eventTri = this.gameObject.AddComponent<EventTrigger>();

        //Add Mouse Enter Event
        EventTrigger.Entry entryPointerEnter = new EventTrigger.Entry();
        entryPointerEnter.eventID = EventTriggerType.PointerEnter;
        entryPointerEnter.callback = new EventTrigger.TriggerEvent();
        UnityAction<BaseEventData> pointerEnter = new UnityAction<BaseEventData>(OnPointerEnterTarget);
        entryPointerEnter.callback.AddListener(pointerEnter);
        eventTri.triggers.Add(entryPointerEnter);

        //Add Mouse Exit Event
        EventTrigger.Entry entryPointerExit = new EventTrigger.Entry();
        entryPointerExit.eventID = EventTriggerType.PointerExit;
        entryPointerExit.callback = new EventTrigger.TriggerEvent();
        UnityAction<BaseEventData> pointerExit = new UnityAction<BaseEventData>(OnPointerExitTarget);
        entryPointerExit.callback.AddListener(pointerExit);
        eventTri.triggers.Add(entryPointerExit);

    }

    void OnPointerEnterTarget(BaseEventData baseEventData)
    {
        //Function while mouse enter
        transform.GetChild(0).GetComponent<Text>().color = Color.blue;
    }

    void OnPointerExitTarget(BaseEventData baseEventData)
    {
        //Function while mouse exit
        transform.GetChild(0).GetComponent<Text>().color = Color.black;
    }

}
