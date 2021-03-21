using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public Character character;

    public Zigouilleur[] zigouilleur;
    public PretreLumiere[] pretreLumiere;
    public Vector3[] planche;
    public Quaternion[] plancheRot;
    public Vector3[] scalePlanche;
    public Vector3[] hitboxPlanche;
    public Quaternion[] hitboxPlancheRot;
    public Vector3[] hitboxPlancheScale;
    public GameObject[] gbPlanche;
    
    public GameObject planchePrefab;

    private void Start()
    {
        scalePlanche = new Vector3[gbPlanche.Length];
        planche = new Vector3[gbPlanche.Length];
        plancheRot = new Quaternion[gbPlanche.Length];
        hitboxPlanche = new Vector3[gbPlanche.Length];
        hitboxPlancheRot = new Quaternion[gbPlanche.Length];
        hitboxPlancheScale = new Vector3[gbPlanche.Length];

        for (int i = 0; i<gbPlanche.Length; i++)
        {
            scalePlanche[i] = gbPlanche[i].transform.localScale;
            planche[i] = gbPlanche[i].transform.position;
            plancheRot[i] = gbPlanche[i].transform.localRotation;
            hitboxPlanche[i] = gbPlanche[i].GetComponentInChildren<Transform>().position;
            hitboxPlancheRot[i] = gbPlanche[i].GetComponentInChildren<Transform>().localRotation;
            hitboxPlancheScale[i] = gbPlanche[i].GetComponentInChildren<Transform>().localScale;
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

        for (int i = 0; i < planche.Length; i++)
        {
            gbPlanche[i] = Instantiate(planchePrefab.gameObject, planche[i], plancheRot[i]);
            gbPlanche[i].transform.localScale = scalePlanche[i];
            gbPlanche[i].transform.localRotation = plancheRot[i];
            Transform hitboxTransform = gbPlanche[i].GetComponentInChildren<Transform>();
            hitboxTransform.position = hitboxPlanche[i];
            hitboxTransform.localRotation = hitboxPlancheRot[i];
            hitboxTransform.localScale = hitboxPlancheScale[i];
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
