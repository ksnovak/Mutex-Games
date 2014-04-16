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
	public AnimationClip die;

	private int health;

	// Use this for initialization
	void Start () {
		health = 100;
	}
	
	// Update is called once per frame
	void Update () {
//		Debug.Log (inRange());

		if (!isDead())
		{
			if (!inRange())
			{
				chase();
			}

			else
			{
				animation.CrossFade(idle.name);
			}
		}
		else
		{
			dieMethod ();
		}
	}

	//Check if player is in range
	bool inRange() 
	{
		return (Vector3.Distance(transform.position, player.position) < range);
	}

	public void getHit(int damage)
	{
		health = health - damage;

		if (health < 0)
		{
			health = 0;
		}

		//Debug.Log (health);
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

	bool isDead()
	{
		return (health <= 0);
	}

	void dieMethod()
	{
		animation.Play(die.name);
		if (animation[die.name].time > animation[die.name].length*0.9)
		{
			Destroy (gameObject);
		}
	}
}
