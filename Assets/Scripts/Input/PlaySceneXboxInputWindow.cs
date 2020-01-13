using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlaySceneXboxInputWindow : MonoBehaviour
{

    private EventSystem eventSystem;
    private bool isActive;
    [SerializeField] private GameObject firstSelection;
    private GameObject lastSelection;

    private void OnEnable()
    {
        isActive = Input.GetJoystickNames().Length > 0 && Input.GetJoystickNames()[0] != "";
        if (!isActive)
        {
            return;
        }
            
        eventSystem = EventSystem.current;
        eventSystem.SetSelectedGameObject(firstSelection);
    }

    private void Update()
    {
        if (!isActive)
        {
            return;
        }
        //Mouse should not remove focus from selection
        if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(2))
        {
            eventSystem.SetSelectedGameObject(lastSelection);
        }
        else if (eventSystem.currentSelectedGameObject != null)
            lastSelection = eventSystem.currentSelectedGameObject;
    }
}
