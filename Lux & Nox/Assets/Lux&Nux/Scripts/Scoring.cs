using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Scoring : MonoBehaviour {

	private Slider slider;
	[SerializeField]
	private PlayerName playerName;

	// Use this for initialization
	void Start () {
		if (playerName == PlayerName.LUX)
			slider = GameObject.Find ("SliderLux").GetComponent<Slider>();
		else if (playerName == PlayerName.NOX)
			slider = GameObject.Find ("SliderNox").GetComponent<Slider>();
		slider.maxValue = 10;
		slider.value = 0;
	}
	
	// Update is called once per frame
	void Update () {
		
		Item playerItem = gameObject.GetComponentInChildren<Item>();
		if (playerItem != null && playerItem.itemType == ItemType.TOPOBJECT) {
			float vps = playerItem.valuePerSecond;
			slider.value += vps * Time.deltaTime;
		}

	}
}
