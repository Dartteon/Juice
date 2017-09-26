using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
	public static GameManager instance;
	private Transform enemyWaveObject;
	private int numEnemiesPerWave = 5;

	public bool camShake;
	public bool bulletExplode;
	public bool pauseTimeOnHit;
	public bool showEnemyDamaged;

	//TBI
	public bool audioEnabled;
	public bool particleSpawnOnExplode;
	public bool trailsEnabled;

	[SerializeField]
	private int numEnemiesToSpawn = 15;

	[SerializeField]
	private GameObject enemyPrefab;

	void Start() {
		instance = this;
		Initialize ();
	}

	private void Initialize() {
		enemyWaveObject = GameObject.Find ("Scene").transform.Find ("EnemyWave");
		SpawnEnemies ();
	}

	private void SpawnEnemies() {
		float xDist = 1.5f;
		float yDist = 1.5f;
		for (int i = 0; i < numEnemiesToSpawn; i++) {
			int rowIndex = i % numEnemiesPerWave;
			float xPos = (rowIndex - ((numEnemiesPerWave)/2)) * xDist;
			float yPos = ((-i / numEnemiesPerWave)) * yDist + (numEnemiesToSpawn/numEnemiesPerWave*yDist);
			GameObject enemy = GameObject.Instantiate (enemyPrefab, enemyWaveObject);
			enemy.transform.position = new Vector3 (xPos, yPos, 0f);
		}
	}
}
