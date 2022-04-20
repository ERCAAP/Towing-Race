using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;


public enum Axel
{
    Front,
    Rear
}

[Serializable]
public struct Wheel
{
    public GameObject model;
    public WheelCollider collider;
    public Axel axel;
}

public class CarControl : MonoBehaviour
{

    [SerializeField]
    private float maxAcceleration = 20.0f;
    [SerializeField]
    private float turnSensitivity = 1.0f;
    [SerializeField]
    private float maxSteerAngle = 45.0f;
    [SerializeField]
    private Vector3 _centerOfMass;
    [SerializeField]
    private List<Wheel> wheels;

    private Rigidbody _rb;
    public float speed;
    public static CarControl ınstance { set; get; }
    private void Start()
    {
        ınstance = this;
        _rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        AnimateWheels();
    }

    private void FixedUpdate()
    {
         _rb.centerOfMass = _centerOfMass;
        Move();
    }

    private void Move()
    {
        foreach (var wheel in wheels)
        {
            // eğer arabanın onu 35 derece kalkık ise arka tekerlere daha fazla guc ver on tekerlere daha az ve sanıyede
            // bir artan hızı düşür + 1 değilde 0.25f olarak artırr
            wheel.collider.motorTorque = speed * maxAcceleration * 500 * Time.deltaTime;
        }
    }
    private void AnimateWheels()
    {
        foreach (var wheel in wheels)
        {
            Quaternion _rot;
            Vector3 _pos;
            wheel.collider.GetWorldPose(out _pos, out _rot);
            wheel.model.transform.position = _pos;
            wheel.model.transform.rotation = _rot;
        }
    }

    private void OnMouseDown()
    {
        speed = 20;
        GameManager.Instance.PlayOnceParticle = false;
        foreach (var wheel in wheels)
        {
            wheel.collider.brakeTorque = 0;
        }
    }

    private void OnMouseDrag()
    {
          speed += 1 * Time.deltaTime;
           Mathf.Clamp(speed, 0, 25);
    }
    private void OnMouseUp()
    {
        foreach (var wheel in wheels)
        {
            wheel.collider.brakeTorque = 3000000;
        }
        GameManager.Instance.WarningParticle.Stop();
    }
 
}