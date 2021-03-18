using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tunnel : MonoBehaviour
{
    public CameraFollow cam;
    public Character character;
    private float initSpeed = 0;
    public float crouchSpeed;
    public Vector3 offset;
    private Vector3 previousOffset;

    void Start()
    {
        //offset = new Vector3(0, 1.5f, -4f);
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "Fille(Clone)")
        {
            previousOffset = cam.offset;
            Debug.Log("previousOffset = " + previousOffset);

            if(initSpeed == 0)
            {
                initSpeed = character.speed;
            }

            cam.offset = offset;
            initSpeed = character.speed;
            character.speed = crouchSpeed;
            character.canTrans = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.name == "Fille(Clone)")
        {
            character.speed = initSpeed;
            character.canTrans = true;
            cam.offset = previousOffset;
            Debug.Log("previousOffset = " + previousOffset + "; offset = " + offset);
        }
    }
}
