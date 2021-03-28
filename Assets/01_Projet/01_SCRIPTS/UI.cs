using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    //Character
    public Character character;
    public Checkpoint start;

    //Menus
    public GameObject accueil;
    public GameObject pause;
    public GameObject fin;
    public RectTransform[] bandesNoir = new RectTransform[2]; 

    //Menus states
    public bool accueil_state;
    public bool pause_state;
    public bool fin_state;

    public GameObject[] buttonsGb = new GameObject[3];
    public bool[] buttons = new bool[3];
    public int buttonSelected;

    //Barre d'énergie
    public Image fondBarre;
    public Image energieBarre;
    public float transiAlpha;
    public float increment;
    private bool animBarre = true;

    // Start is called before the first frame update
    void Start()
    {
        accueil = GameObject.Find("Accueil");
        pause = GameObject.Find("Pause");
        fin = GameObject.Find("Fin");
        accueil_state = true;
        pause_state = false;
        fin_state = false;
        transiAlpha = 0;
        fondBarre.color = new Color(fondBarre.color.r, fondBarre.color.g, fondBarre.color.b, transiAlpha);
        energieBarre.color = new Color(energieBarre.color.r, energieBarre.color.g, energieBarre.color.b, transiAlpha);

        for (int i = 0; i<buttons.Length; i++)
        {
            buttons[i] = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Accueil();
        Pause();
        Fin();
    }

    void Accueil()
    {
        if (accueil_state)
        {
            accueil.SetActive(true);
        }
        else
        {
            accueil.SetActive(false);
            if (animBarre)
            {
                fondBarre.color = new Color(fondBarre.color.r, fondBarre.color.g, fondBarre.color.b, transiAlpha);
                energieBarre.color = new Color(energieBarre.color.r, energieBarre.color.g, energieBarre.color.b, transiAlpha);
                transiAlpha += increment * 2 * Time.deltaTime;

                if (transiAlpha < 0)
                {
                    transiAlpha = 0;
                    animBarre = false;
                }
            }
        }
    }

    void Pause()
    {
        if (pause_state)
        {
            if (!pause.activeSelf)
            {
                buttonSelected = 0;
                buttons[buttonSelected] = true;
            }
            pause.SetActive(true);
            Time.timeScale = 0;

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
                        pause_state = false;
                        break;
                    case 1:
                        pause_state = false;
                        character.respawnPoint = character.respawnPointStart;
                        character.checkpoint = character.respawnPointStart.GetComponentInParent(typeof(Checkpoint)) as Checkpoint;
                        Debug.Log("start = " + start);
                        character.StartCoroutine("fadeIn");
                        character.animator.speed = 0;
                        break;
                    case 2:
                        Application.Quit();
                        break;
                }
            }
        }
        else
        {
            pause.SetActive(false);
            Time.timeScale = 1;
        }
    }

    void Fin()
    {
        if (fin_state)
        {
            fin.SetActive(true);
        }
        else
        {
            fin.SetActive(false);
        }
    }

    IEnumerable gestionFin()
    {
        yield return new WaitForSeconds(2);
    }
}
