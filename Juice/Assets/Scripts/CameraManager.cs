using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour {
	private static CameraManager instance;

	private Vector2 origin;

	private float intensity;
	private float totalShakeDuration;
	private float currShakeDuration;
	private bool isShaking;

	void Start() {
		instance = this;
	}

	void Update() {
		if (TimeManager.isPaused) return;

		if (isShaking) {
			if (currShakeDuration > totalShakeDuration) {
				isShaking = false;
				transform.position = new Vector3 (origin.x, origin.y, transform.position.z);
				currShakeDuration = 0f;
			} else {
				Vector2 offset = new Vector2 (Random.Range (-intensity, intensity), Random.Range (-intensity, intensity));
				transform.position = new Vector3 (origin.x + offset.x, origin.y + offset.y, transform.position.z);
				currShakeDuration += TimeManager.deltaTime;
			}
		}
	}

	public static void CamShake (float intensity, float duration) {
		if (!GameManager.instance.camShake) return;

		if (instance.isShaking) {
			instance.currShakeDuration += duration;
		}
		instance.intensity = intensity;
		instance.totalShakeDuration = duration;
		instance.isShaking = true;
	}

}
