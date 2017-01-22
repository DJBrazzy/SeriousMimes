using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;
using UnityStandardAssets.Utility;
public class WaveGun : MonoBehaviour {

	public bool isWater;
	public bool isLight;
	public bool isSound = true;

	public CustomCharacterController characterControl;

	public GameObject waterHose;
	public GameObject lightBeam;
	public GameObject lightRenderer1;
	public GameObject lightRenderer2;
	public GameObject soundWave;

	public CustomCharacterController characterGuy;
	public SimpleMouseRotator mouseRotate;

	public bool isFiring;

	public int switchCounter;
	public float soundMoveSpeed;
	public float lightMoveSpeed;
	public float waterMoveSpeed;
	public float waterRotationSpeed;

	public float maxAmmoValue;

	public float lightAmmo;
	public float waterAmmo;
	public float soundAmmo;

	public float ammoDepletionRate;

	private float waterTimer, lightTimer, soundTimer;

	public float reloadWait;

	public float reloadRate;
	public float reloadThreshhold;


	// Use this for initialization
	void Start () {

		StartCoroutine (ReloadAmmo ());
	
	}

	public IEnumerator ReloadAmmo()
	{
		WaitForEndOfFrame waitEnd = new WaitForEndOfFrame ();

		while (true) {

			waterTimer += Time.deltaTime;
			lightTimer += Time.deltaTime;
			soundTimer += Time.deltaTime;

			if (Input.GetKey (KeyCode.Mouse0)) {
				waterTimer = 0;
				lightTimer = 0;
				soundTimer = 0;

			}

			if (waterTimer > reloadWait && waterAmmo < reloadThreshhold  && !Input.GetKey(KeyCode.Mouse0)) {

				waterAmmo += reloadRate * Time.deltaTime;
			}

			if (lightTimer > reloadWait && lightAmmo < reloadThreshhold && !Input.GetKey(KeyCode.Mouse0) ) {

				lightAmmo += reloadRate * Time.deltaTime;
			}

			if (soundTimer > reloadWait && soundAmmo < reloadThreshhold && !Input.GetKey(KeyCode.Mouse0) ) {

				soundAmmo += reloadRate * Time.deltaTime;
			}

			yield return waitEnd;
		}
	}
	
	// Update is called once per frame
	void Update () {

		//fire
		if (Input.GetKey (KeyCode.Mouse0) && !characterControl.isStunned) {
			isFiring = true;

			if (isWater && waterAmmo > 0) {
				waterHose.SetActive (true);

				if (waterAmmo > 0) {
					waterAmmo -= ammoDepletionRate * Time.deltaTime;
				} else {
					if (waterAmmo < 0)
						waterAmmo = 0;
				}

				characterGuy.moveSpeed = waterMoveSpeed;
				mouseRotate.rotationSpeed = waterRotationSpeed;
			} else {
				if (waterAmmo <= 0) {
					waterHose.SetActive (false);
				}

			}

			if (isLight && lightAmmo > 0) {
				lightBeam.SetActive (true);
				characterGuy.moveSpeed = lightMoveSpeed;
				mouseRotate.canRotate = false;

				if (lightAmmo > 0) {
					lightAmmo -= ammoDepletionRate * Time.deltaTime;
				}
				else {
					if (lightAmmo < 0)
						lightAmmo = 0;
				}
				Invoke ("ChangeLightLayer", .5f);
			} else {
				if (lightAmmo <= 0) {
					lightBeam.SetActive (false);
					lightRenderer1.layer = 1;
					lightRenderer2.layer = 1;
					CancelInvoke ();
				}

			}

			if (isSound && soundAmmo > 0) {
				soundWave.SetActive (true);

				if (soundAmmo > 0) {
					soundAmmo -= ammoDepletionRate * Time.deltaTime;
				}
				else {
					if (soundAmmo < 0)
						soundAmmo = 0;
				}
				characterGuy.moveSpeed = soundMoveSpeed;
			} else {
				if (soundAmmo <= 0) {
					soundWave.SetActive (false);
				}

			}

		} else {

			isFiring = false;
			waterHose.SetActive (false);
			soundWave.SetActive (false);
			lightBeam.SetActive (false);
			lightRenderer1.layer = 1;
			lightRenderer2.layer = 1;
			characterGuy.moveSpeed = characterGuy.originalMoveSpeed;
			mouseRotate.canRotate = true;
			mouseRotate.rotationSpeed = mouseRotate.originalRotationSpeed;
			CancelInvoke ();
		}
		//change equip
		if (Input.GetKeyDown (KeyCode.Mouse1) && !isFiring) {

			switch (switchCounter) {
			case 0:
				isWater = true;
				isLight = false;
				isSound = false;
				break;

			case 1:
				isWater = false;
				isLight = true;
				isSound = false;
				break;

			case 2:
				isWater = false;
				isLight = false;
				isSound = true;
				break;
			}

			switchCounter++;

			if(switchCounter > 2)
				switchCounter=0;

		}
	}

	public void ChangeLightLayer()
	{
		lightRenderer1.layer = 0;
		lightRenderer2.layer = 0;
	}

	public void LightWave()
	{

	}

	public void WaterWave()
	{

	}

	public void SoundWave()
	{

	}

}
