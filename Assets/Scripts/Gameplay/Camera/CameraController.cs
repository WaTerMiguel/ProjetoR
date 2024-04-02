using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform playerTarget;
    [SerializeField] private float smoothSpeed;

    private void FixedUpdate()
    {
        transform.position = Vector2.Lerp(transform.position, playerTarget.position, smoothSpeed / 100);

    }
}
