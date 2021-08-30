using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FillinatorAddButtonScript : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        fs.Add();
    }
    public FillinatorScript fs;
}
