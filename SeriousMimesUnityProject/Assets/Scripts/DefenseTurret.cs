using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class DefenseTurret : MonoBehaviour {
	
	public bool isLight;
	public bool isWater;
	public bool isSound;

	public float damageRate;

	public EnemyBehaviour enemyGuy;

	public GameObject laser;

	public GameObject focalEnemy;
	public bool alreadyLocked;
	public bool canDisable;

	public float currentAmmo;
	public float maxAmmo;
	public float ammoTimeDecay;
	public float ammoUsage;
	// Use this for initialization
	void Start () {
		laser.SetActive (false);	
	}
	
	// Update is called once per frame
	void Update () {
	
		if (focalEnemy == null) {
			canDisable = true;
			Invoke ("DisableLaser", .1f);
		}
	}

	void OnTriggerExit(Collider collide)
	{
		if (collide.gameObject == focalEnemy) {
			canDisable = true;
			Invoke ("DisableLaser", .1f);
		}
	}
		
	public void DisableLaser()
	{
		if(canDisable)
			laser.SetActive (false);
	}

	void OnTriggerStay(Collider collide)
	{
		if (collide.CompareTag ("Enemy")) {

			canDisable = false;

			if (focalEnemy == null) {
				alreadyLocked = false;
			}

			if (!alreadyLocked) {

				focalEnemy = collide.gameObject;
				enemyGuy = focalEnemy.GetComponent<EnemyBehaviour> ();
				alreadyLocked = true;
			}
			laser.SetActive (true);

			laser.transform.LookAt (focalEnemy.transform.position);

			if ((enemyGuy.isLightWeakness && isLight) || (enemyGuy.isSoundWeakness && isSound) || (enemyGuy.isWaterWeakness && isWater)) {

				enemyGuy.currentHealth -= damageRate * 2 * Time.deltaTime;

			}else
				enemyGuy.currentHealth -= damageRate * Time.deltaTime;

			currentAmmo -= ammoUsage * Time.deltaTime;
		}
	}
}
