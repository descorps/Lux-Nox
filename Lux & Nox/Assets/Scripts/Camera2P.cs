using UnityEngine;

public class Camera2P : MonoBehaviour {

    [SerializeField]
    Transform player1, player2;

	[SerializeField]
	private float minY = 0f;
	
	[SerializeField]
	private float distanceMax = 10f;

	[SerializeField]
	private float sizeMin = 10.5f;

	Vector3 middle;

	public float xMargin = 7f;		// Distance delimiting the borders of the camera relatively to the players' positions
	public float xSmooth = 2f;		// How smoothly the camera catches up with it's target movement in the x axis.
	public Vector2 maxXAndY;		// The maximum x and y coordinates the camera can have.
	public Vector2 minXAndY;		// The minimum x and y coordinates the camera can have.

    void SetCameraPos() {
        Vector3 middle = (player1.position + player2.position) * 0.5f;

        Camera.main.transform.position = new Vector3(
            Mathf.Lerp(transform.position.x, middle.x, xSmooth * Time.deltaTime),
			Mathf.Max(middle.y, minY),
            Camera.main.transform.position.z
        );
    }

	void TrackPlayers ()
	{		
		// multiplying by 0.5, because the ortographicSize is actually half the height
		float width = Mathf.Abs(player1.position.x - player2.position.x) * 0.5f + 2 * xMargin;

		if (Mathf.Abs (player1.position.x - player2.position.x) <= distanceMax)
			Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize,
			                                          sizeMin,
			                                          - xSmooth * Time.deltaTime);
		// ... the target x coordinate should be a Lerp between the camera's current x position and the player's current x position.
		else
			Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize,
		                            width * Screen.height / Screen.width, 
		                            xSmooth * Time.deltaTime);

	}

	void Start() {
		// Camera.main.orthographicSize = sizeMin;
	}

	void FixedUpdate ()
	{
		SetCameraPos();
		TrackPlayers ();
	}


    void Update() {
    }
}