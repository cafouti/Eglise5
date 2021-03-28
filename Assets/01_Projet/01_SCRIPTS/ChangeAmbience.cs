using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeAmbience : MonoBehaviour
{
    public Ambience mix;
    public string ambience;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Fille(Clone)" || other.gameObject.name == "Monstre(Clone)")
        {
            mix.Transi(ambience);
        }
    }
}
