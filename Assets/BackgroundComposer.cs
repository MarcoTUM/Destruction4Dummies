using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundComposer : MonoBehaviour
{

    [SerializeField] private GameObject tile;
    [SerializeField] private Texture2D[] replaceTextures;
    public float forgroundConstant;
    private List<int> usedXCoords = new List<int>(), usedYCoords = new List<int>();
    public int height, width, heightPadding, widthPadding;

    // Start is called before the first frame update
    void Start()
    {

        //per tile
        for (int i = 0; i < replaceTextures.Length; i++)
        {
            Texture2D texture = replaceTextures[i];
            GameObject currentTile = Instantiate(tile, transform);
            currentTile.GetComponent<MeshRenderer>().material.SetTexture("_MainTex", texture);
            currentTile.transform.localPosition = new Vector3(PickXPosition(), forgroundConstant, PickYPosition());
        }
        tile.SetActive(false);
    }

    Texture2D PickTexture()
    {
        int index = Random.Range(0, replaceTextures.Length - 1);
        return replaceTextures[index];
    }

    private float PickXPosition()
    {
        int index = Random.Range(widthPadding, width - widthPadding);
        while (usedXCoords.Contains(index))
        {
            index = (index + 1) % (width - widthPadding);
            if (index == 0)
                index = widthPadding;
        }
        usedXCoords.Add(index);
        float intermediate = index * 0.5f;
        return intermediate -4.75f;
    }

    private float PickYPosition()
    {
        int index = Random.Range(heightPadding, height - heightPadding);

        while (usedYCoords.Contains(index))
        {
            index = (index + 1) % (height - heightPadding);
            if (index == 0)
                index = heightPadding;
        }
        usedYCoords.Add(index);
        float intermediate = index * 0.25f;
        return intermediate - 4.875f;
    }
}
