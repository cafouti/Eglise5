using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public Character character;

    public Zigouilleur[] zigouilleur;
    public PretreLumiere[] pretreLumiere;
    public PlancheDestruction[] planche;
    public GameObject[] gbPlanche;

    private void Start()
    {
        planche = new PlancheDestruction[gbPlanche.Length]; 
        for(int i = 0; i<gbPlanche.Length; i++)
        {
            planche[i] = gbPlanche[i].GetComponentInChildren(typeof(PlancheDestruction)) as PlancheDestruction;
        }
    }

    void changeRespawn()
    {
        character.respawnPoint = transform;
        character.checkpoint = this;
    }

    public void ResetAssets()
    {
        GameObject gb;

        for (int i=0; i<zigouilleur.Length; i++)
        {
            Zigouilleur death = zigouilleur[i];
            gb = zigouilleur[i].Remake().gameObject;
            zigouilleur[i] = gb.GetComponent<Zigouilleur>();
            zigouilleur[i].name = death.gameObject.name;
            zigouilleur[i].enabled = true;
            death.Suppr();
        }

        for(int i = 0; i < pretreLumiere.Length; i++)
        {
            PretreLumiere death = pretreLumiere[i];
            gb = pretreLumiere[i].Remake().gameObject;
            pretreLumiere[i] = gb.GetComponent<PretreLumiere>();
            pretreLumiere[i].name = death.gameObject.name;
            pretreLumiere[i].enabled = true;
            death.Suppr();
        }

        /*for (int i = 0; i < planche.Length; i++)
        {
            PlancheDestruction death = planche[i];            
            gb = planche[i].Remake();
            planche[i] = gb.GetComponent<PlancheDestruction>();
            planche[i].gameObject.name = death.gameObject.name;
            planche[i].enabled = true;
            death.Suppr();
        }*/
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Fille(Clone)" || other.gameObject.name == "Monstre(Clone)")
        {
            changeRespawn();
        }
    }
}
