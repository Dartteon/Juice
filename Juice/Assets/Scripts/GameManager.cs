using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
	public static GameManager instance;
	private Transform sceneObject;
	private int numEnemiesPerWave = 5;

	public bool camShake;
	public bool bulletExplode;
	public bool pauseTimeOnHit;
	public bool showEnemyDamaged;

	//TBI
	public bool audioEnabled;
	public bool particleSpawnOnExplode;
	public bool trailsEnabled;
	public bool knockbackOnHit;

	[SerializeField]
	private int numEnemiesToSpawn = 15;

	[SerializeField]
	private GameObject enemyPrefab;

	private float lastSpawnEnemyTimeWidth = 0f;
	private float spawnEnemyCooldown = 1f;

	void Start() {
		instance = this;
		Initialize ();
	}

	void Update() {
		lastSpawnEnemyTimeWidth += TimeManager.deltaTime;
		if (lastSpawnEnemyTimeWidth >= spawnEnemyCooldown) {
			SpawnEnemy ();
			lastSpawnEnemyTimeWidth = 0f;
			spawnEnemyCooldown = Random.Range (1.5f, 3f);
		}
	}
	private void SpawnEnemy() {
		float yPos = 11f;
		float xPos = Random.Range (-4.73f, 4.73f);
		GameObject enemy = GameObject.Instantiate (enemyPrefab, sceneObject);
		enemy.transform.position = new Vector3 (xPos, yPos, 0f);

	}

	private void Initialize() {
		sceneObject = GameObject.Find ("Scene").transform;
//		SpawnEnemies ();
	}

	private void SpawnEnemies() {
		float xDist = 1.5f;
		float yDist = 1.5f;
		for (int i = 0; i < numEnemiesToSpawn; i++) {
			int rowIndex = i % numEnemiesPerWave;
			float xPos = (rowIndex - ((numEnemiesPerWave)/2)) * xDist;
			float yPos = ((-i / numEnemiesPerWave)) * yDist + (numEnemiesToSpawn/numEnemiesPerWave*yDist);
			GameObject enemy = GameObject.Instantiate (enemyPrefab, sceneObject);
			enemy.transform.position = new Vector3 (xPos, yPos, 0f);
		}
	}
}
