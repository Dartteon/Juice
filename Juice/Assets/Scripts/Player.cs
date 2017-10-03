using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : BaseActor {
	private bool trailEnabled = false;
	[SerializeField]
	private GameObject bigBulletPrefab;

	protected override void Extra_Update() {
		if (GameManager.instance == null) return;

		if (trailEnabled != GameManager.instance.playerTrail) {
			trailEnabled = GameManager.instance.playerTrail;
			transform.Find ("Trail").gameObject.SetActive (trailEnabled);
		}
	}
	protected override void HandleMovement() {
		if (Input.GetJoystickNames ().Length > 0) {
			HandleController ();
			return;
		}
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
	private void HandleController() {
		if (TimeManager.isPaused) {
			rb2d.velocity = Vector2.zero;
			return;
		} 
		float xVel = Input.GetAxis ("Horizontal") * moveSpeed;
		float yVel = Input.GetAxis ("Vertical") * moveSpeed;
		rb2d.velocity = new Vector2 (xVel, yVel);
	}
	protected override void HandleShooting() {
		if (TimeManager.isPaused) return;

		if (Input.GetKey (KeyCode.Space) || Input.GetKeyDown (KeyCode.Space)
			|| Input.GetKey(KeyCode.JoystickButton3)) {
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

		if (Input.GetKeyDown (KeyCode.LeftAlt) || Input.GetKeyDown(KeyCode.JoystickButton1)) {
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
