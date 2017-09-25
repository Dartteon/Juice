using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
	public static GameManager instance;

	public bool camShake;
	public bool bulletExplode;
	public bool pauseTimeOnHit;

	//TBI
	public bool audioEnabled;
	public bool particleSpawnOnExplode;
	public bool trailsEnabled;

	void Start() {
		instance = this;
	}
}
