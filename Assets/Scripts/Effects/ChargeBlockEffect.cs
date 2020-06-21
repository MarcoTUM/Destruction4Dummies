using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeBlockEffect : MonoBehaviour
{
    private List<Block> blocks = new List<Block>();

    private void ClearBlocks()
    {
        foreach (Block block in blocks)
        {
            block.GetComponent<Renderer>().material.color *= 2f;
        }
        blocks.Clear();
    }

    private void RemoveBlock(Block block)
    {
        if (blocks.Remove(block))
            block.GetComponent<Renderer>().material.color *= 2f;
    }

    private void AddBlock(Block block)
    {
        if (!blocks.Contains(block))
        {
            block.GetComponent<Renderer>().material.color /= 2f;
            blocks.Add(block);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Block block = other.GetComponent<Block>();
        if (block != null)
        {
            AddBlock(block);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Block block = other.GetComponent<Block>();
        if (block != null)
        {
            RemoveBlock(block);
        }
    }

    private void OnDisable()
    {
        ClearBlocks();
    }
}
