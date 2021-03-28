using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Ambience : MonoBehaviour
{
    public AudioMixer master;
    public AudioMixerSnapshot[] snapshots;
    public float[] weights;
    public AudioSource ambiance_cachauts;
    public AudioSource ambiance_couloir;
    public AudioSource ambiance_cours;
    public AudioSource ambiance_culte;

    private string state;

    // Start is called before the first frame update
    void Start()
    {
        Transi("cachauts");
    }

    public void Transi(string snap)
    {
        state = snap;
        switch(snap)
        {
            case "cachauts":
                weights[0] = 1;
                weights[1] = 0;
                weights[2] = 0;
                weights[3] = 0;
                weights[4] = 0;
                master.TransitionToSnapshots(snapshots, weights, 2);
                break;
            case "couloir":
                weights[0] = 0;
                weights[1] = 1;
                weights[2] = 0;
                weights[3] = 0;
                weights[4] = 0;
                master.TransitionToSnapshots(snapshots, weights, 2);
                break;
            case "cours":
                weights[0] = 0;
                weights[1] = 0;
                weights[2] = 1;
                weights[3] = 0;
                weights[4] = 0;
                master.TransitionToSnapshots(snapshots, weights, 2);
                break;
            case "culte":
                weights[0] = 0;
                weights[1] = 0;
                weights[2] = 0;
                weights[3] = 1;
                weights[4] = 0;
                master.TransitionToSnapshots(snapshots, weights, 2);
                break;
            case "fin":
                weights[0] = 0;
                weights[1] = 0;
                weights[2] = 0;
                weights[3] = 0;
                weights[4] = 1;
                master.TransitionToSnapshots(snapshots, weights, 2);
                break;
            case "anticipation":
                weights[0] = 0;
                weights[1] = 0;
                weights[2] = 0;
                weights[3] = 0;
                weights[4] = 1;
                master.TransitionToSnapshots(snapshots, weights, 2);
                break;
        }
    }
}
