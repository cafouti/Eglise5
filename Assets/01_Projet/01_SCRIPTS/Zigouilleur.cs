using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zigouilleur : MonoBehaviour
{
    //////////////////////Variables//////////////////////////
    public float speed;
    public float masse;
    private float gravity;
    private bool poursuite = false;
    private bool retour = false;
    private float offsetx = 0.2f;
    private bool droite;
    private bool startDroite = false;

    private GameObject character;
    private CharacterController controller;
    private Vector3 mouvement;
    private BoxCollider detectionZone;
    private Transform player;

    private Vector3 start;
    public Transform destination;

    ///////////////////////Initialisation/////////////////////
    void Start()
    {
        if (transform.localRotation.y == 0)
        {
            startDroite = true;
            droite = startDroite;
        }
        
        start = transform.position;        
        character = gameObject;
        controller = GetComponent<CharacterController>();
        detectionZone = GetComponent<BoxCollider>();
    }

    ///////////////////////Boucle/////////////////////
    void Update()
    {
        if (controller.isGrounded && mouvement.y < 0)
        {
            mouvement.y = masse;
        }        

        if (poursuite)
        {
            Poursuite();
        }
        else if(retour)
        {
            Retour();
            if(!retour)
            {
                mouvement.x = 0;
            }
        }
        else
        {
            mouvement.x = 0;
        }
        
        if((droite && mouvement.x < 0) || (!droite && mouvement.x > 0))
        {
            Rotate();
        }

        controller.Move(mouvement * Time.deltaTime);
    }
    
    /////////////////////////Methode classe//////////////////////////

    void Rotate()
    {
        character.transform.Rotate(0,180,0);
        droite = !droite;
    }

    void Poursuite()
    {
        if(player != null)
        {
            if (character.transform.position.x > player.transform.position.x)
            {
                mouvement.x = -speed;
            }
            else if (character.transform.position.x < player.transform.position.x)
            {
                mouvement.x = speed;
            }
        }        
    }

    void Retour()
    {
        if (start.x > transform.position.x)
        {
            mouvement.x = speed;
        }
        else if (start.x < transform.position.x)
        {
            mouvement.x = -speed;
        }

        if(-offsetx < start.x - transform.position.x && start.x - transform.position.x < offsetx)
        {
            if((droite && !startDroite) || (!droite && startDroite))
            {
                Rotate();
            }

            retour = false;
        }
    }

    /////////////////////////Trigger/////////////////////

    private void OnTriggerEnter(Collider other)
    {
        
        if(other.gameObject.name == "Fille(Clone)" || other.gameObject.name == "Monstre(Clone)")
        {
            player = other.gameObject.transform;
            poursuite = true;
            StopCoroutine("PerteVue");
            StopCoroutine("PerteAgro");
            retour = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "Fille(Clone)" || other.gameObject.name == "Monstre(Clone)")
        {
            StartCoroutine("PerteVue");
        }
    }

    //////////////////////Coroutine///////////////////////
    
    IEnumerator PerteVue()
    {
        yield return new WaitForSeconds(1);
        poursuite = false;
        player = null;
        StartCoroutine(PerteAgro());
    }

    IEnumerator PerteAgro()
    {
        yield return new WaitForSeconds(1);
        retour = true;
    }
}
