using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtrasUnachievableEndingScript : MonoBehaviour
{
    public void SecretEnding()
    {
        UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(5);
    }

    public void TestroomEnding()
    {
        UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(6);
    }
}
