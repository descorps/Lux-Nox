using UnityEngine;
using System.Collections;

public class Collect : MonoBehaviour {

	private Item item;

	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.tag == "Player") {
			Item playerItem = other.GetComponentInChildren<Item>();
			if (playerItem == null) {
				transform.parent = other.gameObject.transform;
				transform.localPosition = new Vector3(0,0.8f,0);
			}
			else if (playerItem.itemType == ItemType.FAKEOBJECT && item.itemType == ItemType.TOPOBJECT) {
				playerItem.transform.parent = null;
				this.transform.parent = other.gameObject.transform;
				this.transform.localPosition = new Vector3(0,0.8f,0);
			}
		}
	}

	// Use this for initialization
	void Start () {
		item = gameObject.GetComponent<Item> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
