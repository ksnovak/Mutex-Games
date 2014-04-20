using UnityEngine;
using System.Collections;

public class Combat : MonoBehaviour {

	public GameObject opponent;
	public AnimationClip attack;
	public AnimationClip dieClip;

<<<<<<< HEAD
	public int maxHealth;
=======
>>>>>>> fd39a36777d512b4454cecd32b316c2b03e34627
	public int health;
	public int damage;
	private double impactLength;

	public double impactTime;
	public bool impacted;
	public float combatEscapeTime;
	public float countDown;

	public float range;

	bool startedDeath;
	bool endedDeath;

	// Use this for initialization
	void Start () {
		impactLength = (animation[attack.name].length * impactTime);
		health = maxHealth;

	}


	// Update is called once per frame
	void Update () {
//		Debug.Log (opponent);
		if (Input.GetKey (KeyCode.Space) && inRange() && !isDead())
		{
			animation.Play(attack.name);
			ClickToMove.attack = true;

			if (opponent != null)
			{
				transform.LookAt (opponent.transform);
			}
		}

		if (animation[attack.name].time > 0.9 * animation[attack.name].length)
		{
			ClickToMove.attack = false;
			impacted = false;
		}
		impact ();
		die ();
	}

	void impact()
	{
		if (opponent != null && animation.IsPlaying(attack.name)  && !impacted)
		{
			if (animation[attack.name].time > impactLength && (animation[attack.name].time < 0.9 * animation[attack.name].length))
			{
				countDown = combatEscapeTime;
				CancelInvoke("checkCombat");
				InvokeRepeating ("checkCombat", 0, 1);
				Profiler.BeginSample ("Test");
				opponent.GetComponent<Mob>().getHit(damage);
				impacted = true;
				Profiler.EndSample ();
			}
		}
	}
	
	void checkCombat() {
		countDown  = countDown - 1;
		if (countDown == 0)
		{
			CancelInvoke ("checkCombat");
			opponent = null;
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
<<<<<<< HEAD
			//revive();
=======
			revive();
>>>>>>> fd39a36777d512b4454cecd32b316c2b03e34627
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






































