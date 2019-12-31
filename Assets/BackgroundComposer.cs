using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundComposer : MonoBehaviour
{

    [SerializeField] private GameObject[] tiles;
    [SerializeField]private Texture2D[] replaceTextures;
    public float forgroundConstant;

    // Start is called before the first frame update
    void Start()
    {

        //per tile
        for (int i = 0; i < tiles.Length; i++)
        {
            //change the tile's texture
            Texture2D texture = pickTexture();
            tiles[i].GetComponent<MeshRenderer>().material.SetTexture("_MainTex",texture);
            //change the location of the tile
            //tiles[i].transform.localPosition = new Vector3(-2.75f, forgroundConstant, -3.25f);
            //tiles[i].transform.localPosition = new Vector3(Random.Range(-5,4) + 0.5f , forgroundConstant, -3.25f);
            tiles[i].transform.localPosition = new Vector3(Random.Range(-5, 4) + 0.5f, forgroundConstant, pickYPosition());
        }
    }

    Texture2D pickTexture()
    {
        int index = Random.Range(0, replaceTextures.Length - 1);
        return replaceTextures[index];
    }

    private float pickYPosition()
    {
        int index = Random.Range(0, 19);
        float intermediate = index * 0.5f;
        return intermediate - 4.75f;
    }
}
