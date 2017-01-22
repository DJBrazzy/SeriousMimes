using UnityEngine;
using System.Collections;

public class SpawnMaster : MonoBehaviour {

	public EnemySpawner spawnPoint1;
	public EnemySpawner spawnPoint2;
	public EnemySpawner spawnPoint3;

	public bool spawned1;
	public bool spawned2;
	public bool spawned3;

	public float nextWaveDelay;
	public float nextWaveDelayMin;
	public float nextWaveReduction;
	public int reductionInterval;
	public int reductionTracker;

	public int waveNum = 1;

	public float wavePause;

	public static int enemyCount;

	public float delay1;
	public float delay2;
	public float delay3;

	private float waveTimer;

	public bool nextWave;

	private float tempDelay;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		if (enemyCount <= 0 && !nextWave) {


			waveTimer += Time.deltaTime;

			//start timer on screen

			if (waveTimer >= wavePause) {

				waveNum++;
				reductionTracker++;
				nextWave = true;
				waveTimer = 0;
				spawned1 = spawned2 = spawned3 = false;

				tempDelay = delay1;
				delay1 = delay2;
				delay2 = delay3;
				delay3 = tempDelay;
				enemyCount = 0;
			}

		}

		if (nextWave) {

			if (reductionTracker == reductionInterval) {

				delay1 -= nextWaveReduction*1.5f;
				delay2 -= nextWaveReduction * 1.2f;
				delay3 -= nextWaveReduction;
				reductionTracker = 0;
			}

			waveTimer += Time.deltaTime;

			if (waveTimer > delay1 && !spawned1) {

				spawnPoint1.nextWave = true;
				spawned1 = true;
			}
			if (waveTimer > delay3 && !spawned3) {

				spawnPoint3.nextWave = true;
				spawned3 = true;
			}
			if (waveTimer > delay3 && !spawned3) {

				spawnPoint3.nextWave = true;
				spawned3 = true;
				nextWave = false;
			}
		}	
	}
}
