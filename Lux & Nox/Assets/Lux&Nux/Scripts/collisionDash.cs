using UnityEngine;
using System.Collections;

public class collisionDash : MonoBehaviour {

	public void OnTriggerEnter2D(Collider2D other) {
		if ((transform.parent.GetComponent<PlayerControl>().dashLeft 
		     || transform.parent.GetComponent<PlayerControl>().dashRight) 
		    && other.tag == "Player"){
			Item playerItem = transform.parent.GetComponentInChildren<Item> ();
			Item otherItem = other.GetComponentInChildren<Item> ();
			if(playerItem == null) {
			}
			else if (playerItem.itemType == ItemType.FAKEOBJECT && otherItem != null) {
				playerItem.transform.parent = null;
				otherItem.transform.parent = transform.parent.transform;
				otherItem.transform.localPosition = new Vector3 (0, 0.8f, 0);
			} 
			else if (playerItem.itemType == ItemType.TOPOBJECT && otherItem != null) {
				otherItem.transform.parent = null;
			}
		} 
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
