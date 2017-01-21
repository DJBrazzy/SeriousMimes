using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour {

	public int waveNum;

	public int spawnNumber;

	public int spawnIncreaseWaveNum;
	public int healthIncreaseWaveNum;

	public int spawnIncrease;
	public int spawnIncreaseInterval;
	public float healthIncrease;
	public int healthIncreaseInterval;

	public int majorityEnemyType;

	public GameObject enemy1;
	public GameObject enemy2;
	public GameObject enemy3;

	public float spawnTimer;

	public int spawnMax;
	public int spawnMin;

	public bool nextWave;
	public float nextWaveDelay;
	public float nextWaveDelayMin;
	public float nextWaveReduction;
	public int reductionInterval;

	private float timer;
	private int reductionTracker;
	private int spawnTracker;
	private int healthTracker;



	// Use this for initialization
	void Start () {

		
	
	}
	
	// Update is called once per frame
	void Update () {

		if (nextWave) {

			timer += Time.deltaTime;

			if(timer > nextWaveDelay) {


				nextWave = false;
				timer = 0;

				reductionTracker++;
				spawnTracker++;
				healthTracker++;

				if (reductionTracker == reductionInterval) {

					if ((nextWaveDelay - nextWaveReduction) > nextWaveDelayMin) {
						nextWaveDelay -= nextWaveReduction;

						reductionTracker = 0;
					}
				}

				if (spawnTracker == spawnIncreaseInterval) {

					spawnMax += spawnIncrease;
					spawnTracker = 0;
				}

				if (healthTracker == healthIncreaseInterval) {

					healthIncrease += healthIncrease;
					healthTracker = 0;
				}

				StartCoroutine (Spawn ());
			}	
		}					
	}

	public IEnumerator Spawn() 
	{
		while(true)
		{

			yield return new WaitForSeconds(2);
		}

	}
}
