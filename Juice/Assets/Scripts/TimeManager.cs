using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour {
	private static TimeManager instance;
	public static bool isPaused { get; private set; }
	public static float deltaTime { get; private set; }
	private static float resumeTime;

	void Start() {
		instance = this;
	}

	void Update() {
		if (!isPaused) {
			if (Input.GetKey (KeyCode.T) || Input.GetKey(KeyCode.JoystickButton2)) {
				Time.timeScale = 0.5f;
			} else {
				Time.timeScale = 1f;
			}
			deltaTime = Time.deltaTime;
		} else {
			deltaTime = 0f;
			if (Time.time >= resumeTime) {
				isPaused = false;
			}
		}
	}

	public static void StopTime (float duration) {
		resumeTime = Time.time + duration;
		isPaused = true;
	}
}
