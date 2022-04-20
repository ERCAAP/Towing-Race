using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;


public enum Axeles
{
    Front,
    Rear
}

[Serializable]
public struct Wheels
{
    public GameObject model;
    public WheelCollider collider;
    public Axeles axel;
}

public class TowedCar : MonoBehaviour
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
    private List<Wheels> wheels;

    private float inputX, inputY;

    private Rigidbody _rb;

    public bool N;
    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.centerOfMass = _centerOfMass;
    }


    private void Update()
    {
        AnimateWheels();
        GetInputs();
        // fark büyükse ve parmağını çektiyse eğer burdaki arabayı hareket ettir
    }

    private void LateUpdate()
    {
        Move();
        Turn();
    }

    private void GetInputs()
    {

        //   inputY = Input.GetAxis("Vertical");

    }

    private void Move()
    {
        foreach (var wheel in wheels)
        {
            wheel.collider.motorTorque = CarControl.ınstance.speed - 2 * maxAcceleration * 500 * Time.deltaTime;
        }
    }

    private void Turn()
    {
        foreach (var wheel in wheels)
        {
            if (wheel.axel == Axeles.Front)
            {
                var _steerAngle = inputX * turnSensitivity * maxSteerAngle;
                wheel.collider.steerAngle = Mathf.Lerp(wheel.collider.steerAngle, _steerAngle, 0.5f);
            }
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
        N = true;
        GameManager.Instance.PlayOnceParticle = false;
        foreach (var wheel in wheels)
        {
            wheel.collider.brakeTorque = 0;
        }
    }
    private void OnMouseDrag()
    {
        inputY = 4;
        Mathf.Clamp(inputY, 0, 5);
    }
    private void OnMouseUp()
    {
        foreach (var wheel in wheels)
        {
           // wheel.collider.brakeTorque = 3000000000;
        }
        N = false;
        GameManager.Instance.WarningParticle.Stop();
    }
}