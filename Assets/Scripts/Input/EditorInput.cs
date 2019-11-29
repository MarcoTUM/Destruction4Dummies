using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class EditorInput : MonoBehaviour
{
    private new Camera camera;
    int layerMask;

    private void Awake()
    {
        layerMask = LayerMask.GetMask(LayerDictionary.Block);
        camera = Camera.main;
    }

    /// <summary>
    /// Casts ray from mouse and tests if it hits a block
    /// </summary>
    /// <returns>Coordinate of block hit in grid; if nothing is hit returns (-1,-1)</returns>
    public Vector2Int GetBlockMouseIsOn()
    {
        if (Input.GetMouseButton(0))
        {
            RaycastHit hit;
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, layerMask))
            {
                return BlockCoordinateFromName(hit.transform.gameObject.name);
            }
        }
        return new Vector2Int(-1,-1);
    }

    private Vector2Int BlockCoordinateFromName(string name)
    {
        string[] numbers = Regex.Split(name, @"\D+");
        int x = int.Parse(numbers[1]);
        int y = int.Parse(numbers[2]);
        return new Vector2Int(x, y);
    }
}
