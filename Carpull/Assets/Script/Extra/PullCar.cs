using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PullCar : MonoBehaviour
{
    public static PullCar Instance { set; get; }
    public List<WheelCollider> Whells;

    public float poweraccumulated;
    public float speed;
    public float Maxspeed=25;
    public Vector3 CenterOfCar;
    private Rigidbody rb;
    bool _Stop;
    public bool _Pull;

    public float Timer;
    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        CenterOfCar = rb.centerOfMass;
        Instance = this;
    }
    private void FixedUpdate()
    {
        if (_Stop == true)
        {
            foreach (var whell in Whells)
            {
                whell.brakeTorque = 3000;
                whell.brakeTorque = 0;
            }
        }
    }
    private void OnMouseDown()
    {
        speed = 250;
        GameManager.Instance.PlayOnceParticle = false;  // elini ekrandan çekitğinid kırmızı uyarı beliriyor
        _Stop = false;
        rb.constraints = RigidbodyConstraints.None;
        rb.constraints = RigidbodyConstraints.FreezePositionX;

    }
    private void OnMouseDrag()
    {
        speed += 15 * Time.deltaTime;
        foreach (var whell in Whells)
        {
            whell.motorTorque = speed * Maxspeed * Time.deltaTime;
        }
        poweraccumulated += 1 * Time.deltaTime;
        GivePowerBehindCar();

        // belli bir değere geldikten sonra o kadar geitr
    }
    private void OnMouseUp()
    {
        //towedcar.Instance.PullTowed = false;
      //  towedcar.Instance.AccEnergy+=unusedEnergy();
        _Stop = true;
        rb.velocity = Vector3.zero;
        rb.constraints = RigidbodyConstraints.FreezePositionZ;
        GameManager.Instance.WarningParticle.Stop(); // elini ekrandan çekitğinid kırmızı uyarı duruyor 
    }
    public float unusedEnergy()
    {
        float Unenergy = accumulatEnergy();
        poweraccumulated =0;
        return Unenergy;

    }
    public float accumulatEnergy()
    {
        float Acc = poweraccumulated;
        return Acc;
    }
    public void GivePowerBehindCar()
    {
        while (poweraccumulated>=0.75f)
        {
           // towedcar.Instance.PullTowed = true;
           // towedcar.Instance.AccEnergy += 0.75f;
            poweraccumulated -= 0.75f;
        }

    }
}
