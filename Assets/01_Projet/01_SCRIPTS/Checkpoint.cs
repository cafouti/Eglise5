using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public Character character;

    public Zigouilleur[] zigouilleur;
    public PretreLumiere[] pretreLumiere;
    public GameObject gb;
    public Zigouilleur zig;

    private void Start()
    {
        zig = gb.GetComponent<Zigouilleur>();
    }

    void changeRespawn()
    {
        character.respawnPoint = transform;
        /*Zigouilleur death = zig; 
        gb = zig.Remake().gameObject;
        zig = gb.GetComponent<Zigouilleur>();
        zig.name = death.gameObject.name;
        zig.enabled = true;
        death.Suppr();*/
        ResetAssets();
    }

    void ResetAssets()
    {
        for(int i=0; i<zigouilleur.Length; i++)
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
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Fille(Clone)" || other.gameObject.name == "Monstre(Clone)")
        {
            changeRespawn();
        }
    }
}
