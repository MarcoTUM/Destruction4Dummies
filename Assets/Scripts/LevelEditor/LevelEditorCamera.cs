using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEditorCamera : MonoBehaviour
{
    [SerializeField] private float movementSpeed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float xMovement = Input.GetAxis(InputDictionary.Horizontal);
        float yMovement = Input.GetAxis(InputDictionary.Vertical);
        this.transform.Translate(new Vector3(xMovement, yMovement, 0) * movementSpeed * Time.deltaTime);
    }
}
