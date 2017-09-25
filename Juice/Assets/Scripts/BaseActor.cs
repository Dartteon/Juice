using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseActor : MonoBehaviour {
	[SerializeField]
	protected GameObject bulletPrefab;
	protected Transform gunPoint;
	protected Rigidbody2D rb2d;
	protected float moveSpeed = 5f;

	protected int health;

	protected float lastFiredTimeDistance = 100f;

	void Start() {
		Initialize ();
		gunPoint = transform.Find ("GunPoint");
	}

	public void Initialize() {
		rb2d = transform.GetComponent<Rigidbody2D> ();
	}
	void Update() {
		if (TimeManager.isPaused) return;
		HandleMovement ();
		HandleShooting ();
	}
	protected virtual void HandleMovement() {
	}
	protected virtual void HandleShooting() {
	}
	public virtual void Damage(int dmg) {
		CameraManager.CamShake (0.1f, 0.2f);
	}
}
