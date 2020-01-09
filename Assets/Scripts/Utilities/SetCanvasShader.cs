using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetCanvasShader : MonoBehaviour
{
    [SerializeField] private Shader shader;

    private void Awake()
    {
        if (shader)
            Canvas.GetDefaultCanvasMaterial().shader = shader;
    }
}
