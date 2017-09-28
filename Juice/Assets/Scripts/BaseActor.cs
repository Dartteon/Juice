using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseActor : MonoBehaviour {
	[SerializeField]
	protected GameObject bulletPrefab;
	[SerializeField]
	protected GameObject fireEffectPrefab;
	protected Transform gunPoint;
	protected Rigidbody2D rb2d;
	protected float moveSpeed = 5f;

	protected int health = 2;

	protected float lastFiredTimeDistance = 100f;

	void Start() {
		Initialize ();
		gunPoint = transform.Find ("GunPoint");
	}

	public void Initialize() {
		rb2d = transform.GetComponent<Rigidbody2D> ();
		Extra_Initialize ();
	}
	protected virtual void Extra_Initialize() {
	}
	void Update() {
		HandleMovement ();
		HandleShooting ();
		Extra_Update ();
	}
	protected virtual void Extra_Update() {

	}
	protected virtual void HandleMovement() {
	}
	protected virtual void HandleShooting() {
	}
	public virtual void Damage(int dmg) {
	}
	protected virtual void StartDeathSequence() {
	}
}
