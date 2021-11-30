using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;


public class Clickable : MonoBehaviour
{
    GraphicRaycaster raycaster;
    PointerEventData pointerEventData;
    EventSystem eventSystem;

    void Start()
    {
        raycaster = GameObject.Find("Canvas").GetComponent<GraphicRaycaster>();
        eventSystem = GetComponent<EventSystem>();
    }
    
    protected bool HaveIBeenClicked()
    {
        pointerEventData = new PointerEventData(eventSystem);
        pointerEventData.position = Input.mousePosition;

        //Create a list of Raycast Results
        List<RaycastResult> results = new List<RaycastResult>();

        //Raycast using the Graphics Raycaster and mouse click position
        raycaster.Raycast(pointerEventData, results);
        if(results.Count > 0)
            return (gameObject == results[0].gameObject);
        return false;
    }
}
