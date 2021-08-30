using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Audio;
using UnityEngine.UI;

public class TrevorScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        randomBagChance = UnityEngine.Random.Range(0, 3500);
        if(!trashDropped && randomBagChance == 1500f)
        {
            DropTrash();
        }
    }

    public void DropTrash()
    {
        trashDropped = true;
        this.audioDevice.PlayOneShot(trashDropSounds[UnityEngine.Random.Range(0, trashDropSounds.Length)]);
        trevorSpriteRenderer.sprite = trevorBagless;
        GameObject.Instantiate(bag, trashDropSpot);
        currentBag = GameObject.FindWithTag("TrashBag");
        currentBag.transform.position = trashDropSpot.transform.position;
        StartCoroutine(PostTrashDrop());
    }

    public IEnumerator PostTrashDrop()
    {
        new WaitForSeconds(UnityEngine.Random.Range(120, 135));
        GameObject.Destroy(currentBag);
        trashDropped = false;
        trevorSpriteRenderer.sprite = trevorWithBag;
        yield break;
    }
    public GameObject bag;
    public Sprite trevorBagless;
    public Sprite trevorWithBag;
    public AudioClip[] trashDropSounds;
    public bool trashDropped;
    public Transform trashDropSpot;
    public GameObject currentBag;
    public float randomBagChance;
    public AudioSource audioDevice;
    public SpriteRenderer trevorSpriteRenderer;
}
