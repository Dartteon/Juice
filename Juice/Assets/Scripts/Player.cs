using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : BaseActor {
	private bool trailEnabled = false;
	[SerializeField]
	private GameObject bigBulletPrefab;

	protected override void Extra_Update() {
		if (GameManager.instance == null) return;

		if (trailEnabled != GameManager.instance.playerTrailEnabled) {
			trailEnabled = GameManager.instance.playerTrailEnabled;
			transform.Find ("Trail").gameObject.SetActive (trailEnabled);
		}
	}
	protected override void HandleMovement() {
		bool isMovingLeft = Input.GetKey (KeyCode.LeftArrow);
		bool isMovingRight = Input.GetKey (KeyCode.RightArrow);

		bool isMovingDown = Input.GetKey (KeyCode.DownArrow);
		bool isMovingUp = Input.GetKey (KeyCode.UpArrow);

		float xVel = 0f;
		float yVel = 0f;

		if (TimeManager.isPaused) {
			rb2d.velocity = Vector2.zero;
			return;
		} else {
			if (isMovingLeft) {
				xVel += -moveSpeed;
			}
			if (isMovingRight) {
				xVel += moveSpeed;
			}
			if (isMovingUp) {
				yVel += moveSpeed;
			}
			if (isMovingDown) {
				yVel += -moveSpeed;
			}
			rb2d.velocity = new Vector2 (xVel, yVel);
		}
	}
	protected override void HandleShooting() {
		if (TimeManager.isPaused) return;


//		if (Input.GetKeyDown (KeyCode.Space)) {
//			GameObject bulletObj = GameObject.Instantiate (bulletPrefab, transform.parent);
//			bulletObj.transform.position = gunPoint.transform.position;
//			bulletObj.SetActive (true);
//			Bullet bullet = bulletObj.GetComponent<Bullet> ();
//			bullet.Initialize (Vector2.up, true);
//		}
		if (Input.GetKey (KeyCode.Space) || Input.GetKeyDown (KeyCode.Space)) {
			if (lastFiredTimeDistance >= 0.1f) {
				lastFiredTimeDistance = 0f;
				GameObject bulletObj = GameObject.Instantiate (bulletPrefab, transform.parent);
				bulletObj.transform.position = gunPoint.transform.position;
				bulletObj.SetActive (true);
				Bullet bullet = bulletObj.GetComponent<Bullet> ();
				bullet.Initialize (Vector2.up, true);

				if (GameManager.instance.bulletSummonEffects) {
					GameObject fireEffect = GameObject.Instantiate (fireEffectPrefab, transform.parent);
					fireEffect.transform.position = gunPoint.transform.position;
					Destroy (fireEffect, 0.5f);
					rb2d.AddForce (Vector2.down * 10f, ForceMode2D.Impulse);
				}
			} else {
				lastFiredTimeDistance += TimeManager.deltaTime;
			}
		}

		if (Input.GetKeyDown (KeyCode.LeftAlt)) {
			GameObject bulletObj = GameObject.Instantiate (bigBulletPrefab, transform.parent);
			bulletObj.transform.position = gunPoint.transform.position;
			bulletObj.SetActive (true);
			Bullet bullet = bulletObj.GetComponent<Bullet> ();
			bullet.Initialize (Vector2.up, true);

			if (GameManager.instance.bulletSummonEffects) {
				GameObject fireEffect = GameObject.Instantiate (fireEffectPrefab, transform.parent);
				fireEffect.transform.position = gunPoint.transform.position;
				Destroy (fireEffect, 0.5f);
				rb2d.AddForce (Vector2.down * 30f, ForceMode2D.Impulse);
			}
		}
	}
}
