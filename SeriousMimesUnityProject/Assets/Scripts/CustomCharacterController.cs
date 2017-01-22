using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;
using UnityStandardAssets.Utility; 

public class CustomCharacterController : MonoBehaviour {


	public CharacterController control;
	public Transform m_Cam;                  // A reference to the main camera in the scenes transform
	public Vector3 m_CamForward;             // The current forward direction of the camera
	public Vector3 m_Move;
	public float originalMoveSpeed;
	public float moveSpeed;

	public bool isStunned;
	public float stunTime;
	public Renderer[] mainCharacterRender; 

	private bool isRunning;
	// Use this for initialization
	void Start () {
		control = GetComponent<CharacterController> ();	

	}
		
	private void FixedUpdate()
	{
		if (!isStunned) {
			// read inputs
			float h = CrossPlatformInputManager.GetAxis ("Horizontal");
			float v = CrossPlatformInputManager.GetAxis ("Vertical");

			// calculate move direction to pass to character
			if (m_Cam != null) {
				// calculate camera relative direction to move:
				m_CamForward = Vector3.Scale (m_Cam.forward, new Vector3 (1, 0, 1)).normalized;
				m_Move = v * m_CamForward + h * m_Cam.right;
			} else {
				// we use world-relative directions in the case of no main camera
				m_Move = v * Vector3.forward + h * Vector3.right;
			}

			control.Move (m_Move * moveSpeed);
		}
	}

	public void OnCollisionEnter(Collision collide) 
	{
		print ("bop");
		if (collide.gameObject.CompareTag ("Enemy")) {

			isStunned = true;
			print ("yessir");

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

			timer += .2f;

			for (int x = 0; x < mainCharacterRender.Length; x++) {

				mainCharacterRender [x].enabled = !mainCharacterRender [x].enabled;
			}
						
			yield return waitEnd;
		}

		for (int x = 0; x < mainCharacterRender.Length; x++) {

			mainCharacterRender [x].enabled = true;
		}

		isStunned = false;
		isRunning = true;
	}
}
