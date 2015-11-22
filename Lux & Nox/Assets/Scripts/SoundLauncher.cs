using UnityEngine;

public class SoundLauncher : MonoBehaviour {

    [SerializeField]
    AudioSource jump;
	
	public void Jump() {
        jump.Play();
    }
}
