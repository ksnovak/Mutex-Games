using UnityEngine;
using System.Collections;

public class Mob : MonoBehaviour {

	public float speed;		//How fast you can move
	public float range;		//The range at which the player is close enough to hit
	public bool isInRange; //If the player is close enough to hit

	//References to the player
	public Transform player;	//The player's location
	private Combat opponent;	//The player's combat script
	public LevelSystem playerLevel; //Used to increment the player's XP after the mob dies


	public CharacterController controller; //Controls motion for the mob

	public AnimationClip attackClip;
	public AnimationClip run;
	public AnimationClip idle;
	public AnimationClip die;

	public int maxHealth; 
	public int health;
	public int expworth; //How much XP will be given to the player when this mob dies
	public int damage; //How much damage you deal in a hit
	public float impactTime = 0.46f;	//At what part of the attack animation does a "hit" actually occur
	private bool impacted;	//If the hit has occurred yet or not.

	public int stunTime;

	// Use this for initialization
	void Start () {
		health = maxHealth;
		opponent = player.GetComponent<Combat>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (!isDead())
		{
			if (stunTime <= 0)
			{

				if (!inRange())
				{
					chase();
				}
				else
				{
					animation.Play(attackClip.name);
					attack ();

					if (animation[attackClip.name].time > 0.9*animation[attackClip.name].length)
					{
						impacted = false;
					}
				}
			}
			else 
			{ 
			
			}
		}
		else 
		{
			dieMethod ();
		}
	}

	void attack() 
	{
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

	public void getHit(double damage)
	{
		health -= (int)damage;

		if (health < 0)
		{
			health = 0;
		}

		//Debug.Log (health);
	}

	public void getStun(int seconds)
	{
		CancelInvoke("stunCountDown");
		Debug.Log ("getStun!");
		stunTime = seconds;
		InvokeRepeating ("stunCountDown", 0f, 1f);
	}

	void stunCountDown()
	{
		Debug.Log("countdown!");
		stunTime --;
		if (stunTime <= 0)
		{
			CancelInvoke("stunCountDown");
		}
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
			playerLevel.currentExp += expworth;
			Destroy (gameObject);
		}
	}
}
