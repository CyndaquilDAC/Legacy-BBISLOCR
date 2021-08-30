using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchToGetAllBooksScript : MonoBehaviour
{
    // Start is called before the first frame update

    private void OnTriggerEnter(Collider other)
    {
        gc.notebooks = 7;
        notebooks.SetActive(false);
        gc.UpdateNotebookCount();
    }
    public Collider touch;
    public GameControllerScript gc;
    public GameObject notebooks;
}
