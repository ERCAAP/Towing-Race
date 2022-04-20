using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Obi;
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { set; get; }
    [Header("RopeGrup")]
    public float Distance;
    public float ColorChangeSpeed;
    public GameObject[] Car;
    public MeshRenderer Rope;
    public bool RopeBroke;
    public GameObject RobeKepper;
    public ObiRope rope;
    [Header("ParticleGrup")]
    public Material AlertRed;
    public ParticleSystem WarningParticle;
    public bool PlayOnceParticle;
    public float Strain;
    public float Resting;
    public int RobeCount;
    private void Start()
    {

        // son ip ile ilk ip arasındaki far 4 olduğu zaman halat birazdan kopucak diye ikaz ver
        Instance = this;

        Resting = rope.CalculateLength(); // maksimum dayanacagı mıktar
        RobeCount = rope.activeParticleCount; // aktif particle sayısı


    }
    private void Update()
    {
        Strain = rope.CalculateLength() / rope.restLength;
        ColorChange();
    }
    public float CheckRobeCount() // halatın aktıf partıcle sayısını kontorl ediyor
    {
        float robecount = rope.activeParticleCount;
        return robecount;
    }
    public Color RopeSelfColor()
    {
       Color mat= Rope.GetComponent<MeshRenderer>().material.color;
        return mat;
    }
    public void ColorChange()
    {
        if (CheckRobeCount() > RobeCount)
        {
            RopeBroke = true;
            RobeKepper.transform.parent = (null);
            RobeKepper.GetComponent<Rigidbody>().isKinematic = false;
        }
        /*
        if (Distance > 7.5F)
        {
            Rope.material.color = Color.Lerp(RopeSelfColor(), AlertRed.color, ColorChangeSpeed *Time.deltaTime);  
        }
        if (Distance < 7.5F)
        {
            Rope.material.color = Color.Lerp(RopeSelfColor(), Color.white, 2 * Time.deltaTime);    
        }
        if (Distance >21.75f)
        {
            public int GetLeng;
         GetLeng = rope.elements.Count; // halatın listesi
            rope.Tear(rope.elements[GetLeng - 1]);
            rope.RebuildConstraintsFromElements();
               
        }
        if (Distance > 14 && PlayOnceParticle == false )
        {
            PlayOnceParticle = true;
            WarningParticle.Play();
        }
        */
    }


}
