using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Camera))]
public class LevelEditorCamera : MonoBehaviour
{
    [SerializeField] private InputField nameInputField;
    [SerializeField] private RectTransform canvas, menuBar, blockSelection;
    [SerializeField] private float keyboardSpeedMultiplier = 1.2f, mouseSpeedMultiplier = 1.5f;
    [SerializeField] private float zoomSpeed = 1f;
    [SerializeField] private float minZoom = 5;
    private float maxZoom = 15;

    private Level level;
    private Camera cam;

    private bool mouseScroll = false;
    private Vector3 mouseScrollStart;
    [SerializeField] private float startMargin = 1.15f;

    private void Start()
    {
        level = Gamemaster.Instance.GetLevel();
        cam = this.GetComponent<Camera>();
    }

    void Update()
    {
        //Zoom with mouseWheel
        if(!EventSystem.current.IsPointerOverGameObject())
            cam.orthographicSize = Mathf.Clamp(cam.orthographicSize - Input.mouseScrollDelta.y * zoomSpeed, minZoom, maxZoom);

        //Get movement Values 
        float horizontal = 0, vertical = 0;
        if (Input.GetMouseButtonDown(InputDictionary.MouseRightClick))
        {
            mouseScroll = true;
            mouseScrollStart = Input.mousePosition;
        }
        else if (Input.GetMouseButtonUp(InputDictionary.MouseRightClick))
        {
            mouseScroll = false;
        }

        if (mouseScroll)
        {
            horizontal = (Input.mousePosition.x - mouseScrollStart.x) / Screen.width * mouseSpeedMultiplier;
            vertical = (Input.mousePosition.y - mouseScrollStart.y) / Screen.height * mouseSpeedMultiplier;
        }
        else if(!nameInputField.isFocused)
        {
            horizontal = Input.GetAxisRaw(InputDictionary.Horizontal) * keyboardSpeedMultiplier;
            vertical = Input.GetAxisRaw(InputDictionary.Vertical) * keyboardSpeedMultiplier;
        }
        //Check for boundaries
        if (this.transform.position.x < 0)
            horizontal = Mathf.Max(horizontal, 0);
        else if (this.transform.position.x > level.GetWorldWidth())
            horizontal = Mathf.Min(horizontal, 0);
        if (this.transform.position.y < 0)
            vertical = Mathf.Max(vertical, 0);
        else if (this.transform.position.y > level.GetWorldHeight())
            vertical = Mathf.Min(vertical, 0);

        //Translation
        this.transform.Translate(cam.orthographicSize * Time.deltaTime * new Vector3(horizontal, vertical, 0));

    }

    public void InitializeCamera()
    {
        float levelWidth = level.GetWorldWidth();
        float levelHeight = level.GetWorldHeight();

        if (levelWidth > levelHeight * cam.aspect)
        {
            cam.orthographicSize = levelWidth * startMargin / cam.aspect / 2f;
        }
        else
        {
            cam.orthographicSize = levelHeight * startMargin / 2f;
        }
        maxZoom = cam.orthographicSize;
        this.transform.position = new Vector3(levelWidth / 2f, levelHeight / 2f, this.transform.position.z);

    }
    /*
    private float movementSpeed;
    private const int LEFT = 12;
    private const int DOWN = 6;
    private const int RIGHT = 80;
    private const int UP = 38;
    
    // Distance from edge scrolling starts
    [SerializeField] private int boundary = 50;
    private void OldMovement()
    {

        if (Input.mousePosition.x > Screen.width - boundary)
        {
            // Move on +X axis
            if (transform.position.x < RIGHT)
                this.transform.Translate(new Vector3(1, 0, 0) * movementSpeed * Time.deltaTime);
            else
                transform.position.Set(RIGHT, transform.position.y, transform.position.z);
        }
        if (Input.mousePosition.x < 0 + boundary)
        {
            // Move on -X axis
            if (transform.position.x > LEFT)
                this.transform.Translate(new Vector3(-1, 0, 0) * movementSpeed * Time.deltaTime);
            else
                transform.position.Set(LEFT, transform.position.y, transform.position.z);
        }
        if (Input.mousePosition.y > Screen.height - boundary)
        {
            // Move on +Y axis
            if (transform.position.y < UP)
                this.transform.Translate(new Vector3(0, 1, 0) * movementSpeed * Time.deltaTime);
            else
                transform.position.Set(transform.position.x, UP, transform.position.z);
        }
        if (Input.mousePosition.y < 0 + boundary)
        {
            // Move on -Y axis
            if (transform.position.y > DOWN)
                this.transform.Translate(new Vector3(0, -1, 0) * movementSpeed * Time.deltaTime);
            else
                transform.position.Set(transform.position.x, DOWN, transform.position.z);
        }
    }
    */
}
