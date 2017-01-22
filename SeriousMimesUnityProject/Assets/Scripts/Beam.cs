using UnityEngine;
using System.Collections;

public class Beam : MonoBehaviour {

	public WaveGun gunThing;

	public EnemyBehaviour enemy;
	public DefenseTurret turret;

	public float damageRate;

	public float fillRate;

	// Use this for initialization

	void OnTriggerStay(Collider collide)
	{
		if (collide.CompareTag ("Enemy")) {

			enemy = collide.GetComponent<EnemyBehaviour> ();

			if ((enemy.isLightWeakness && gunThing.isLight) || (enemy.isSoundWeakness && gunThing.isSound) || (enemy.isWaterWeakness && gunThing.isWater)) {

				enemy.currentHealth -= damageRate * 2 * Time.deltaTime;

			}else
				enemy.currentHealth -= damageRate * Time.deltaTime;

		}

		if (collide.CompareTag ("Turret")) {

			if ((turret.isLight && gunThing.isLight) || (turret.isSound && gunThing.isSound) || (turret.isWater && gunThing.isWater)) {

				turret.currentAmmo += fillRate * Time.deltaTime;

			} else {
				turret.currentAmmo = 0;
			}



			if (gunThing.isLight)
				turret.isLight = true;

			if (gunThing.isSound)
				turret.isSound = true;

			if (gunThing.isWater)
				turret.isWater = true;
		}

	}
}
