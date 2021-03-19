using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform joueur;
    public Vector3 offset;
    public Vector3 offsetGeneral;
    
    public float vitesse;
    public bool droite = true;
    //public bool inTunnel = false;

    void Start()
    {
        offset = offsetGeneral;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 positionFutur = joueur.position + offset;
        Vector3 positionTransit = Vector3.Lerp(transform.position, positionFutur, vitesse*Time.deltaTime) ;
        transform.position = positionTransit;
        Debug.Log("Transform = " + (joueur.position.x + offset.x));
    }    

    public void ChangeOffset(Vector3 newOffset)
    {
        offset = newOffset;
    }

    public void DefaultOffset()
    {
        offset = offsetGeneral;
    }
}
