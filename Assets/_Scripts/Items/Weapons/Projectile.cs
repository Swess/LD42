using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

	public float timeBeforeImpact = 1f;
	public float frictionFactor = 2f;

	public GameObject instanciateOnImpact;

	private bool _hasHit = false;
	private Rigidbody2D _rb;
	private AudioSource _audio;

	// Use this for initialization
	void Start() {
		_audio = GetComponent<AudioSource>();
		_rb = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
		if ( timeBeforeImpact > 0 ) {
			timeBeforeImpact -= Time.deltaTime;
		} else if(!_hasHit) {
			Hit();
		}
	}


	private void FixedUpdate() {
		SlowDown();
	}


	private void SlowDown() {
		Vector2 opposite = -_rb.velocity;
		_rb.AddForce(opposite * frictionFactor);
	}

	void Hit() {
		_hasHit = true;
		_audio.Play();
		GetComponent<CircleCollider2D>().enabled = true;
		StartCoroutine(DestroyAfterHit());
	}


	private void OnTriggerStay2D(Collider2D other) {
		if (other.CompareTag("Fire")) {
			FireLogic script = other.gameObject.GetComponent<FireLogic>();
			if ( script && script.parentTilemap ) {
				StartCoroutine(DestroyFireTile(script));
			} else {
				Destroy(other);
			}
		}
	}


	IEnumerator DestroyFireTile(FireLogic script) {
		yield return new WaitForEndOfFrame();

		script.AddScore();

		if(script.parentTilemap)
			script.parentTilemap.SetTile(script.tilemapPos, null);
	}


	IEnumerator DestroyAfterHit() {
		yield return new WaitForSeconds(0.1f);

		if (instanciateOnImpact) {
			// Particles ?? Something else ??
			Instantiate(instanciateOnImpact, transform.position, Quaternion.identity);
		}
		Destroy(gameObject);
	}

}
