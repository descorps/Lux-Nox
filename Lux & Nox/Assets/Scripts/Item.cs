using UnityEngine;
using System.Collections;

public class Item : MonoBehaviour {

	[SerializeField]
	public ItemType itemType;
	[SerializeField]
	public float valuePerSecond;

	// Use this for initialization
	void Start () {
		gameObject.GetComponent<BoxCollider2D>().enabled = true;
	}
	
	// Update is called once per frame
	void Update () {
		if(transform.parent == null)
			gameObject.GetComponent<BoxCollider2D>().enabled = true;
		else
			gameObject.GetComponent<BoxCollider2D>().enabled = false;

	}
}
