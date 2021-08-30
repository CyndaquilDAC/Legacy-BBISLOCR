using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FillinatorMinusButtonScript : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        fs.Subtract();
    }
    public FillinatorScript fs;
}
