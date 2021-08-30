using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TemperatureControllerScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(TempValueUpdater());
    }

    // Update is called once per frame
    void Update()
    {
        temperatureText.text = temperatureValue + " Degrees";
        if (temperatureValue == 120 || temperatureValue == 0)
        {
            UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(gameoverSceneNumber);
        }
        if(Input.GetKeyDown(KeyCode.F))
        {
            if(acOn)
            {
                acOn = false;
                acPlayer.volume = 0;
                acStateText.text = "AC is OFF. \nPress F to turn it ON.";
            }
            else if (!acOn)
            {
                acOn = true;
                acPlayer.volume = 1;
                acStateText.text = "AC is ON. \nPress F to turn it OFF.";
            }
        }
        acPowerText.text = acPower + "% Battery Power";
    }

    public IEnumerator TempValueUpdater()
    {
        if(acOn)
        {
            yield return new WaitForSeconds(1f);
            temperatureValue = temperatureValue - 1;
            acPower = acPower - 1;

        }
        if (!acOn)
        {
            yield return new WaitForSeconds(1f);
            temperatureValue = temperatureValue + 1;
        }
        StartCoroutine(TempValueUpdater());
        yield break;
    }

    public TextMeshProUGUI temperatureText;
    public float temperatureValue;
    public GameControllerScript gc;
    public int gameoverSceneNumber;
    public bool acOn;
    public int acPower;
    public TextMeshProUGUI acPowerText;
    public TextMeshProUGUI acStateText;
    public AudioSource acPlayer;
}
