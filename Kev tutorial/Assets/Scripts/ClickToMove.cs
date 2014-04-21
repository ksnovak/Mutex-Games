using UnityEngine;
using System.Collections;

public class ClickToMove : MonoBehaviour {

	private Vector3 position;
	public float speed;

	public CharacterController controller;

	public AnimationClip run;
	public AnimationClip idle;

	public static bool attack;
	public static bool dead;

	public static Vector3 cursorPosition;

	// Use this for initialization
	void Start () {
		position = transform.position;
	}
	
	// Update is called once per frame
	void Update () 
	{
		locateCursor();

		if (!attack && !dead)
		{
			if(Input.GetMouseButton (0))
			{
				//Find where the click actually is on the ground
				locatePosition();
			}
			//Move the player to the position
			moveToPosition();
		}
	}

	void locatePosition() 
	{
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;

		if (Physics.Raycast(ray, out hit, 1000))
		{
			if (hit.collider.tag != "Player" && hit.collider.tag != "Enemy")
			{
				position = hit.point;
			}
		}
	}


	void locateCursor() 
	{
		RaycastHit hit;
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		
		if (Physics.Raycast(ray, out hit, 1000))
		{
			cursorPosition = hit.point;
		}
		
	}


	void moveToPosition() 
	{
		//Game object is moving
		if (Vector3.Distance (transform.position, position) > 1)
	    {
			Quaternion newRotation = Quaternion.LookRotation (position-transform.position, Vector3.forward); 

			newRotation.x = 0f;
			newRotation.z = 0f;

			transform.rotation = Quaternion.Slerp (transform.rotation, newRotation, Time.deltaTime * 10);

			controller.SimpleMove(transform.forward * speed);

			animation.CrossFade(run.name);
		}
		else 
		{
			animation.CrossFade (idle.name);
		}
	}
}
