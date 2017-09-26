using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : BaseActor {


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
			} else {
				lastFiredTimeDistance += TimeManager.deltaTime;
			}
		}
	}
}
