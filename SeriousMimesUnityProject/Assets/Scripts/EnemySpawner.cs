using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour {

	public List<Transform> wayPoints;

	public Transform startPosition;
	public Transform stopPosition;

	public float moveSpeed;

	public int spawnIncrease;
	public int spawnIncreaseInterval;
	public float healthIncrease;
	public int healthIncreaseInterval;

	public int majorityEnemyType;

	public GameObject enemy1;
	public GameObject enemy2;
	public GameObject enemy3;

	public GameObject majorityEnemy;

	public float spawnTimer;

	public int spawnMax;
	public int spawnMin;

	public bool nextWave = true;
	public float nextWaveDelay;
	public float nextWaveDelayMin;
	public float nextWaveReduction;
	public int reductionInterval;

	private float timer;
	private int reductionTracker;
	private int spawnTracker;
	private int healthTracker;
	private bool goToStart;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		if (goToStart) {
			transform.position = Vector3.MoveTowards (transform.position, startPosition.position, Time.deltaTime * moveSpeed);

			if (transform.position.x <= startPosition.position.x)
				goToStart = false;

		} else {
			transform.position = Vector3.MoveTowards (transform.position, stopPosition.position, Time.deltaTime * moveSpeed);

			if (transform.position.x >= stopPosition.position.x)
				goToStart = true;

		}

		if (nextWave) {

			nextWave = false;
			timer = 0;

			reductionTracker++;
			spawnTracker++;
			healthTracker++;



			if (spawnTracker == spawnIncreaseInterval) {

				spawnMax += spawnIncrease;
				spawnMin += spawnIncrease;
				spawnTracker = 0;
			}

			if (healthTracker == healthIncreaseInterval) {

				healthIncrease += healthIncrease;
				healthTracker = 0;
			}

			StartCoroutine (Spawn ());	
		}					
	}

	public IEnumerator Spawn() 
	{
		int spawnNum = Random.Range (spawnMin, spawnMax + 1);

		GameObject tempReference;

		majorityEnemyType = Random.Range (0, 3);

		if (majorityEnemyType == 0) {

			majorityEnemy = enemy1;
		}

		if (majorityEnemyType == 1) {
			majorityEnemy = enemy2;
		}

		if (majorityEnemyType == 2) {
			majorityEnemy = enemy3;
		}

		for (int x = 0; x < spawnNum; x++) {

			switch (Random.Range (0, 5)) {

			case 0:
				tempReference = Instantiate (enemy1);
				tempReference.transform.position = transform.position;
				tempReference.GetComponent<EnemyBehaviour> ().wayPoints = wayPoints;
				tempReference.GetComponent<EnemyBehaviour> ().InitializeBehaviour ();
				break;

			case 1:
				tempReference = Instantiate (enemy2);
				tempReference.transform.position = transform.position;
				tempReference.GetComponent<EnemyBehaviour> ().wayPoints = wayPoints;
				tempReference.GetComponent<EnemyBehaviour> ().InitializeBehaviour ();
				break;

			case 2:
				tempReference = Instantiate (enemy3);
				tempReference.transform.position = transform.position;
				tempReference.GetComponent<EnemyBehaviour> ().wayPoints = wayPoints;
				tempReference.GetComponent<EnemyBehaviour> ().InitializeBehaviour ();
				break;

			default:
				tempReference = Instantiate (majorityEnemy);
				tempReference.transform.position = transform.position;
				tempReference.GetComponent<EnemyBehaviour> ().wayPoints = wayPoints;
				tempReference.GetComponent<EnemyBehaviour> ().InitializeBehaviour ();
				break;
			}

			yield return new WaitForSeconds(.33f);
		}


	}
}
