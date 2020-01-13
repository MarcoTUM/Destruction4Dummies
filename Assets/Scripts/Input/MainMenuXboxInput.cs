using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MainMenuXboxInput : MonoBehaviour
{
    private enum Tab { Main, Custom };
    [SerializeField] private GameObject firstSelected;
    [SerializeField] private Transform mainLevels, customLevels;
    [SerializeField] private Button backTab;
    [SerializeField] private Toggle mainTab, customTab;
    [SerializeField] private GameObject[] controlTexts;
    private EventSystem eventSystem;
    private Tab currentTab;
    private bool isActive;
    private bool isInLevelSelection;
    private bool leftTriggerDown, rightTriggerDown;
    private GameObject levelSelectionButton;
    private GameObject lastSelection;

    private void Start()
    {
        isActive = Input.GetJoystickNames().Length > 0 && Input.GetJoystickNames()[0] != "";
        if (!isActive)
            return;
        eventSystem = EventSystem.current;
        eventSystem.firstSelectedGameObject = firstSelected;
    }

    private void Update()
    {
        if (!isActive)
            return;

        //Mouse should not remove focus from selection
        if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(2))
        {
            eventSystem.SetSelectedGameObject(lastSelection);
        }
        else if (eventSystem.currentSelectedGameObject != null)
            lastSelection = eventSystem.currentSelectedGameObject;

        //Levelselection Inputs
        if (isInLevelSelection)
        {
            if (Input.GetAxisRaw(InputDictionary.XboxLeftTrigger) == 0)
                leftTriggerDown = false;
            if (Input.GetAxisRaw(InputDictionary.XboxRightTrigger) == 0)
                rightTriggerDown = false;
            if (currentTab != Tab.Main && (Input.GetKeyDown(InputDictionary.XboxLeftBumper) || (!leftTriggerDown && Input.GetAxisRaw(InputDictionary.XboxLeftTrigger) > 0)))
            {
                leftTriggerDown = true;
                mainTab.OnPointerClick(new PointerEventData(eventSystem));
            }
            else if (currentTab != Tab.Custom && (Input.GetKeyDown(InputDictionary.XboxRightBumper) || (!rightTriggerDown && Input.GetAxisRaw(InputDictionary.XboxRightTrigger) > 0)))
            {
                rightTriggerDown = true;
                customTab.OnPointerClick(new PointerEventData(eventSystem));
            }

            if (Input.GetKeyDown(InputDictionary.XboxBButton))
            {
                backTab.OnPointerClick(new PointerEventData(eventSystem));
            }

        }
    }

    public void GoToMainTab()
    {
        if (!isActive)
            return;
        currentTab = Tab.Main;
        eventSystem.SetSelectedGameObject(mainLevels.GetChild(0).gameObject);
    }

    public void GoToCustomTab()
    {
        if (!isActive)
            return;
        currentTab = Tab.Custom;
        eventSystem.SetSelectedGameObject(customLevels.GetChild(0).gameObject);
    }

    public void CloseLevelSelection()
    {
        if (!isActive)
            return;
        eventSystem.SetSelectedGameObject(levelSelectionButton);
        isInLevelSelection = false;
    }

    public void OpenLevelSelection()
    {
        if (!isActive)
            return;
        if (levelSelectionButton == null)
        {
            levelSelectionButton = eventSystem.currentSelectedGameObject;
            currentTab = Tab.Main;
            foreach (GameObject text in controlTexts)
                text.SetActive(true);
        }
        isInLevelSelection = true;


        if (currentTab == Tab.Main)
            eventSystem.SetSelectedGameObject(mainLevels.GetChild(0).gameObject);
        else if (currentTab == Tab.Custom)
            eventSystem.SetSelectedGameObject(customLevels.GetChild(0).gameObject);
    }

}
