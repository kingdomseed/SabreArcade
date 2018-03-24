using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class BgChanger : MonoBehaviour {

    private Image background;
    private int index = 0;
    [SerializeField]
    private List<Sprite> bgImage = new List<Sprite>();
    public float timeToStart;
    public float timeToNext;

    void Start()
    {
        background = GetComponent<Image>();
        SetBackground();
        StartCoroutine(CrossAlpha(timeToStart, timeToNext));
       
    }

    void Change()
    {
        if (index < (bgImage.Count - 1))
        {
            index++;
            SetBackground();
        }
        else
        {
            index = 0;
            SetBackground();
        }
    }

    void SetBackground()
    {
        background.sprite = bgImage[index];
    }

    IEnumerator CrossAlpha(float TimeUntilChangeBackground, float FadingTime)
    {
        yield return new WaitForSeconds(TimeUntilChangeBackground - (2 * FadingTime));
        background.CrossFadeAlpha(0, FadingTime, false);
        yield return new WaitForSeconds(FadingTime);
        Change();
        background.CrossFadeAlpha(1, FadingTime, false);
        yield return new WaitForSeconds(FadingTime);
        StartCoroutine(CrossAlpha(TimeUntilChangeBackground, FadingTime));
    }
}
