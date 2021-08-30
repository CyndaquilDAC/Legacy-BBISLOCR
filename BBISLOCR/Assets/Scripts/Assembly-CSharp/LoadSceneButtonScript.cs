using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadSceneButtonScript : MonoBehaviour
{
    public void Button()
    {
        UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(Mathf.RoundToInt(sceneNumber));
    }
    public float sceneNumber;
}
