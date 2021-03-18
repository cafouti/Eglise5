using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public Character character;

    void changeRespawn()
    {
        character.respawnPoint = transform;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Fille(Clone)" || other.gameObject.name == "Monstre(Clone)")
        {
            changeRespawn();
        }
    }
}
