using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PointsTextScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        pointsText.text = PlayerPrefs.GetFloat("ShopPoints").ToString() + " Points";
    }

    // Update is called once per frame
    void Update()
    {
        pointsText.text = PlayerPrefs.GetFloat("ShopPoints").ToString() + " Points";
    }
    public TextMeshProUGUI pointsText;
}
