using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI : MonoBehaviour
{
    public GameObject accueil;
    public GameObject pause;
    public GameObject fin;

    public bool accueil_state;
    public bool pause_state;
    public bool fin_state;

    // Start is called before the first frame update
    void Start()
    {
        accueil = GameObject.FindWithTag("Accueil");
        pause = GameObject.FindWithTag("Pause");
        fin = GameObject.FindWithTag("Fin");
    }

    // Update is called once per frame
    void Update()
    {
        if(accueil_state)
        {
            accueil.SetActive(true);
        }
        else
        {
            accueil.SetActive(false);
        }

        if (pause_state)
        {
            pause.SetActive(true);
        }
        else
        {
            pause.SetActive(false);
        }

        if (fin_state)
        {
            fin.SetActive(true);
        }
        else
        {
            fin.SetActive(false);
        }
    }
}
