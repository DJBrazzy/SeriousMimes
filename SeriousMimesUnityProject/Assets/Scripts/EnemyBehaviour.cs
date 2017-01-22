using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyBehaviour : MonoBehaviour {

	public List<Transform> wayPoints;

	public bool isLightWeakness;
	public bool isWaterWeakness;
	public bool isSoundWeakness;
	public Patrol patrolScript;

	public float baseHealth;
	public float currentHealth;

	public float originalSpeed;
	public float speed;

	public GameObject poofParticle;
	public GameObject crystal1;
	public GameObject bonusCrystal;

	public bool killedWithElement;

	// Use this for initialization
	public void InitializeBehaviour () {

		for (int x = 0; x < wayPoints.Count; x++) {
			if (Random.Range (0, 2) == 0 && x < wayPoints.Count - 1) {

				x++;
			}
			patrolScript.wayPoints.Add(wayPoints[x]);
		}
	}
	
	// Update is called once per frame
	void Update () {

		if (currentHealth <= 0) {

			poofParticle.SetActive (true);
			poofParticle.transform.parent = null;

			crystal1.SetActive (true);
			crystal1.transform.parent = null;

			if (killedWithElement) {
				bonusCrystal.SetActive (true);
				bonusCrystal.transform.parent = null;
			}

			Destroy (gameObject);
		}
	
	}
}
