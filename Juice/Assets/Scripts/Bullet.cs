using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
	private Vector2 direction;
	private bool isFriendly;
	private Rigidbody2D rb2d;
	[SerializeField]
	private float speed = 10f;
	private Animator anim;
	[SerializeField]
	private GameObject explosionPrefab;
	[SerializeField]
	private int damage = 1;
	[SerializeField]
	private int maxNumHit = 1;
	[SerializeField]
	private bool stopTimeOnHit;

	private int currNumHit = 0;
	private float timeAfterDeath;

	public void Initialize(Vector2 direction, bool isFriendly) {
		this.direction = direction.normalized;
		this.isFriendly = isFriendly;
		rb2d = transform.GetComponent<Rigidbody2D> ();
		anim = transform.GetComponent<Animator> ();

		if (GameManager.instance.bulletSummonEffects) {
			anim.enabled = true;
		}
		if (GameManager.instance.bulletTrail) {
			transform.Find ("Trail").gameObject.SetActive (true);
		}
	}

	void Update() {
		Vector3 currPos = transform.position;
		Vector2 offset = direction * TimeManager.deltaTime * speed;
		Vector3 offset3 = new Vector3 (offset.x, offset.y, 0f);
		Vector3 nextPos = currPos + offset3;
		rb2d.MovePosition (nextPos);
	}

	void OnTriggerEnter2D (Collider2D col) {
		if (col.GetComponent<Floor> () != null) {
			StartDestructSequence ();
		}

		if (isFriendly) {
			currNumHit++;
			Enemy e = col.GetComponent<Enemy> ();
			if (e != null) {
				e.Damage (damage);
				StopTime ();
				SpawnExplosion ();
			}
			if (currNumHit >= maxNumHit) {
				StartDestructSequence ();
			}
		} else {
			Player p = col.GetComponent<Player> ();
			if (p != null) {
				p.Damage (damage);
				StopTime ();
				StartDestructSequence ();
			}
		}
	}

	private void StopTime() {
		if (stopTimeOnHit && GameManager.instance.pauseTimeOnHit) {
			TimeManager.StopTime (.07f);
		}
	}

	private void StartDestructSequence() {
		SpawnExplosion ();
		DestroySelf ();
	}

	private void SpawnExplosion() {
		GameObject explosion = GameObject.Instantiate (explosionPrefab, transform.parent);
		explosion.transform.position = transform.position + new Vector3(0f, 0f, -1f);
		explosion.SetActive (true);
		Destroy (explosion, 0.5f);

		if (GameManager.instance.particleSpawnOnExplode) {
			explosion.transform.Find ("ExplodeParticles").gameObject.SetActive (true);
		}
	}

	private void DestroySelf() {
		Destroy (gameObject);
	}
}
