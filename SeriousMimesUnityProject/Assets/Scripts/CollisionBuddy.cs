using UnityEngine;
using System.Collections;

public class CollisionBuddy : MonoBehaviour {

	public Transform focusObject;
	public WaveGun beamGun;
	public CustomCharacterController controller;
	public float stunTime;

	public Renderer[] mainCharacterRender; 

	private bool isRunning;
	private ItemPickup crystal;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = focusObject.position;	
	}

	public void OnTriggerEnter(Collider collide) 
	{
		if (collide.gameObject.CompareTag ("Enemy")) {
			
			controller.isStunned = true;

			if (!isRunning) {
				StartCoroutine (Stunning ());
				isRunning = true;
			}

		}

		if(collide.CompareTag("Item"))
		{
			crystal = collide.GetComponent<ItemPickup> ();

			if (crystal.isLight)
				beamGun.lightAmmo += crystal.ammoValue;

			if (crystal.isWater)
				beamGun.waterAmmo += crystal.ammoValue;

			if (crystal.isWater)
				beamGun.waterAmmo += crystal.ammoValue;

			Destroy (collide.gameObject);
		}
	}

	public IEnumerator Stunning()
	{
		WaitForSeconds waitEnd = new WaitForSeconds (.2f);

		float timer = 0;

		while (timer < stunTime) {

			timer += .2f;

			for (int x = 0; x < mainCharacterRender.Length; x++) {

				mainCharacterRender [x].enabled = !mainCharacterRender [x].enabled;
			}

			yield return waitEnd;
		}

		for (int x = 0; x < mainCharacterRender.Length; x++) {

			mainCharacterRender [x].enabled = true;
		}

		controller.isStunned = false;
		isRunning = false;
	}
}
