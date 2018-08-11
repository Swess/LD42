using UnityEngine;
using UnityEditor;

namespace Others.Graphics {

	[ExecuteInEditMode]
	public class SpriteToTextureBridge : MonoBehaviour {

		[Header("The sprite file need to be imported with the read/write option enabled for this to work.")]
		public Sprite sprite;

		public Material templateMaterial;

		private Sprite _previousSprite;
		private string _mirrorName = "SecondSide";
		private Transform _mirror;

		private void Start() {
//			if(Application.isPlaying)
//				CreateMirrorQuad();
			_mirror = transform.Find(_mirrorName);

			UpdateTexture();
		}

		private void Update() {
			CheckForSpriteUpdate();
		}


		///
		/// Checkn if the sprite source has changed. Trigger the textures update if so,.
		///
		private void CheckForSpriteUpdate() {
			if (sprite != _previousSprite) {
				UpdateTexture();
				_previousSprite = sprite;
			}
		}


		///
		/// Script Main Feature
		/// Take the sprite, creates a texture with it, and apply
		/// it to both the main object renderer and the child/mirror object
		///
		public void UpdateTexture() {
			Material frontMat = new Material(templateMaterial);
			Material mirroredMat = new Material(templateMaterial);
			frontMat.CopyPropertiesFromMaterial(templateMaterial);
			mirroredMat.CopyPropertiesFromMaterial(templateMaterial);

			mirroredMat.name = frontMat.name = "Generated Material";

			var croppedTexture = new Texture2D((int) sprite.textureRect.width, (int) sprite.textureRect.height);

			var pixels = sprite.texture.GetPixels((int) sprite.textureRect.x,
				(int) sprite.textureRect.y,
				(int) sprite.textureRect.width,
				(int) sprite.textureRect.height);

			croppedTexture.SetPixels(pixels);
			croppedTexture.Apply();

			frontMat.mainTexture = croppedTexture;
			mirroredMat.mainTexture = croppedTexture;

			// Flip second Material
			mirroredMat.SetTextureScale("_MainTex", new Vector2(-1, 1));

			// Replace materials with generated one
			GetComponent<Renderer>().sharedMaterial = frontMat;
			if (Application.isPlaying && _mirror)
				_mirror.GetComponent<Renderer>().sharedMaterial = mirroredMat;
		}



		///
		/// Creates a child quad if there is none to be able to mirror the texture
		///
		private void CreateMirrorQuad() {
			// Already Exist ?
			if (transform.Find(_mirrorName))
				return;

			// Create Quad
			// Note : Uses prefab to prevent any collider on the object and errors
			Object quad = AssetDatabase.LoadAssetAtPath("Assets/_Prefabs/PrimitiveQuad.prefab", typeof(GameObject));
			if (!quad)
				return;

			GameObject plane = (GameObject) Instantiate(quad, Vector3.zero, Quaternion.identity, transform);

			// Set properties
			plane.transform.name = _mirrorName;
			plane.transform.localPosition = Vector3.zero;
			plane.transform.localScale = Vector3.one;
			plane.transform.rotation = new Quaternion(0, 180, 0, 0);

		}

	}
}
