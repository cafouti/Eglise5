using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform joueur;
    public Vector3 offset;
    public Vector3 offsetGeneral;
    public Vector3 offsetTunnel;
    
    public float vitesse;
    public bool droite = true;
    //public bool inTunnel = false;

    void Start()
    {
        offsetGeneral = new Vector3(0.6f, 1.4f, -4); //new Vector3(0.5f, 0.6f, -3)
        offset = offsetGeneral;
        offsetTunnel = new Vector3(2,1.4f,-2f);
    }

    // Update is called once per frame
    void Update()
    {
        //Tunnel(inTunnel);      
        Vector3 positionFutur = joueur.position + offset ;
        Vector3 positionTransit = Vector3.Lerp(transform.position, positionFutur, vitesse*Time.deltaTime) ;
        transform.position = positionTransit;
    }    

    void Tunnel(bool inTunnel)
    {
        if(inTunnel)
        {
            offset = offsetTunnel;
        }
        else
        {
            offset = offsetGeneral;
        }
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
