using UnityEngine;

public class ImaginarySpeed : MonoBehaviour {

    [SerializeField]
    float speedFactor;

    Collider2D collider2D;

	void Start() {
        collider2D = GetComponent<Collider2D>();
    }

	void FixedUpdate() {
        Vector2 v = collider2D.attachedRigidbody.velocity * speedFactor;
        transform.localPosition += new Vector3(v.x, v.y, 0);
	}
}
