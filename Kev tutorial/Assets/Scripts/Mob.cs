using UnityEngine;
using System.Collections;

public class Mob : MonoBehaviour {

	public float speed;
	public float range;
	public bool isInRange;

	public Transform player;
	private Combat opponent;

	public CharacterController controller;
	public AnimationClip attackClip;
	public AnimationClip run;
	public AnimationClip idle;
	public AnimationClip die;

	public int maxHealth;
	public int health;
	public int damage;
	public float impactTime = 0.46f;
	private bool impacted;


	// Use this for initialization
	void Start () {
		health = maxHealth;
		opponent = player.GetComponent<Combat>();
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
				attack ();
				if (animation[attackClip.name].time > 0.9*animation[attackClip.name].length)
				{
					impacted = false;
				}
			}
		}
		else
		{
			dieMethod ();
		}
	}

	void attack() 
	{
		animation.Play (attackClip.name);
		if (animation[attackClip.name].time > animation[attackClip.name].length * impactTime && !impacted && animation[attackClip.name].time < 0.9*animation[attackClip.name].length)
		{
			impacted = true;
			opponent.getHit(damage);
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
