using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HonkScript : MonoBehaviour
{
    public void Honk()
    {
        source.PlayOneShot(honk);
    }
    public AudioClip honk;
    public AudioSource source;
}
