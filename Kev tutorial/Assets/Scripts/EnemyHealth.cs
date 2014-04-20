using UnityEngine;
using System.Collections;
public class EnemyHealth : MonoBehaviour {

	public Combat player;
	public Mob target;
	public float healthPercentage;

	public Texture2D frame;
	public Rect framePosition;

	public float xOffset;
	public float yOffset;

	public Texture2D healthBar;
	public Rect healthBarPosition;

	void Start () {
		
	}
	
	void Update () {
		if (player.opponent != null)
		{
			target = player.opponent.GetComponent<Mob>();
			healthPercentage = (float)target.health / target.maxHealth;
		}
		else
		{
			target = null;
			healthPercentage = 0;
		}
	}

	void OnGUI(){
		if (target != null)
			drawFrames();



	}

	void drawFrames() {
		/*Frame:
		 * 	Width: We want it to take 40% of the screen width
		 * 	Height: The texture has a 10:1 ratio. We want to preserve THAT, so we are basing the frame height off of the screen WIDTH (not screen height)
		 *  X: This centers it.
		 *  Y: We want it to start about 5% down the screen. 
		 *  
		 *Bar:
		 *	Width: This should fit inside the frame. It starts as ~80% of the frame width. That gets decremented as the target loses health;
		 *	Height: Similarly, it should fit inside the frame, and is ~40% the height of the frame.
		 *	X: We need to offset the X a bit, since the bar starts slightly inside. It starts ~14% into the frame
		 *	Y: This needs to be offset too, to be inside the frame. Note this is scaling based off of WIDTH again.
		 */

		framePosition.width = Screen.width * .4f;
		framePosition.height = Screen.width * .04f;
		framePosition.x = (Screen.width - framePosition.width)/2;
		framePosition.y = Screen.height * .05f;
		
		healthBarPosition.width = framePosition.width * .79f * healthPercentage;
		healthBarPosition.height = framePosition.height * .41f;
		healthBarPosition.x = framePosition.x + (framePosition.x * 0.139f);		//Screen.width * 0.042f;
		healthBarPosition.y = framePosition.y + (framePosition.y * 0.34f);		//Screen.width * 0.012f;
		
		GUI.DrawTexture(framePosition, frame);
		GUI.DrawTexture(healthBarPosition, healthBar);
	}
}
