using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FillinatorScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        valueNeededState = UnityEngine.Random.Range(0, 4);
        valueNeededString = valueNeededState + "/4";
        valueNeededText.text = valueNeededString;
    }

    // Update is called once per frame
    void Update()
    {
        fillinatorRenderer.material = fillLevels[Mathf.RoundToInt(fillinatorState)];
    }

    public void Add()
    {
        if (fillinatorState == 4)
        {
            
        }
        else
        {
            fillinatorState++;
        }
    }
    public void Subtract()
    {
        if (fillinatorState == 0)
        {

        }
        else
        {
            fillinatorState--;
        }
    }
    public void Done()
    {
        if(fillinatorState == valueNeededState)
        {
            audioSource.PlayOneShot(buzzSound);
            notebook.SetActive(true);
        }
        if (fillinatorState != valueNeededState)
        {
            audioSource.PlayOneShot(tryAgain);
        }
    }
    public TextMeshPro valueNeededText;
    public string valueNeededString;
    public float fillinatorState;
    public float valueNeededState;
    public GameObject notebook;
    public AudioSource audioSource;
    public AudioClip tryAgain;
    public AudioClip buzzSound;
    public Material[] fillLevels;
    public MeshRenderer fillinatorRenderer;

}
