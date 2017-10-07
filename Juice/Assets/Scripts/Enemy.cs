using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : BaseActor {
	private Animator anim;

	protected override void Extra_Initialize() {
		this.health = GameManager.instance.enemyHealth;
		this.moveSpeed = 1f;
		anim = transform.GetComponent<Animator> ();
	}
	public override void Damage(int dmg) {
		health -= dmg;
		ShowHitEffect ();

		if (health <= 0) {
			StartDeathSequence ();
		}
	}
	protected override void StartDeathSequence() {
		CameraManager.CamShake (GameManager.instance.camShakeIntensity, 0.2f);
		if (GameManager.instance.enemyDeathExplosion) {
			anim.Play ("EnemyDeath");
			Invoke ("DestroySelf", 0.5f);
			transform.GetComponentInChildren<Collider2D> ().enabled = false;
			GameObject explosion = GameObject.Instantiate (deathExplosionPrefab, transform.parent);
			explosion.transform.position = transform.position;
			Destroy (explosion, 0.5f);
		} else {
			DestroySelf ();
		}
	}
	private void DestroySelf() {
		gameObject.SetActive (false);
	}

	protected override void HandleMovement() {

		float xVel = 0f;
		float yVel = 0f;

		if (TimeManager.isPaused) {
			rb2d.velocity = Vector2.zero;
			return;
		} else {
			yVel += -moveSpeed;
			rb2d.velocity = new Vector2 (xVel, yVel);
		}
	}

	private void ShowHitEffect() {
		if (GameManager.instance.enemyWince) {
			anim.Play ("EnemyHit");
		}
	}
}
