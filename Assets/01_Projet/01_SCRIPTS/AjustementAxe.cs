using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AjustementAxe : MonoBehaviour
{
    public Transform destination;
    public Character character;
    private bool move = false;
    private bool forward;
    public float speed;

    // Update is called once per frame
    void Update()
    {
        if(move)
        {
            Decale();
        }               
    }

    public void Decale()
    {
        //Application mouvement
        if (character.controller.transform.position.z <= destination.position.z && forward)
        {
            character.controller.Move(new Vector3(0, 0,speed) * Time.deltaTime);
            Debug.Log("moving forward");
        }
        else if(character.controller.transform.position.z >= destination.position.z && !forward)
        {
            character.controller.Move(new Vector3(0, 0, -speed) * Time.deltaTime);
            Debug.Log("moving backward");
        }
        else
        {
            move = false;
            Debug.Log("character already in good place");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "Fille(Clone)" || other.gameObject.name == "Monstre(Clone)")
        {
            move = true;
            if(character.controller.transform.position.z - destination.position.z < 0)
            {
                forward = true;
            }
            else if(character.controller.transform.position.z - destination.position.z < 0)
            {
                forward = false;
            }
            Debug.Log("joueur detecté");
        }
    }
}
