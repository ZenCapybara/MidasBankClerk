using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;


public class Clickable : MonoBehaviour
{
    GraphicRaycaster raycaster;
    PointerEventData pointerEventData;
    EventSystem eventSystem;

    void Awake()
    {
        raycaster = GameObject.Find("Canvas").GetComponent<GraphicRaycaster>();
        eventSystem = GetComponent<EventSystem>();
    }
    
    /// <summary>
    /// Use if object must be on top of pile to be clicked.
    /// </summary>
    /// <returns></returns>
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

    /// <summary>
    /// Use if obejct can be clicked through pile.
    /// </summary>
    /// <returns></returns>
    protected bool HasSomethingBeenClickedOverMe()
    {
        pointerEventData = new PointerEventData(eventSystem);
        pointerEventData.position = Input.mousePosition;

        //Create a list of Raycast Results
        List<RaycastResult> results = new List<RaycastResult>();

        //Raycast using the Graphics Raycaster and mouse click position
        raycaster.Raycast(pointerEventData, results);
        foreach(RaycastResult item in results)
            if (item.gameObject == gameObject) { return true; }
          
        return false;
    }
}
