using UnityEngine;
using System.Collections;

public class Beam : MonoBehaviour {

	public WaveGun gunThing;

	public float damageRate;

	// Use this for initialization

	void OnTriggerStay(Collider collide)
	{
		EnemyBehaviour enemy;

		if (collide.CompareTag ("Enemy")) {

			enemy = collide.GetComponent<EnemyBehaviour> ();

			if ((enemy.isLightWeakness && gunThing.isLight) || (enemy.isSoundWeakness && gunThing.isSound) || (enemy.isWaterWeakness && gunThing.isWater)) {

				enemy.currentHealth -= damageRate * 2 * Time.deltaTime;

			}else
				enemy.currentHealth -= damageRate * Time.deltaTime;

		}

	}
}
