using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerSpoopMode : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        gc.ActivateSpoopMode();
    }
    public GameControllerScript gc;
}
