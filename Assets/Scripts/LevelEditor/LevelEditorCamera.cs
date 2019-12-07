using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEditorCamera : MonoBehaviour
{
    [SerializeField] 
    private float movementSpeed;

    // Distance from edge scrolling starts
    [SerializeField]
    private int boundary = 50;

    private const int LEFT = 12;
    private const int DOWN = 6;
    private const int RIGHT = 80;
    private const int UP = 38;

    void Update()
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
}
