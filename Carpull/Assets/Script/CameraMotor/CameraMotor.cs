using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMotor : MonoBehaviour
{
    public Transform LookAt;
    public Vector3 offset;
    public Vector3 rot;
    public float speed;
 
    private void LateUpdate()
    {
        Vector3 desiredPos = LookAt.transform.position + offset;
        transform.position = Vector3.Lerp(transform.position, desiredPos, speed * Time.deltaTime);
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(rot), speed * Time.deltaTime);
    }
}
