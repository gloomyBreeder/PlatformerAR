using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class FixedJoystickButton : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
{
    private bool _pressed;

    public void OnPointerDown(PointerEventData eventData)
    {
        _pressed = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _pressed = false;
    }

    public bool IsPressed()
    {
        return _pressed;
    }
}
