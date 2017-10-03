using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
	public static GameManager instance;
	private Transform sceneObject;
	private int numEnemiesPerWave = 5;

	// 1 - Camera Shake
	public bool camShake;
	// 2 - Time Manipulation
	public bool pauseTimeOnHit;
	// 3 - Explosion Particles
	public bool particleSpawnOnExplode;
	// 4 - Enemy Wince
	public bool enemyWince;
	public bool enemyDeathExplosion;
	// 5 - Emphasize Movement
	public bool playerTrail;
	public bool bulletTrail;
	// 6 - Fire Impact
	public bool bulletSummonEffects;
	// 7 - Animated Background
	public bool animatedBackground;

	//TBI
	public bool audioEnabled;
	public bool knockbackOnHit;

	public bool useAllJuices;

	[SerializeField]
	private int numEnemiesToSpawn = 15;
	public bool spawnEnemies;
	[SerializeField]
	private float spawnRate = 2f;

	[SerializeField]
	private GameObject enemyPrefab;

	public int enemyHealth;

	private float lastSpawnEnemyTimeWidth = 0f;
	private float spawnEnemyCooldown = 1f;

	public float camShakeIntensity = 0.2f;

	void Start() {
		if (useAllJuices) {
			UseAllJuices ();
		}
		instance = this;
		Initialize ();
	}

	private void UseAllJuices() {
		camShake = true;
		pauseTimeOnHit = true;
		particleSpawnOnExplode = true;
		enemyWince = true;
		enemyDeathExplosion = true;
		playerTrail = true;
		bulletTrail = true;
		bulletSummonEffects = true;
		animatedBackground = true;

		audioEnabled = true;
		knockbackOnHit = true;
	}

	void Update() {
		lastSpawnEnemyTimeWidth += TimeManager.deltaTime;
		if (lastSpawnEnemyTimeWidth >= spawnEnemyCooldown) {
			SpawnEnemy ();
			lastSpawnEnemyTimeWidth = 0f;
			spawnEnemyCooldown = Random.Range (1/spawnRate - 0.5f, 1/spawnRate + 0.5f);
		}

		if (Input.GetKey (KeyCode.T)) {
			Time.timeScale = 0.5f;
		} else {
			Time.timeScale = 1f;
		}

		if (Input.GetKeyDown (KeyCode.JoystickButton0)) {
			UnityEngine.SceneManagement.SceneManager.LoadScene ("Main");
		}
	}
	private void SpawnEnemy() {
		if (!spawnEnemies) {
			return;
		}

		float yPos = 11f;
		float xPos = Random.Range (-4.73f, 4.73f);
		GameObject enemy = GameObject.Instantiate (enemyPrefab, sceneObject);
		enemy.transform.position = new Vector3 (xPos, yPos, 0f);

	}

	private void Initialize() {
		sceneObject = GameObject.Find ("Scene").transform;
		if (animatedBackground) {
			sceneObject.Find ("MovingEffect").gameObject.SetActive (true);
		}
	}
}
