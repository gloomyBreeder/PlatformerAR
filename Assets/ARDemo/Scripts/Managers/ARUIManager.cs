using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ARUIManager : BasicManager<ARUIManager>, IPointerClickHandler
{
    [SerializeField]
    public List<Message> Messages = new List<Message>() { };
    public GameObject MessagePanel;
    public GameObject Gif;
    public GameObject TurnButton;
    public GameObject Lean;
    private PanelConditions _current;

    public enum PanelConditions
    {
        None,
        Search,
        Found,
        Task,
        TaskPinch,
        TaskContinue,
        HideAll,
        End,
        Lost,
        Turn
    }

    public PanelConditions GetCurrentCondition()
    {
        return _current;
    }
    public void ShowMessage(PanelConditions condition)
    {
        switch (condition)
        {
            case PanelConditions.Search:
                _current = PanelConditions.Search;
                break;
            case PanelConditions.Found:
                _current = PanelConditions.Found;
                break;
            case PanelConditions.Task:
                _current = PanelConditions.Task;
                break;
            case PanelConditions.TaskPinch:
                _current = PanelConditions.TaskPinch;
                Gif.SetActive(true);
                Lean.SetActive(true);
                break;
            case PanelConditions.Turn:
                //Gif.SetActive(false);
                TurnButton.SetActive(true);
                _current = PanelConditions.Turn;
                break;
            case PanelConditions.TaskContinue:
                _current = PanelConditions.TaskContinue;
                break;
            case PanelConditions.HideAll:
                _current = PanelConditions.HideAll;
                MessagePanel.SetActive(false);
                break;
            case PanelConditions.End:
                _current = PanelConditions.End;
                MessagePanel.SetActive(true);
                break;
            case PanelConditions.Lost:
                _current = PanelConditions.Lost;
                MessagePanel.SetActive(true);
                break;
        }
        MessagePanel.GetComponentInChildren<Text>().text = Messages.FirstOrDefault(mes => mes.condition == condition).Text;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (Gif != null)
            Gif.SetActive(false);

        if (_current == PanelConditions.Task)
            ShowMessage(PanelConditions.TaskPinch);
        else if (_current == PanelConditions.TaskPinch)
            ShowMessage(PanelConditions.Turn);
        else if (_current == PanelConditions.Turn)
            ShowMessage(PanelConditions.TaskContinue);
        else if (_current == PanelConditions.TaskContinue || _current == PanelConditions.End || _current == PanelConditions.Lost)
            ShowMessage(PanelConditions.HideAll);
        //Debug.Log("Clicked: " + eventData.pointerCurrentRaycast.gameObject.name);
    }
}
