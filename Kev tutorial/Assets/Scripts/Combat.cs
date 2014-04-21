using UnityEngine;
using System.Collections;

public class Combat : MonoBehaviour {

	public GameObject opponent;
	public AnimationClip attack;
	public AnimationClip dieClip;

	public int maxHealth;
	public int health;
	public int damage;
	private double impactLength;

	public double impactTime;
	public bool impacted;
	public bool inAction;
	public float combatEscapeTime;
	public float countDown;

	public float range;

	bool startedDeath;
	bool endedDeath;

	public bool specialAttack;

	// Use this for initialization
	void Start () {
		impactLength = (animation[attack.name].length * impactTime);
		health = maxHealth;

	}


	// Update is called once per frame
	void Update () {
		if (!specialAttack && Input.GetKeyDown (KeyCode.Space))
		{
			inAction = true;
		}

		if (inAction)
		{
			if (attackFunctions(0, 1, KeyCode.Space, null, 0, true))
			{
			}
			else
			{
				inAction = false;
			}
		}

		die ();
	}

	public bool attackFunctions(int stunSeconds, double scaledDamage, KeyCode key, GameObject particleEffect, int projectile, bool opponentBased)
	{
		if (opponentBased)
		{
			if (Input.GetKey (key) && inRange())
			{
				animation.Play(attack.name);
				ClickToMove.attack = true;
				
				if (opponent != null)
				{
					transform.LookAt (opponent.transform.position);
				}
			}
		}
		else
		{
			if (Input.GetKey (key))
			{
				animation.Play(attack.name);
				ClickToMove.attack = true;
				transform.LookAt (ClickToMove.cursorPosition);
			}
		}
		if (animation[attack.name].time > 0.9 * animation[attack.name].length)
		{
			ClickToMove.attack = false;
			impacted = false;
			if (specialAttack)
			{
				specialAttack = false;
			}
			return false;
		}
		impact (stunSeconds, scaledDamage, key, particleEffect, projectile, opponentBased);
		return true;
	}

	public void resetAttackFunction() 
	{
		ClickToMove.attack = false;
		impacted = false;
		animation.Stop(attack.name);
	}

	void stunMob()
	{

	}

	void impact(int stunSeconds, double scaledDamage, KeyCode key, GameObject particleEffect, int projectile, bool opponentBased)
	{
		if ((opponentBased || opponent != null) && animation.IsPlaying(attack.name)  && !impacted)
		{
			if (animation[attack.name].time > impactLength && (animation[attack.name].time < 0.9 * animation[attack.name].length))
			{
				countDown = combatEscapeTime + 2;
				CancelInvoke("checkCombat");
				InvokeRepeating ("checkCombat", 0, 1);
				if (opponentBased)
				{
					opponent.GetComponent<Mob>().getHit(damage*scaledDamage);
					opponent.GetComponent<Mob>().getStun(stunSeconds);
				}

				Quaternion rot = transform.rotation;
				rot.x = 0;
				rot.z = 0;

				if (projectile > 0)
				{
					Debug.Log ("hi " + projectile);
					Instantiate(Resources.Load("Projectile"), new Vector3(transform.position.x, transform.position.y + 1.5f, transform.position.z), rot);
				}

				if (particleEffect != null)
					Instantiate (particleEffect, new Vector3(opponent.transform.position.x, opponent.transform.position.y + 1.5f, opponent.transform.position.z), Quaternion.identity);

				impacted = true;
			}
		}
	}
	
	void checkCombat() {
		countDown  = countDown - 1;
		if (countDown == 0)
		{
			CancelInvoke ("checkCombat");
			//opponent = null;
		}
	}

	public void getHit(int damage)
	{
		health -= damage;
		if (health < 0)
		{
			health = 0;
			die ();
		}
	}

	public bool isDead()
	{
		return (health <= 0);
	}

	void die() {
		if (isDead () && !endedDeath)
		{
			if (!startedDeath)
			{
				animation.Play(dieClip.name);
				startedDeath = true;
				ClickToMove.dead = true;
			}

			else if (startedDeath && !animation.IsPlaying(dieClip.name))
			{
				Debug.Log ("Death is done");
				endedDeath = true;
			}
		}
		else if (isDead() && endedDeath)
		{
			//revive();
		}
	}

	void revive()
	{
		health = 100;
		startedDeath = false;
		endedDeath = false;
		ClickToMove.dead=false;
	}


	bool inRange()
	{
		if (opponent != null)
			return (Vector3.Distance(opponent.transform.position, transform.position) <= range);
		return false;
	}
}






































