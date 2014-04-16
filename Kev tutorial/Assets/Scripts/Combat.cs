using UnityEngine;
using System.Collections;

public class Combat : MonoBehaviour {

	public GameObject opponent;
	public AnimationClip attack;

	public int damage;
	private double impactLength;

	public double impactTime;
	public bool impacted;

	public float range;

	// Use this for initialization
	void Start () {
		impactLength = (animation[attack.name].length * impactTime);
	}
	
	// Update is called once per frame
	void Update () {
//		Debug.Log (opponent);
		if (Input.GetKey (KeyCode.Space) && inRange())
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
	}

	void impact()
	{
		if (opponent != null && animation.IsPlaying(attack.name)  && !impacted)
		{
			if (animation[attack.name].time > impactLength && (animation[attack.name].time < 0.9 * animation[attack.name].length))
			{
				Profiler.BeginSample ("Test");
				opponent.GetComponent<Mob>().getHit(damage);
				impacted = true;
				Profiler.EndSample ();
			}
		}
	}

	bool inRange()
	{
		return (Vector3.Distance(opponent.transform.position, transform.position) <= range);
	}
}






































