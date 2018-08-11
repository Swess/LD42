using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cameras {

	public class CameraBGLerping : MonoBehaviour {

		[Range(0, 5)]public float lerpSpeed = 1f;
		public Color color1 = Color.white;
		public Color color2 = Color.white;

		private Camera _camera;


		private void Start() { _camera = GetComponent<Camera>(); }

		void Update() {
			Color lerpedColor = Color.Lerp(color1, color2, Mathf.PingPong(Time.time * lerpSpeed, 1));
			_camera.backgroundColor = lerpedColor;
		}

	}

}
