using System.Collections;
using System.Collections.Generic;
using Core;
using UnityEngine;
using UnityEngine.UI;

public class MenuBestScoreDisplay : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Text text = GetComponent<Text>();
		text.text = "BEST SCORE : " + GameController.Instance.saveFile.bestScore;
	}

}
