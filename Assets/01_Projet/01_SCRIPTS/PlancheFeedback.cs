using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlancheFeedback : MonoBehaviour
{
    public GameObject touch;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("detection");
        if (other.gameObject.name == "Monstre(Clone)")
        {
            
            Debug.Log("ya");
        }
    }
}
