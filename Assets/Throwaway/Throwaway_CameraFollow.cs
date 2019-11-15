using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throwaway_CameraFollow : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] private float smoothTime = .5f;
    [SerializeField] private float cameraDistance = 10f;

    private Vector3 velocity = Vector3.zero;

    
    // Update is called once per frame
    void Update()
    {
        SmoothDampToPosition(player.transform.position - cameraDistance * Vector3.forward);
    }

    private void SmoothDampToPosition(Vector3 targetPosition)
    {
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
    }
}
