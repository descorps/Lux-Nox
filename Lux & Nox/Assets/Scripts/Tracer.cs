using UnityEngine;

public class Tracer : MonoBehaviour {

    [SerializeField]
    GameObject traceChecker;
    
    [SerializeField]
    GameObject trace;

    Collider2D collider2D;
    Collider2D colliderChecker;

    void Start() {
        collider2D = GetComponent<Collider2D>();
        colliderChecker = traceChecker.GetComponent<Collider2D>();
    }
    
	void FixedUpdate() {
        if (!IsTouchingMine()) {
            Instantiate(trace, transform.position, Quaternion.identity); // TODO use Pooling (TrashMan)
        }
	}

    public bool IsTouchingMine() {
        return colliderChecker.IsTouchingLayers(1 << trace.layer);
    }

    public bool IsTouchingEnemy() {
        return collider2D.IsTouchingLayers(1 << gameObject.layer);
    }
}
