using UnityEngine;
using System.Collections;

public class CollisionBuddy : MonoBehaviour {

	public Transform focusObject;
	public CustomCharacterController controller;
	public float stunTime;

	public Renderer[] mainCharacterRender; 

	private bool isRunning;
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
	}

	public IEnumerator Stunning()
	{
		WaitForSeconds waitEnd = new WaitForSeconds (.2f);

		float timer = 0;

		while (timer < stunTime) {
			print ("joey");

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
