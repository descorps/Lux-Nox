using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour
{
	public string HorizontalKey, JumpKey;

	[HideInInspector]
	public bool facingRight = true;			// For determining which way the player is currently facing.
	[HideInInspector]
	public bool jump = false;				// Condition for whether the player should jump.

	[HideInInspector]
	public bool dash = false;				// Condition for whether the player should dash.
	
	[SerializeField]
	private float dashCooldownRate = 0.5f;	// Cooldown for dashes.
	[SerializeField]
	private int airDashesRemaining = 2;		// Number of dashes remaining in the air.
	[SerializeField]
	private float dashForce = 1000f;			// Force of a dash.


	public float moveForce = 365f;			// Amount of force added to move the player left and right.
	public float maxSpeed = 5f;				// The fastest the player can travel in the x axis.
	public AudioClip[] jumpClips;			// Array of clips for when the player jumps.
	public float jumpForce = 500f;			// Amount of force added when the player jumps.
	public AudioClip[] taunts;				// Array of clips for when the player taunts.
	public float tauntProbability = 50f;	// Chance of a taunt happening.
	public float tauntDelay = 1f;			// Delay for when the taunt should happen.

	private float dashTimeStamp	= 0f;		// 


	//private int tauntIndex;					// The index of the taunts array indicating the most recent taunt.
	private Transform groundCheck;			// A position marking where to check if the player is grounded.
	private bool grounded = false;			// Whether or not the player is grounded.
	//private Animator anim;					// Reference to the player's animator component.
     

	void Awake()
	{
		// Setting up references.
		groundCheck = transform.Find("groundCheck");
		//anim = GetComponent<Animator>();
		//Screen.SetResolution(384, 216, true, 60);
	}


	void Update()
	{
		// The player is grounded if a linecast to the groundcheck position hits anything on the ground layer.
		grounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));  

		// If the jump button is pressed and the player is grounded then the player should jump.
		if(Input.GetButtonDown(JumpKey) && grounded)
			jump = true;

        if (Input.GetKeyDown(KeyCode.LeftShift)) {
			dash = true;
		}
    }


	void FixedUpdate ()
	{
		// Cache the horizontal input.
		float h = Input.GetAxis(HorizontalKey);

		// The Speed animator parameter is set to the absolute value of the horizontal input.
		//anim.SetFloat("Speed", Mathf.Abs(h));

		// If the player is changing direction (h has a different sign to velocity.x) or hasn't reached maxSpeed yet...
		if ((h * GetComponent<Rigidbody2D> ().velocity.x < maxSpeed)) {
		    //&& (Mathf.Abs (h) > 0.30f) && (!dash)) {
			// ... add a force to the player.
			GetComponent<Rigidbody2D> ().AddForce (Vector2.right * h * moveForce);
			//Debug.Log ("1");
		}

		// If the player's horizontal velocity is greater than the maxSpeed...
		else if (Mathf.Abs (GetComponent<Rigidbody2D> ().velocity.x) > maxSpeed) {
			// ... set the player's velocity to the maxSpeed in the x axis.
			GetComponent<Rigidbody2D> ().velocity = new Vector2 (Mathf.Sign (GetComponent<Rigidbody2D> ().velocity.x) * maxSpeed, GetComponent<Rigidbody2D> ().velocity.y);
			//Debug.Log ("2");
		}

		// If the player doesn't press the button, the character stops moving instantaneously
		//else if ((Mathf.Abs (h) <= 0.30f) && (!dash)) {
		//	GetComponent<Rigidbody2D> ().velocity = new Vector2 (0, GetComponent<Rigidbody2D> ().velocity.y);
			//Debug.Log ("3");
		//}

		// If the input is moving the player right and the player is facing left...
		if(h > 0 && !facingRight)
			// ... flip the player.
			Flip();
		// Otherwise if the input is moving the player left and the player is facing right...
		else if(h < 0 && facingRight)
			// ... flip the player.
			Flip();

		// If the player should jump...
		if(jump)
		{
			// Set the Jump animator trigger parameter.
			//anim.SetTrigger("Jump");

			// Play a random jump audio clip.
			//int i = Random.Range(0, jumpClips.Length);
			//AudioSource.PlayClipAtPoint(jumpClips[i], transform.position);

			// Add a vertical force to the player.
			GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, jumpForce));

			// Make sure the player can't jump again until the jump conditions from Update are satisfied.
			jump = false;

			if(dash) {
				if(dashTimeStamp + dashCooldownRate < Time.time && airDashesRemaining > 0) {
					dashTimeStamp = Time.time;
					airDashesRemaining--;
					// Add a force to the player aiming for the mouse position.
					GetComponent<Rigidbody2D>().AddForce(new Vector2 (Mathf.Sign(Input.mousePosition.x - Screen.width / 2) * dashForce, 0), ForceMode2D.Impulse);
					//Debug.Log(GetComponent<Rigidbody2D> ().velocity.x);
				} else
					dash = false;
			}
		}

		if(dash) {
			if(dashTimeStamp + dashCooldownRate < Time.time) {
				dashTimeStamp = Time.time;
				// Add a force to the player aiming for the mouse position.
				GetComponent<Rigidbody2D>().AddForce (new Vector2 (Mathf.Sign(Input.mousePosition.x - Screen.width / 2) * dashForce, 0), ForceMode2D.Impulse);
				//Debug.Log(GetComponent<Rigidbody2D> ().velocity.x);
			} else
				dash = false;
		}
	}
	
	
	void Flip ()
	{
		// Switch the way the player is labelled as facing.
		facingRight = !facingRight;

		// Multiply the player's x local scale by -1.
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}


	public IEnumerator Taunt()
	{
		// Check the random chance of taunting.
		float tauntChance = Random.Range(0f, 100f);
		if(tauntChance > tauntProbability)
		{
			// Wait for tauntDelay number of seconds.
			yield return new WaitForSeconds(tauntDelay);

			// If there is no clip currently playing.
			/*if(!GetComponent<AudioSource>().isPlaying)
			{
				// Choose a random, but different taunt.
				tauntIndex = TauntRandom();

				// Play the new taunt.
				GetComponent<AudioSource>().clip = taunts[tauntIndex];
				GetComponent<AudioSource>().Play();
			}*/
		}
	}

	/*
	int TauntRandom()
	{
		// Choose a random index of the taunts array.
		int i = Random.Range(0, taunts.Length);

		// If it's the same as the previous taunt...
		if(i == tauntIndex)
			// ... try another random taunt.
			return TauntRandom();
		else
			// Otherwise return this index.
			return i;
	}*/
}
