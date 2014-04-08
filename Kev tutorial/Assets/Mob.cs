using UnityEngine;
using System.Collections;

public class Mob : MonoBehaviour {

	public float speed;
	public float range;
	public bool isInRange;

	public Transform player;

	public CharacterController controller;
	public AnimationClip run;
	public AnimationClip idle;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
//		Debug.Log (inRange());
		if (!inRange())
		{
			chase ();
		}

		else
		{
			animation.CrossFade(idle.name);
		}
	}

	//Check if player is in range
	bool inRange() 
	{
		return (Vector3.Distance(transform.position, player.position) < range);
	}

	void chase()
	{
		transform.LookAt (player.position);
		controller.SimpleMove (transform.forward * speed);
		animation.CrossFade (run.name);
	}

	void OnMouseOver()
	{
		player.GetComponent<Combat>().opponent = gameObject;






	}
}
