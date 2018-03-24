using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FinalScore : MonoBehaviour {

    private Text score;

	// Use this for initialization
	void Start ()
    {
        score = GetComponent<Text>();
        score.text = ScoreKeeper.score.ToString();
        ScoreKeeper.Reset();
	}
	
}
