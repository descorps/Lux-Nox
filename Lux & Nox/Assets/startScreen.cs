using UnityEngine;
using System.Collections;

public class startScreen : MonoBehaviour {

    [SerializeField]
    private GameObject loading;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        if ((Input.GetButtonDown("Start1") || Input.GetButtonDown("Start2")))
        {
            Application.LoadLevelAsync(1);
            loading.SetActive(true);
        }
        if (Input.GetButtonDown("ESC"))
        {
            Application.Quit();
        }
    }
}
