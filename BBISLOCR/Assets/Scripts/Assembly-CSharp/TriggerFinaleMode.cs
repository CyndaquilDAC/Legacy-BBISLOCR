using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerFinaleMode : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        gc.ActivateFinaleMode();
    }
    public GameControllerScript gc;
}
