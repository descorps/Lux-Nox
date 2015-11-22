using UnityEngine;
using System.Collections;

public class collisionDash : MonoBehaviour {
	[SerializeField]
	private Vector2 dashLeftForce = new Vector2(-5000,500), dashRightForce = new Vector2(5000,500);

	public void OnTriggerEnterOrExit2D(Collider2D other) {
		if (transform.parent.GetComponent<PlayerControl>().isDashing && other.tag == "Player"){
			Item playerItem = transform.parent.GetComponentInChildren<Item> ();
			Item otherItem = other.GetComponentInChildren<Item> ();
			if(playerItem == null || otherItem == null) {
			}
			else if (playerItem.itemType == ItemType.FAKEOBJECT) {
				if(transform.parent.GetComponent<PlayerControl>().dashLeft)
					otherItem.transform.parent.gameObject.GetComponent<Rigidbody2D>().AddForce(dashLeftForce);
				else
					otherItem.transform.parent.gameObject.GetComponent<Rigidbody2D>().AddForce(dashRightForce);
				playerItem.transform.parent = null;
				playerItem.transform.localScale *= 1.5f;
				otherItem.transform.parent = transform.parent.transform;
				otherItem.transform.localPosition = new Vector3 (0, 0.8f, 0);
                if (otherItem.itemType == ItemType.TOPOBJECT) {
                    if (transform.parent.name == "Lux") MusicManager.Lux(); else MusicManager.Nox();
                }
			} 
			else if (playerItem.itemType == ItemType.TOPOBJECT) {
				if(transform.parent.GetComponent<PlayerControl>().dashLeft)
					otherItem.transform.parent.gameObject.GetComponent<Rigidbody2D>().AddForce(dashLeftForce);
				else
					otherItem.transform.parent.gameObject.GetComponent<Rigidbody2D>().AddForce(dashRightForce);
				otherItem.transform.parent = null;
				otherItem.transform.localScale *= 1.5f;
			}
		}

	}

	public void OnTriggerEnter2D(Collider2D other) {
		OnTriggerEnterOrExit2D(other);
	}
	public void OnTriggerExit2D(Collider2D other) {
		OnTriggerEnterOrExit2D(other);
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
