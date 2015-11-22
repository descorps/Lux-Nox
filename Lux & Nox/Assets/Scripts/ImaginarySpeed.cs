using UnityEngine;

public class ImaginarySpeed : MonoBehaviour {

    [SerializeField]
    float speedFactor;
    public float SpeedFactor { get { return speedFactor; } }

    Collider2D collider2D;
    Rigidbody2D rigidbody2D;

	void Awake() {
        collider2D = GetComponent<Collider2D>();
        rigidbody2D = GetComponent<Rigidbody2D>();
        enabled = false;
    }

    void OnEnable() {
        rigidbody2D.gravityScale = 1.5f;
    }

    void OnDisable() {
        rigidbody2D.gravityScale = 1;
    }

    void FixedUpdate() {
        Vector2 v = collider2D.attachedRigidbody.velocity * speedFactor;
        transform.localPosition += new Vector3(v.x, v.y, 0);
	}
}
