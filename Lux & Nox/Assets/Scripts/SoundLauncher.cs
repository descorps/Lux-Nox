using UnityEngine;

public class SoundLauncher : MonoBehaviour {

    [SerializeField]
    AudioSource jump, dash, collectFake, collectTop;
	
	public void Jump() { jump.Play(); }
    public void Dash() { dash.Play(); }
    public void CollectFake() { collectFake.Play();  }
    public void CollectTop() { collectTop.Play(); }
}
