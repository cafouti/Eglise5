using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Fin : MonoBehaviour
{
    public UI ui;
    public Character character;
    public RectTransform[] bandesNoir = new RectTransform[2];
    public Text[] fin;
    public Image black;
    public bool animBarre = true;
    public bool anim = true;
    public bool anim2 = true;
    public bool anim3 = true;
    public float transiAlpha = 0;
    private float increment;

    private bool menu = true;
    public GameObject[] buttonsGb = new GameObject[2];
    public bool[] buttons = new bool[2];
    public int buttonSelected;
    public Image[] selection = new Image[2];

    public Image fondBarre;
    public Image energieBarre;
    private Color fdBarre;
    private Color enBarre;

    public int speedBarres;

    void Start()
    {
        increment = transiAlpha;
        for(int i = 0; i<buttonsGb.Length;i++)
        {
            buttonsGb[i].SetActive(false);
        }
        fdBarre = fondBarre.color;
        enBarre = energieBarre.color;
    }
    void Update()
    {
        if (ui.fin_state && anim)
        {
            if(transiAlpha == 0)
            {
                transiAlpha = 0.001f;
            }            
            moveBandes();
        }
        else if(anim2 && !anim)
        {
            fade();
        }
        else if(!anim2 && anim3)
        {
            apparitionMenu();
        }
        else if(!anim3 && menu)
        {
            gestionMenu();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        ui.fin_state = true;
        //StartCoroutine("bandesNoirs");
    }

    void moveBandes()
    {        
        if (anim)
        {
            bandesNoir[0].anchoredPosition += new Vector2(0, speedBarres) * Time.deltaTime;
            bandesNoir[1].anchoredPosition += new Vector2(0, -speedBarres) * Time.deltaTime;

            if (bandesNoir[0].anchoredPosition.y > -495)
            {
                anim = false;
                Debug.Log("fin move barres noir");
            }
        }

        if(animBarre)
        {
            fondBarre.color = new Color(fondBarre.color.r, fondBarre.color.g, fondBarre.color.b, transiAlpha);
            energieBarre.color = new Color(energieBarre.color.r,energieBarre.color.g,energieBarre.color.b,transiAlpha);
            transiAlpha -= increment*2 * Time.deltaTime;

            if (transiAlpha < 0)
            {
                transiAlpha = 0;
                animBarre = false;
            }
        }
    }
     
    void fade()
    {
        if(anim2)
        {
            black.color = new Color(0,0,0,transiAlpha);
            Debug.Log("transiAlpha = " + transiAlpha);
            transiAlpha += increment * Time.deltaTime;
            
            if(transiAlpha>=1)
            {
                transiAlpha = 0;
                anim2 = false;
            }

        }        
    }

    void apparitionMenu()
    {
        if(anim3)
        {
            for(int i =0; i<fin.Length; i++)
            {
                fin[i].color = new Color(1, 1, 1, transiAlpha);
                Debug.Log("transiAlpha = " + transiAlpha);
            }            
            transiAlpha += increment * Time.deltaTime;

            if (transiAlpha > 1)
            {
                anim3 = false;
            }
        }
    }

    void  gestionMenu()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            if (i == buttonSelected)
            {
                buttonsGb[i].SetActive(true);
            }
            else
            {
                buttonsGb[i].SetActive(false);
            }
        }

        if (Input.GetButtonDown("Haut"))
        {
            Debug.Log("navigation vers le bas");
            if (buttonSelected > 0)
            {
                buttonSelected--;
            }
        }
        else if (Input.GetButtonDown("Bas"))
        {
            Debug.Log("navigation vers le haut");
            if (buttonSelected < 2)
            {
                buttonSelected++;
            }
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            switch (buttonSelected)
            {
                case 0:
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                    break;
                case 1:
                    Application.Quit();
                    break;
            }
        }
    }
}
