using UnityEngine;
using System.Collections;

public class CameraFollowGuy : MonoBehaviour {

	public Transform thePlayer;
	public Vector3 startOffset;
	// Use this for initialization
	void Start () {
		startOffset = transform.position;
		Cursor.visible = false;
	}
	
	// Update is called once per frame
	void Update () {

		transform.position = thePlayer.transform.position + startOffset;
	
	}
}
