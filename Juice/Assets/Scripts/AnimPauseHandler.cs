using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimPauseHandler : MonoBehaviour {
	private bool isPaused;
	private Animator[] anims;
	private ParticleSystem[] pEmitters;

	void Start() {
		anims = transform.GetComponentsInChildren<Animator> ();
		pEmitters = transform.GetComponentsInChildren<ParticleSystem>();
	}

	void Update () {
		if (isPaused) {	
			if (!TimeManager.isPaused) {
				isPaused = false;
				SetAnimsEnabled (true);
			}
		} else {
			if (TimeManager.isPaused) {
				isPaused = true;
				SetAnimsEnabled (false);
			}
		}
	}

	private void SetAnimsEnabled (bool enabled) {
		for (int i = 0; i < anims.Length; i++) {
			anims [i].enabled = enabled;
		}
		for (int i = 0; i < pEmitters.Length; i++) {
			if (enabled) {
				pEmitters [i].Play ();
			} else {
				pEmitters [i].Pause ();
			}
		}
	}
}
