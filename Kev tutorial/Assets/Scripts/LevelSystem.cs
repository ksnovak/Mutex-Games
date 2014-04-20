using UnityEngine;
using System.Collections;

public class LevelSystem : MonoBehaviour {

	public int level;
	public int currentExp;
	public int expToLevel;
	public Combat player;


	void Start () {
		level = 0;
		expToLevel = (level * 2) + 1;
	}
	
	void Update () {
		levelUp ();
	}

	void levelUp() {
		if (currentExp >= expToLevel) 
		{
			level++;
			currentExp -= expToLevel;
			levelEffect();
		}
	}

	void levelEffect() {
		player.maxHealth += 100;
		player.health = player.maxHealth;
		player.damage += 10;
		expToLevel = (level * 2) + 1;
	}
}
