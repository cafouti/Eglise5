using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zigouilleur : MonoBehaviour
{
    //////////////////////Variables//////////////////////////
    public float speed;
    public float masse;
    public bool poursuite = false;
    public bool retour = false;
    private float offsetx = 0.2f;
    public bool droite;
    private bool startDroite = false;
    private bool canMove = true;

    private CharacterController controller;    
    private Vector3 mouvement;
    private BoxCollider detectionZone;
    private Transform player = null;

    private Vector3 start;
    private Quaternion startRot; 
    public Transform destination;

    private Animator animator;

    ///////////////////////Initialisation/////////////////////


    void Awake()
    {
        StopAllCoroutines();
    }
    void Start()
    {
        startDroite = droite;
        start = transform.position;
        startRot = transform.localRotation;
        controller = gameObject.GetComponent<CharacterController>();
        detectionZone = GetComponent<BoxCollider>();
        animator = GetComponent<Animator>();
        animator.speed = 1;
        mouvement.x = 0;
        poursuite = false;
        StopCoroutine("PerteVue");
        StopCoroutine("PerteAgro");
        StopAllCoroutines();
        animator.Play("EO1_Idle");
        player = null;
        Debug.Log("Respawn poursuite = " + poursuite);       
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
            
            if (!retour)
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

        if(canMove)
        {
            controller.Move(mouvement * Time.deltaTime);
        }
        //Debug.Log("retour = " + retour);
    }
    
    /////////////////////////Methode classe//////////////////////////

    void Rotate()
    {
        transform.Rotate(0,180,0);
        droite = !droite;
    }

    void Poursuite()
    {
        if(player != null)
        {
            if (transform.position.x > player.transform.position.x)
            {
                mouvement.x = -speed;
            }
            else if (transform.position.x < player.transform.position.x)
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
            animator.Play("EO1_Idle");
        }
    }

    public Zigouilleur Remake()
    {
        Zigouilleur newZig = Instantiate(this, start, startRot);
        return newZig;
    }

    public void Suppr()
    {
        Destroy(this.gameObject);
    }

    /////////////////////////Trigger/////////////////////

    private void OnTriggerEnter(Collider other)
    {        
        if(other.gameObject.name == "Fille(Clone)" || other.gameObject.name == "Monstre(Clone)")
        {
            if (!other.gameObject.GetComponentInParent<Character>().death)
            {
                player = other.gameObject.transform;
                poursuite = true;
                Debug.Log("nique ta race");
                StopCoroutine("PerteVue");
                StopCoroutine("PerteAgro");
                animator.Play("EO1_Course");
                retour = false;
            }                
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "Fille(Clone)" || other.gameObject.name == "Monstre(Clone)")
        {
            StartCoroutine("PerteVue");
        }
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.name == "Fille(Clone)" || hit.gameObject.name == "Monstre(Clone)")
        {
            hit.gameObject.GetComponentInParent<Character>().StartCoroutine("fadeIn");
            canMove = false;
            animator.speed = 0;
            //character.StartCoroutine("fadeIn");
        }
    }

    //////////////////////Coroutine///////////////////////
    
    IEnumerator PerteVue()
    {
        yield return new WaitForSeconds(1);
        Debug.Log("nique ta grande daronne");
        poursuite = false;
        player = null;
        StartCoroutine("PerteAgro");
    }

    IEnumerator PerteAgro()
    {
        animator.Play("EO1_Recherche");
        yield return new WaitForSeconds(3);
        animator.Play("EO1_Marche");
        retour = true;
    }
}
