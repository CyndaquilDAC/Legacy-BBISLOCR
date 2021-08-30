using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchySwapperDoneScript : MonoBehaviour
{
   private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            sss.Done();
        }
    }
    public SwitchySwapperScript sss;
}
