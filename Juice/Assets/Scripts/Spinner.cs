using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spinner : MonoBehaviour {
	[SerializeField]
	private float spinSpeed;
	private float currRot = 0f;

	void Update () {
		if (!TimeManager.isPaused) {	
			currRot = currRot + (spinSpeed * TimeManager.deltaTime);
			transform.rotation = Quaternion.Euler (0f, 0f, currRot);
		}
	}
}
