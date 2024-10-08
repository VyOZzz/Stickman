using System.Collections.Generic;
using NUnit.Framework.Internal.Filters;
using UnityEngine;
using UnityEngine.EventSystems;

public class MobileTouchController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    public bool IsPointerOverNonActionButton()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if(touch.phase == TouchPhase.Began)
            {
                if (EventSystem.current.IsPointerOverGameObject(touch.fingerId))
                {
                    PointerEventData pointerData = new PointerEventData(EventSystem.current)
                    {
                        position = touch.position
                    };
                    List<RaycastResult> results = new List<RaycastResult>();
                    EventSystem.current.RaycastAll(pointerData, results);
                    foreach (RaycastResult result in results)
                    {
                        if(result.gameObject.CompareTag("Button"))
                        {
                            return true;
                        }
                    }

                    return false;
                }
            }
        }

        return false;
    }
}
