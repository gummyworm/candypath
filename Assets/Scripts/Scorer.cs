using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Scorer : MonoBehaviour {
	public Text scoreText;
	protected int score;

	// Use this for initialization
	void Start () {
		score = 0;
	}
	
	// Update is called once per frame
	void Update () {
	}

	// Score adds pts to the total score
	public void Score(int pts) {
		score += pts;
		scoreText.text = score.ToString ("D6");
	}

	// Reset sets the score to 0
	void Reset() {
		score = 0;
	}
}
