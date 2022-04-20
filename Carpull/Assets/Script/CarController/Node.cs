using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    public Transform Obi;
    public float speed;
    private void FixedUpdate()
    {
             // vites ile takip etsin belli bir limite kadar
        if (GameManager.Instance.RopeBroke == false)
        {
            transform.position = new Vector3(transform.position.x,
            transform.position.y, Mathf.Lerp(transform.position.z, Obi.transform.position.z - 6.5f, Time.deltaTime * speed));
        }
    }
}
