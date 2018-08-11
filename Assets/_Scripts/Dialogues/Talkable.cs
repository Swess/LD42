using System.Collections.ObjectModel;
using Core;
using Core.Inputs;
using Rewired;
using UI;
using UnityEngine;

namespace Entities {
    [RequireComponent(typeof(CircleCollider2D))]
    public class Talkable : MonoBehaviour {

        public GameObject  interactionPanel;
        public DialogueSet dialogueSet;

        private bool             _listeningInputs = false;
        private Rewired.Player   _inputs;
        private bool             _talking = false;
        private CircleCollider2D _collider;

        private DialoguesController _dialoguesController;

        // ========================================================
        // ========================================================
        // ========================================================


        private void Awake() {
            _dialoguesController = GameController.Instance.UiController.dialoguesController;

            _inputs   = ReInput.players.GetPlayer(0);
            _collider = GetComponent<CircleCollider2D>();

            if ( !_collider.isTrigger ) {
                Debug.LogError("The CircleCollider2D of this Talkable GameObject needs to be a for this script to function properly.");
            }
        }


        private void Start() { interactionPanel.SetActive(false); }


        private void Update() {
            if ( _listeningInputs && !_talking && _inputs.GetButtonDown(RewiredConsts.Action.Interact) ) {
                StartDialogue();
            }

            // Dialogue loop
            if ( _talking && _inputs.GetButtonDown(RewiredConsts.Action.UISubmit) ) {
                string text = dialogueSet.GetNext();
                if(text != null)
                    _dialoguesController.SetCurrentText(text);
                else
                    EndDialogue();
            }
        }


        /// <summary>
        /// If the player gets inside the interactable radius
        /// </summary>
        /// <param name="other"></param>
        private void OnTriggerEnter2D(Collider2D other) {
            if ( !_talking && other.CompareTag("Player") ) {
                interactionPanel.SetActive(true);
                _listeningInputs = true;
            }
        }


        /// <summary>
        /// If the player leaves the interactable radius
        /// </summary>
        /// <param name="other"></param>
        private void OnTriggerExit2D(Collider2D other) {
            if ( other.CompareTag("Player") ) {
                EndDialogue();
            }
        }


        // ========================================================
        // ========================================================
        // ========================================================


        /// <summary>
        /// Start the dialogue process and actions
        /// </summary>
        public void StartDialogue() {
            _talking = true;
            interactionPanel.SetActive(false);
            _dialoguesController.StartDialogue();
            _dialoguesController.SetCurrentText(dialogueSet.GetNext());
        }


        public void EndDialogue() {
            _talking         = false;
            _listeningInputs = false;
            interactionPanel.SetActive(false);
            _dialoguesController.EndDialogue();
            dialogueSet.Reset();
        }

    }
}