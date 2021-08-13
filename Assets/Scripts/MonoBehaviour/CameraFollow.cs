using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField]
    private Transform target;

    [SerializeField]
    private Vector3 offset = new Vector3(0f, 0f, 0f);

    [SerializeField]
    private float smoothSpeed = 0.125f;


    private void LateUpdate()
    {
        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        transform.position = desiredPosition;

        transform.eulerAngles = new Vector3(45f, target.eulerAngles.y, 0f);
       //transform.LookAt(target);
    }


}
