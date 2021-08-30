using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RandomizedWarnTextScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnEnable()
    {
        warnText.text = possibleText[UnityEngine.Random.Range(0, possibleText.Length)];
    }

    public TextMeshProUGUI warnText;
    private string[] possibleText = new string[]
    {
        "No running out of stamina in the halls!",
        "Slow down!",
        "Stop and breathe for a while!",
        "There's lots of content here. Slow down and take a closer look at it.",
        "You need rest!",
        "Current Stamina: 0",
        "No need to run from me! Hahahaha!",
        "I get angrier for every problem you get wrong! So don't do that! Haha!"
    };

}
