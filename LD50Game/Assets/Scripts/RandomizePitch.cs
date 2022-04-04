using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class RandomizePitch : MonoBehaviour
{
    public float maxPitch = 1;
    public float minPitch = 1;

    AudioSource source;

    // Start is called before the first frame update
    void Awake()
    {
        source = GetComponent<AudioSource>();
        source.pitch = Random.Range(minPitch, maxPitch);
        source.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
