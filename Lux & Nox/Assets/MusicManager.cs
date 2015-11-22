using UnityEngine;

public class MusicManager : MonoBehaviour {

    static MusicManager Instance { get; set; }

    public static void Idle() {
        Change(Instance.idle);
    }
    public static void Lux() {
        Change(Instance.lux);
    }
    public static void Nox() {
        Change(Instance.nox);
    }
    static void Change(AudioSource next) {
        next.time = Instance.current.time;
        Instance.current.Stop();
        next.Play();
        Instance.current = next;
    }

    public AudioSource idle, lux, nox;
    AudioSource current;

    void Awake() {
        Instance = this;
        current = idle;
	}
	
}
