using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public float moveSpeed = 10;
	public float rotateSpeed = 20;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		//Determine how much to move the player. For the GetAxis, the value will be between -1 and 1. Up/Right are positive, Down/Left are negative
		float moveForward = moveSpeed * Time.smoothDeltaTime * Input.GetAxis ("Vertical"); 
		float moveRight = moveSpeed * Time.smoothDeltaTime * Input.GetAxis ("Horizontal"); 

		//This actually moves the character. Add the L/R motion to the U/D motion, and translate by that.
		transform.Translate ((Vector3.forward * moveForward) + (Vector3.right * moveRight));

	}
}
