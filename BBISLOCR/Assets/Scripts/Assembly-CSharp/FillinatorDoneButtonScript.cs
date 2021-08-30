using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FillinatorDoneButtonScript : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        fs.Done();
    }
    public FillinatorScript fs;
}
