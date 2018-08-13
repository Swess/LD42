using System.Collections;
using System.Collections.Generic;
using Core;
using UnityEngine;
using UnityEngine.UI;

public class MenuLastScoreDisplay : MonoBehaviour {

	// Use this for initialization
	void Start () {
		// Rest score
		GameController.Instance.score = 0;

		Text text = GetComponent<Text>();
		text.text = "Last Score : " + GameController.Instance.saveFile.lastScore;
	}

}
