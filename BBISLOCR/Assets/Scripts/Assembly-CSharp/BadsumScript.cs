using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BadsumScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            errorScreen.SetActive(false);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            errorScreen.SetActive(true);
        }
    }
    public IEnumerator ErrorScreen()
    {
        new WaitForSeconds(30f);
        if (errorScreen.activeSelf == true)
        {
            Application.Quit();
        }
        yield break;
    }
    public GameObject errorScreen;
}
