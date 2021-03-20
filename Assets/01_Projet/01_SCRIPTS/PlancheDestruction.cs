using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlancheDestruction : MonoBehaviour
{
    public GameObject planche;
    private Vector3 start;
    private Quaternion startRot;

    private void Start()
    {
        start = transform.position;
        startRot = transform.localRotation;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Monstre(Clone)")
        {
            Destroy(planche);
        }
    }

    public GameObject Remake()
    {
        GameObject newZig = Instantiate(transform.parent.gameObject, start, startRot);
        return newZig;
    }

    public void Suppr()
    {
        Destroy(this.gameObject);
    }
}
