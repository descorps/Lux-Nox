using UnityEngine;

public class Trace : MonoBehaviour {
    
	void Start() {}

    void OnTriggerEnter2D(Collider2D other) {
    }

    void OnTriggerExit2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            Destroy(gameObject); // TODO use Pooling
        }
    }

    void OnTriggerStay2D(Collider2D other) {
        // other.attachedRigidbody.AddForce(other.attachedRigidbody.velocity*10);
    }
}
