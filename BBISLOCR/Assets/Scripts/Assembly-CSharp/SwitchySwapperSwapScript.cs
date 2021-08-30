using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchySwapperSwapScript : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            sss.Swap();
        }
    }
    public SwitchySwapperScript sss;
}
