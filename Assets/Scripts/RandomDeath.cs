using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class RandomDeath : MonoBehaviour {

    private Image titleImage;
    [SerializeField]
    private List<Sprite> deathImages = new List<Sprite>();

    void Start ()
    {
        titleImage = GetComponent<Image>();
        SetTitleImage();
    }
	
    void SetTitleImage()
    {
        titleImage.sprite = deathImages[Random.Range(0, (deathImages.Count - 1))];
    }

}
