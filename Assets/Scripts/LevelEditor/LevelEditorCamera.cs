using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class LevelEditorCamera : MonoBehaviour
{
    [SerializeField] private RectTransform menuBar, blockSelection;
    [SerializeField] private float keyboardSpeedMultiplier = 1.2f, mouseSpeedMultiplier = 1.5f;
    [SerializeField] private float zoomSpeed = 1f;
    [SerializeField] private float minZoom = 5, maxZoom = 15;

    private Level level;
    private Camera cam;

    private float uiLeftMargin, uiDownMargin;
    private bool mouseScroll = false;
    private Vector3 mouseScrollStart;

    private void Start()
    {
        level = Gamemaster.Instance.GetLevel();
        cam = this.GetComponent<Camera>();
        //I think this calculation is wrong but still works
        uiLeftMargin = Mathf.Abs(cam.ScreenToWorldPoint(new Vector3(0, 0, 0)).x - cam.ScreenToWorldPoint(new Vector3(menuBar.sizeDelta.x, 0, 0)).x);
        uiDownMargin = Mathf.Abs(cam.ScreenToWorldPoint(new Vector3(0, 0, 0)).y - cam.ScreenToWorldPoint(new Vector3(0, blockSelection.sizeDelta.y, 0)).y);
    }

    void Update()
    {
        //Zoom with mouseWheel
        cam.orthographicSize = Mathf.Clamp(cam.orthographicSize - Input.mouseScrollDelta.y * zoomSpeed, minZoom, maxZoom);

        //Get movement Values 
        float horizontal, vertical;
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
        else
        {
            horizontal = Input.GetAxisRaw(InputDictionary.Horizontal) * keyboardSpeedMultiplier;
            vertical = Input.GetAxisRaw(InputDictionary.Vertical) * keyboardSpeedMultiplier;
        }

        //Check for boundaries
        if (this.transform.position.x < 0)
            horizontal = Mathf.Max(horizontal, 0);
        else if (this.transform.position.x > level.GetWidth() * Block_Data.BlockSize - uiLeftMargin)
            horizontal = Mathf.Min(horizontal, 0);
        if (this.transform.position.y < 0)
            vertical = Mathf.Max(vertical, 0);
        else if (this.transform.position.y > level.GetHeight() * Block_Data.BlockSize - uiDownMargin)
            vertical = Mathf.Min(vertical, 0);

        //Translation
        this.transform.Translate(cam.orthographicSize * Time.deltaTime * new Vector3(horizontal, vertical, 0));
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
