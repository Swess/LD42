using Core;
using Entities.Player;
using Mechanics;
using UnityEngine;
using UnityEngine.UI;

namespace UI {
    public class ScoreDisplay : MonoBehaviour {

        private GameController   _core;
        private string _prefix = "";

        private void Start() {
            _core   = GameController.Instance;
            _prefix = GetComponent<Text>().text;

            _core.onScoring.AddListener( UpdateScore );
            _core.onScoring.Invoke();
        }


        private void UpdateScore() {
            GetComponent<Text>().text = _prefix + _core.score;
        }

    }
}