using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour
{
	public string HorizontalKey, JumpKey, DashKey, LeftDashKey, RightDashKey, VerticalKey, StartKey, EscKey;
    
	[HideInInspector]
	public bool facingRight = true;			// For determining which way the player is currently facing.
	[HideInInspector]
	public bool jump = false;				// Condition for whether the player should jump.
	
	[HideInInspector]
	public bool dashLeft = false;				// Condition for whether the player should dash.
	[HideInInspector]
	public bool dashRight = false;				// Condition for whether the player should dash.
	public bool isDashing = false;
	
	[SerializeField]
	private float dashCooldownRate = 0.5f;	// Cooldown for dashes.
	//[SerializeField]
	//private int airDashesRemaining = 2;		// Number of dashes remaining in the air.
	[SerializeField]
	private float dashForce = 50f;			// Force of a dash.


	public float moveForce = 365f;			// Amount of force added to move the player left and right.
	public float maxSpeed = 5f;				// The fastest the player can travel in the x axis.
	public AudioClip[] jumpClips;			// Array of clips for when the player jumps.
	public float jumpForce = 260f;			// Amount of force added when the player jumps.
	public AudioClip[] taunts;				// Array of clips for when the player taunts.
	public float tauntProbability = 50f;	// Chance of a taunt happening.
	public float tauntDelay = 1f;			// Delay for when the taunt should happen.

	private float dashTimeStamp	= 0f;		// 


	//private int tauntIndex;					// The index of the taunts array indicating the most recent taunt.
	private Transform groundCheck;			// A position marking where to check if the player is grounded.
	private bool grounded = false;          // Whether or not the player is grounded.
                                            //private Animator anim;					// Reference to the player's animator component.
    ImaginarySpeed imaginarySpeed;
    SoundLauncher soundLauncher;

	void Awake()
	{
		// Setting up references.
		groundCheck = transform.Find("groundCheck");
        imaginarySpeed = GetComponent<ImaginarySpeed>();
        soundLauncher = GetComponent<SoundLauncher>();
        //anim = GetComponent<Animator>();
        //Screen.SetResolution(384, 216, true, 60);
    }


	void Update()
	{
		// The player is grounded if a linecast to the groundcheck position hits anything on the ground layer.
		grounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"))
			|| Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Platform"));  

		// If the jump button is pressed and the player is grounded then the player should jump.
		if(Input.GetButtonDown(JumpKey) && grounded)
			jump = true;
		if (Input.GetButtonDown(LeftDashKey) || (Input.GetButtonDown(DashKey) && Input.GetAxis(HorizontalKey) < 0))
			dashLeft = true;
		if (Input.GetButtonDown(RightDashKey) || (Input.GetButtonDown(DashKey) && Input.GetAxis(HorizontalKey) > 0))
			dashRight = true;
        if (Input.GetButtonDown(StartKey) && Time.timeScale == 0) {
            Time.timeScale = 1;
            Application.LoadLevel(0);
            Time.timeScale = 1;
        }
        if (Input.GetButtonDown(EscKey) && Time.timeScale == 0) {
            Application.Quit();
        }
        /*if (Input.GetAxis (VerticalKey) < 0) {
			Physics2D.IgnoreLayerCollision (gameObject.layer, LayerMask.NameToLayer ("Platform"), true);
		} else {
			Physics2D.IgnoreLayerCollision (gameObject.layer, LayerMask.NameToLayer ("Platform"), false);
		}*/
        isDashing = dashLeft || dashRight || (dashTimeStamp != 0 && dashTimeStamp < 0.5);
	}


	void FixedUpdate ()
	{
		// Cache the horizontal input.
		float h = Input.GetAxis(HorizontalKey);

		// The Speed animator parameter is set to the absolute value of the horizontal input.
		//anim.SetFloat("Speed", Mathf.Abs(h));
	

		// If the player is changing direction (h has a different sign to velocity.x) or hasn't reached maxSpeed yet...
		if (/*(h * GetComponent<Rigidbody2D> ().velocity.x <= maxSpeed)*/Mathf.Abs (GetComponent<Rigidbody2D> ().velocity.x) <= maxSpeed /*&& (Mathf.Abs (h) > 0.30f) && (!dashLeft) && (!dashRight)*/) {
			// ... add a force to the player.
			GetComponent<Rigidbody2D> ().AddForce (Vector2.right * h * moveForce);
		}

		// If the player's horizontal velocity is greater than the maxSpeed...
		else if (Mathf.Abs (GetComponent<Rigidbody2D> ().velocity.x) > maxSpeed && !dashRight && !dashLeft) {
			// ... set the player's velocity to the maxSpeed in the x axis.
			GetComponent<Rigidbody2D> ().velocity = new Vector2 (Mathf.Sign (GetComponent<Rigidbody2D> ().velocity.x) * maxSpeed, GetComponent<Rigidbody2D> ().velocity.y);
		}

		// If the player doesn't press the button, the character stops moving instantaneously
		/*else if ((Mathf.Abs (h) <= 0.30f) && (!dashLeft) && (!dashRight)) {
			GetComponent<Rigidbody2D> ().velocity = new Vector2 (0, GetComponent<Rigidbody2D> ().velocity.y);
		}*/

		// If the player should jump...
		if(jump && GetComponent<Rigidbody2D>().velocity.y == 0)
		{
            // Set the Jump animator trigger parameter.
            //anim.SetTrigger("Jump");

            // Play a random jump audio clip.
            //int i = Random.Range(0, jumpClips.Length);
            //AudioSource.PlayClipAtPoint(jumpClips[i], transform.position);
            soundLauncher.Jump();

            // Add a vertical force to the player.
            GetComponent<Rigidbody2D>().AddForce(new Vector2(jumpForce * h, jumpForce));

			// Make sure the player can't jump again until the jump conditions from Update are satisfied.
			jump = false;
			
			if(dashLeft) {
				if(dashTimeStamp + dashCooldownRate < Time.time /*&& airDashesRemaining > 0*/) {
					dashTimeStamp = Time.time;
					//airDashesRemaining--;
					// Add a force to the player aiming for the mouse position.
					GetComponent<Rigidbody2D>().AddForce(new Vector2 (-dashForce, 0), ForceMode2D.Force);
                    soundLauncher.Dash();
				} else {
					dashLeft = false;
				}
			}
			
			if(dashRight) {
				if(dashTimeStamp + dashCooldownRate < Time.time /*&& airDashesRemaining > 0*/) {
					dashTimeStamp = Time.time;
					//airDashesRemaining--;
					// Add a force to the player aiming for the mouse position.
					GetComponent<Rigidbody2D>().AddForce(new Vector2 (dashForce, 0), ForceMode2D.Force);
                    soundLauncher.Dash();
                } else {
					dashRight = false;
				}
			}
		}
		
		if(dashLeft) {
			if(dashTimeStamp + dashCooldownRate < Time.time) {
				dashTimeStamp = Time.time;
				// Add a force to the player aiming for the mouse position.
				GetComponent<Rigidbody2D>().AddForce (new Vector2 (-dashForce, 0), ForceMode2D.Impulse);
                soundLauncher.Dash();
            } else {
				dashLeft = false;
			}
		}
		
		if(dashRight) {
			if(dashTimeStamp + dashCooldownRate < Time.time) {
				dashTimeStamp = Time.time;
				// Add a force to the player aiming for the mouse position.
				GetComponent<Rigidbody2D>().AddForce (new Vector2 (dashForce, 0), ForceMode2D.Impulse);
                soundLauncher.Dash();
            } else {
				dashRight = false;
			}
		}

		// If the input is moving the player right and the player is facing left...
		if(h > 0 && !facingRight)
			// ... flip the player.
			Flip();
		// Otherwise if the input is moving the player left and the player is facing right...
		else if(h < 0 && facingRight)
			// ... flip the player.
			Flip();

        // Dash with high speed : take object control
        /*
        if (imaginarySpeed.enabled && (dashLeft || dashRight)) {
            RaycastHit2D hit = Physics2D.Raycast(
                transform.position + (dashRight ? Vector3.right : Vector3.left),
                (GetComponent<Rigidbody2D>().velocity + (GetComponent<Rigidbody2D>().velocity*imaginarySpeed.SpeedFactor)) * Time.fixedDeltaTime,
                (GetComponent<Rigidbody2D>().velocity * Time.fixedDeltaTime).magnitude,
                LayerMask.GetMask("Dash")
            );
            if (hit.collider != null) {
                GetComponentInChildren<collisionDash>().OnTriggerEnter2D(hit.collider.gameObject.transform.parent.GetComponent<Collider2D>());
                Debug.Log(hit.collider.gameObject.transform.parent.tag);
                // hit.collider.gameObject.transform.parent.GetComponentInChildren<collisionDash>().OnTriggerEnter2D();
            } else {
                Debug.Log("No collider");
            }
        }
        */
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

}
