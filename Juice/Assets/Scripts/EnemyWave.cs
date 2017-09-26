using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWave : MonoBehaviour {
	private float moveSpeed =1.5f;
	private bool isMovingRight;
	private float maxDisplacementX = 4f;
	private bool isMovingDown;
	private float amountToMoveDown = .5f;

	void Update () {
		Vector3 currPos = transform.position;

		if (isMovingDown) {
			isMovingDown = false;
			transform.position = new Vector3 (currPos.x, currPos.y - amountToMoveDown, 0f);
		}

		else if (isMovingRight) {
			if (currPos.x > maxDisplacementX) {
				isMovingRight = false;
				isMovingDown = true;
			} else {
				float nextX = currPos.x + TimeManager.deltaTime * moveSpeed;
				transform.position = new Vector3 (nextX, currPos.y, 0f);
			}
		} 

		else {
			if (currPos.x < -maxDisplacementX) {
				isMovingRight = true;
				isMovingDown = true;
			} else {
				float nextX = currPos.x - TimeManager.deltaTime * moveSpeed;
				transform.position = new Vector3 (nextX, currPos.y, 0f);
			}
		}
	}
}
