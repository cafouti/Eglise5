using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCam : MonoBehaviour
{
    public Vector3 offset;
    public CameraFollow cam;

    void Start()
    {
        //offset = new Vector3(0.6f, 1.4f, -6f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "Fille(Clone)" || other.gameObject.name == "Monstre(Clone)")
        {
            Debug.Log("starfoula");
            cam.offset = offset;
        }
    }
}
