using UnityEngine;
using System.Collections;

public class Collect : MonoBehaviour {

	private Item item;
    SoundLauncher soundLauncher;

	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.tag == "Player" && transform.parent == null) {
			Item playerItem = other.GetComponentInChildren<Item> ();
			if (playerItem == null) {
				transform.parent = other.gameObject.transform;
				transform.localPosition = new Vector3 (0, 0.8f, 0);
				transform.localScale /= 1.5f;
                if (item.itemType == ItemType.FAKEOBJECT) soundLauncher.CollectFake(); else soundLauncher.CollectTop();
			} else if (playerItem.itemType == ItemType.FAKEOBJECT && item.itemType == ItemType.TOPOBJECT) {
				playerItem.transform.parent = null;
				playerItem.transform.localScale *= 1.5f;
				transform.parent = other.gameObject.transform;
				transform.localPosition = new Vector3 (0, 0.8f, 0);
				transform.localScale /= 1.5f;
                soundLauncher.CollectTop();
            }
		} 
		/*else if (other.gameObject.tag == "Player" && transform.parent != null)
			other.GetComponent<PlayerControl>().Collect(item);*/
	}
    
    // Use this for initialization
    void Start () {
		item = gameObject.GetComponent<Item> ();
        soundLauncher = GetComponent<SoundLauncher>();
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
