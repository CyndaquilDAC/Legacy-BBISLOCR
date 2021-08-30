using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IfNoPointsSet0Script : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if(!PlayerPrefs.HasKey("ShopPoints"))
        {
            PlayerPrefs.SetFloat("ShopPoints", 0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!PlayerPrefs.HasKey("ShopPoints"))
        {
            PlayerPrefs.SetFloat("ShopPoints", 0);
        }
    }
}
