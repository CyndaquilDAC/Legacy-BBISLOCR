using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SwitchySwapperScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
      
    }

    public void Swap()
    {
        if (leftAnswer.text == answerOneString)
        {
            leftAnswer.text = answerTwoString;
            rightAnswer.text = answerOneString;
        }
        else if (leftAnswer.text == answerTwoString)
        {
            leftAnswer.text = answerOneString;
            rightAnswer.text = answerTwoString;
        }
        audioSource.PlayOneShot(beepSound);
    }

    public void Done()
    {
        if (leftAnswer.text == answerTwoString)
        {
            notebook.SetActive(true);
            audioSource.PlayOneShot(beepSound);
        }
        else
        {
            audioSource.PlayOneShot(tryAgain);
        }
    }

    public float answerOne;
    public float answerTwo;
    public TextMeshPro leftAnswer;
    public TextMeshPro rightAnswer;
    public string answerOneString;
    public string answerTwoString;
    public AudioSource audioSource;
    public AudioClip beepSound;
    public GameObject notebook;
    public AudioClip tryAgain;
}
