using UnityEngine;

public class Trace : MonoBehaviour {

    [SerializeField]
    float time = 5;

	void Start() {
        Invoke("AutoDestroy", time);
    }

    void AutoDestroy() {
        Destroy(gameObject); // TODO use Pooling
    }
    
    void OnTriggerExit2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            AutoDestroy();
        }
    }
}
