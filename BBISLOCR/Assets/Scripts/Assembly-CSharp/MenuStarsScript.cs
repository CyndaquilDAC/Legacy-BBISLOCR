using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuStarsScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.HasKey("HasWonGame"))
        {
            starOne.sprite = filledStar1;
        }
        if (PlayerPrefs.HasKey("HasWonGameSecret"))
        {
            starTwo.sprite = filledStar2;
        }
        if (PlayerPrefs.HasKey("HasSeenTestroom"))
        {
            starThree.sprite = filledStar3;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerPrefs.HasKey("HasWonGame"))
        {
            starOne.sprite = filledStar1;
        }
        if (PlayerPrefs.HasKey("HasWonGameSecret"))
        {
            starTwo.sprite = filledStar2;
        }
        if (PlayerPrefs.HasKey("HasSeenTestroom"))
        {
            starThree.sprite = filledStar3;
        }
    }
    public Image starOne;
    public Image starTwo;
    public Image starThree;
    public Sprite filledStar1;
    public Sprite filledStar2;
    public Sprite filledStar3;
}
