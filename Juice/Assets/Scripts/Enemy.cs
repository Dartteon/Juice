using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : BaseActor {

	protected override void Extra_Initialize() {
		this.health = 15;
	}
	public override void Damage(int dmg) {
		CameraManager.CamShake (0.1f, 0.2f);
		health -= dmg;

		if (GameManager.instance.showEnemyDamaged) {
			SpriteRenderer spriteR = transform.GetComponentInChildren<SpriteRenderer> ();
			Color col = spriteR.color;
			spriteR.color = new Color (col.r, col.g, col.b, 0.5f);
		}

		if (health <= 0) {
			StartDeathSequence ();
		}
	}
	protected override void StartDeathSequence() {
		gameObject.SetActive (false);
	}
}
