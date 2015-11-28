using System;
using UnityEngine;

public class MusicManager : MonoBehaviour {

    public static event Action OnIdle, OnLux, OnNox;
    static MusicManager Instance { get; set; }

    public static void Idle() {
        Change(Instance.idle);
        OnIdle();
    }
    public static void Lux() {
        Change(Instance.lux);
        OnLux();
    }
    public static void Nox() {
        Change(Instance.nox);
        OnNox();
    }
    public static void Win()
    {
        Stop();
    }
    static void Change(AudioSource next) {
        next.time = Instance.current.time;
        Instance.current.Stop();
        next.Play();
        Instance.current = next;
        Instance.transition.Play();
    }
    static void Stop()
    {
        Instance.current.Stop();
    }

    public AudioSource idle, lux, nox, transition;
    AudioSource current;

    void Awake() {
        Instance = this;
        current = idle;
	}
	
}
