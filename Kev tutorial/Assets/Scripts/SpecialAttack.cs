using UnityEngine;
using System.Collections;

public class SpecialAttack : MonoBehaviour {

	public double damagePercentage;
	public int stunTime;
	public KeyCode key;
	public Combat player;
	public bool inAction;
	public GameObject particleEffect;
	public int projectile;
	public bool opponentBased;

	void Start () {
	
	}

	void Update () {

		if (Input.GetKeyDown (key) && !inAction)
		{
			Debug.Log("Special attack w/" + key.ToString());
			player.resetAttackFunction();
			player.specialAttack = true;
			inAction = true;
		}
		if(inAction)
		{
			if (player.attackFunctions(stunTime, damagePercentage, key, particleEffect, projectile, opponentBased))
			{
			}
			else
			{
				inAction = false;
			}
		}
	}
}
