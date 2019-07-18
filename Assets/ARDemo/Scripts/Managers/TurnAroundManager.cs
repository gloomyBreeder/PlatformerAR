using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TurnAroundManager : BasicManager<TurnAroundManager>, IPointerClickHandler
{
    public Transform ObjectToTurn;

    public void OnPointerClick(PointerEventData eventData)
    {
        ObjectToTurn.eulerAngles = new Vector3(0, ObjectToTurn.eulerAngles.y + 90, 0);
    }
}
