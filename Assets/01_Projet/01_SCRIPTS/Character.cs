using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Character : MonoBehaviour
{
    private bool monstreEtat = false;
    public Vector3 mouvement;

    public float speed;
    private float saut;
    private float masse;
    private float gravity;
    private float energie = 0;
    public float increment = 0.01f;
    public int nbPushMax = 30;
    private int nbPush = 0;
    public bool jumping = false;
    public bool canTrans = true;

    public float speedF;
    public float sautF;
    public float masseF;
    public float gravityF;

    public float speedM;
    public float sautM;
    public float masseM;
    public float gravityM;

    private GameObject character;
    public CharacterController controller;
    public CameraFollow cam;
    public RectTransform curseur;
    private Vector2 xOffsetCurseur;

    public float fadeTime;
    public Image blackFade;
    public Transform respawnPoint;
    public Transform respawnPointStart;
    private bool death = false;

    public GameObject gbF;
    public GameObject gbM;

    private float[] fille;
    private float[] monstre;

    /////////////////////////////////////////////////////////////////////////////////////////////

    // Start is called before the first frame update
    void Start()
    {
        xOffsetCurseur = curseur.anchoredPosition ;
        fille = new float[4] { speedF, sautF, masseF, gravityF };
        monstre = new float[4] { speedM, sautM, masseM, gravityM };
        ChangeGB(gbF, 0, fille);
        energie = 1;
        Vector2 move = new Vector2(energie, 0);
        curseur.anchoredPosition = xOffsetCurseur + 75 * move;
        StartCoroutine("fadeOut");
    }

    // Update is called once per frame
    void Update()
    {
        //Recuperation de la valeur de la touche de deplacement
        float x = Input.GetAxis("Horizontal");

        if(!death)
        {
            mouvement.x = x * speed;

            //Maintien au sol
            if (controller.isGrounded && mouvement.y < 0)
            {
                mouvement.y = masse;
            }

            //Gestion saut
            if (jumping)
            {
                EndJump();
            }
            Jump();

            //Gestion orientation perso
            Rotate();

            //Gestion transformation
            if (canTrans)
            {
                Transformation();
                Energie();
            }                

            //Gestion gravité
            mouvement.y += gravity * Time.deltaTime;

            //Application mouvement
            controller.Move(mouvement * Time.deltaTime);
        }

        if (Input.GetKeyDown(KeyCode.Keypad0) && !death)
        {            
            StartCoroutine("fadeIn");
        }      
    }

    /////////////////////////////////////////////////////////////////////////////////////////////

    void Transformation()
    {
        if (Input.GetButtonDown("Transformation") && monstreEtat)
        {
            energie = 1;
            nbPush = nbPushMax;
        }
        else if(Input.GetButtonDown("Transformation") && !monstreEtat && nbPush>0)
        {
            if(energie + increment * 5 * nbPush >=1)
            {
                energie = 1;
            }
            else
            {
                energie += increment * 5 * nbPush;
            }
            nbPush -= 1;
        }
    }

    void ChangeGB(GameObject gb, float decalage, float[] stats)
    {
        Vector3 spawn;

        if (!character)
        {
            spawn = transform.position + new Vector3(0, decalage, 0);
        }
        else
        {
            spawn = character.transform.position + new Vector3(0, decalage, 0);
        }

        Destroy(character);
        character = Instantiate(gb, spawn, Quaternion.identity, transform);
        controller = character.GetComponent<CharacterController>();
        cam.joueur = character.transform;
        speed = stats[0];
        saut = stats[1];
        masse = stats[2];
        gravity = stats[3];
    }

    void Jump()
    {
        if (Input.GetButtonDown("Jump") && controller.isGrounded)
        {
            mouvement.y = Mathf.Sqrt(saut * -2 * gravity);
            jumping = true;
        }
    }

    public void CancelJump()
    {
        mouvement.y = gravity * Time.deltaTime;
    }

    public void EndJump()
    {
        if (controller.isGrounded)
        {
            this.jumping = false;
        }
    }

    void Rotate()
    {
        if (cam.droite && mouvement.x < 0)
        {
            character.transform.Rotate(0, 180, 0);
        }
        else if (!cam.droite && mouvement.x > 0)
        {
            character.transform.Rotate(0, -180, 0);
        }
    }

    void Energie()
    {
        if(!monstreEtat)
        {
            energie -= increment * Time.deltaTime;
        }        
        
        Vector2 move = new Vector2(energie, 0);
        curseur.anchoredPosition = xOffsetCurseur + 75 * move;

        if (energie <= 0 && !monstreEtat)
        {
            ChangeGB(gbM, 0.8f, monstre);
            monstreEtat = true;

        }
        else if (energie > 0 && monstreEtat)
        {
            ChangeGB(gbF, -0.6f, fille);
            monstreEtat = false;
        }
    }

    IEnumerator fadeOut()
    {
        blackFade.CrossFadeAlpha(0,fadeTime,false);
        yield return new WaitForSeconds(fadeTime);
        death = false;
    }

    IEnumerator fadeIn()
    {
        death = true;
        blackFade.CrossFadeAlpha(1,fadeTime,false);
        yield return new WaitForSeconds(fadeTime*1.5f);
        Respawn(respawnPoint);
        StartCoroutine("fadeOut");
    }

    void Respawn(Transform destination)
    {
        controller.transform.position = destination.position;
    }
}
