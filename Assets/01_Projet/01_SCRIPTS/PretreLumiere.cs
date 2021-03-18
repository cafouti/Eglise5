using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PretreLumiere : MonoBehaviour
{
    public float rotationSpeed;
    public float angleMax;
    public float angleVision;
    private float y = 0;
    public float initRotY;
    private float rotY;
    public Transform lookUpStart;
    public Transform lookUpStop;
    public Transform lookDownStart;
    public Transform lookDownStop;
    public float porte;

    // Start is called before the first frame update
    void Start()
    {

    }

    void FixedUpdate()
    {
        Detection();
        Rotate();        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Rotate()
    {
        if (y > angleMax || y < -angleMax)
        {
            rotationSpeed *= -1;
        }

        y += Time.deltaTime * rotationSpeed;
        transform.localRotation = Quaternion.Euler(0, initRotY + y, 0);
    }

    void Detection()
    {
        for (float i = -1; i <= 1; i++)
        {
            lookUpStart.transform.localRotation = Quaternion.Euler(0, i * angleVision + initRotY, 0);
            Debug.DrawRay(lookUpStart.position, lookUpStart.transform.TransformDirection(Vector3.forward) * porte, Color.yellow);            
            lookDownStart.transform.localRotation = Quaternion.Euler(0, i * angleVision + initRotY, 0);
            Debug.DrawRay(lookDownStart.position, lookDownStart.transform.TransformDirection(Vector3.forward) * porte, Color.yellow);

            RaycastHit hit;

            // Does the ray intersect any objects excluding the player layer
            if (Physics.Raycast(lookDownStart.transform.position, lookDownStart.transform.TransformDirection(Vector3.forward), out hit, porte))
            {
                if (hit.collider.name == "Fille(Clone)" || hit.collider.name == "Monstre(Clone)")
                {
                    Debug.Log("juste master du code");
                }
            }

            if (Physics.Raycast(lookUpStart.transform.position, lookUpStart.transform.TransformDirection(Vector3.forward), out hit, porte))
            {
                if (hit.collider.name == "Fille(Clone)" || hit.collider.name == "Monstre(Clone)")
                {
                    Debug.Log("juste master du code");
                }
            }
        }
    }
}



