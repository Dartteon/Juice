using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
	private Vector2 direction;
	private bool isFriendly;
	private Rigidbody2D rb2d;
	private float speed = 10f;
	private Animator anim;
	[SerializeField]
	private GameObject explosionPrefab;

	private bool isAnimExploding;
	private float timeAfterDeath;

	public void Initialize(Vector2 direction, bool isFriendly) {
		this.direction = direction.normalized;
		this.isFriendly = isFriendly;
		rb2d = transform.GetComponent<Rigidbody2D> ();
		anim = transform.GetComponent<Animator> ();

		if (GameManager.instance.trailsEnabled) {
			transform.Find ("Trail").gameObject.SetActive (true);
		}
	}

	void Update() {
//		if (TimeManager.isPaused) {
//			if (isAnimExploding) {
//				anim.enabled = false;
//			}
//		}

		Vector3 currPos = transform.position;
		Vector2 offset = direction * TimeManager.deltaTime * speed;
		Vector3 offset3 = new Vector3 (offset.x, offset.y, 0f);
		Vector3 nextPos = currPos + offset3;
		rb2d.MovePosition (nextPos);

		if (isAnimExploding) {
//			anim.enabled = true;
			if (timeAfterDeath >= 0.5f) {
				DestroySelf ();
			} else {
				timeAfterDeath += TimeManager.deltaTime;
			}
		}
	}

	void OnTriggerEnter2D (Collider2D col) {
		if (col.GetComponent<Floor> () != null) {
			StartDestructSequence ();
		}

		if (isFriendly) {
			Enemy e = col.GetComponent<Enemy> ();
			if (e != null) {
				e.Damage (1);
				StopTime ();
				SpawnExplosion ();
				StartDestructSequence ();
			}
		} else {
			Player p = col.GetComponent<Player> ();
			if (p != null) {
				p.Damage (1);
				StopTime ();
				StartDestructSequence ();
			}
		}
	}

	private void StopTime() {
		if (GameManager.instance.pauseTimeOnHit) {
			TimeManager.StopTime (.07f);
		}
	}

	private void StartDestructSequence() {
		SpawnExplosion ();
		if (!GameManager.instance.particleSpawnOnExplode && !GameManager.instance.bulletExplode) {
			DestroySelf ();
		} else {
			transform.Find ("Sprite").gameObject.SetActive (false);
		}
	}

	private void SpawnExplosion() {
		GameObject explosion = GameObject.Instantiate (explosionPrefab, transform);
		explosion.transform.position = transform.position;
		explosion.SetActive (true);

		if (GameManager.instance.particleSpawnOnExplode) {
			explosion.transform.Find ("ExplodeParticles").gameObject.SetActive (true);
		}
		if (GameManager.instance.bulletExplode) {
			direction = Vector2.zero;
			anim.enabled = true;
			//			anim.Play ("BulletExplode");
			isAnimExploding = true;
			timeAfterDeath = 0f;
			transform.GetComponentInChildren<Collider2D> ().enabled = false;
			explosion.transform.Find ("Sprite").gameObject.SetActive (true);
		} 
	}

	private void DestroySelf() {
		Destroy (gameObject);
	}
}
