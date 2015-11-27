using UnityEngine;

public class BackgroundManager : MonoBehaviour {
    float time, timeAnim = .4f, nextAlpha, currentAlpha;
    public GameObject[] idle, lux, nox;
    GameObject[] current, next;
    void OnEnable() { MusicManager.OnIdle += OnIdle; MusicManager.OnLux += OnLux; MusicManager.OnNox += OnNox; }
    void Start() { current = idle; time = 0; }
    void OnIdle() { if (current != idle) Change(idle); }
    void OnLux() { if (current != lux) Change(lux); }
    void OnNox() { if (current != nox) Change(nox); }
    void Change(GameObject[] next) {
        for (int i = 0; i < next.Length; ++i) {
            next[i].GetComponent<SpriteRenderer>().sortingOrder = -5 - (next.Length - i);
            next[i].SetActive(true);
        }
        this.next = next;
        time = timeAnim;

        // TODO: when object is recupered under timeAnim ms
        // nextAlpha = next.GetComponent<SpriteRenderer>().material.color.a;
        // currentAlpha = current.GetComponent<SpriteRenderer>().material.color.a;
    }
    void Update() {
        if (next != null) {
            time = Mathf.Max(0, time - Time.deltaTime);

            //Color cc = current.GetComponent<SpriteRenderer>().material.color;
            //Color cn = next.GetComponent<SpriteRenderer>().material.color;

            for (int i = 0; i < next.Length; ++i) {
                current[i].GetComponent<SpriteRenderer>().material.color = new Color(1, 1, 1, (time / timeAnim));
                next[i].GetComponent<SpriteRenderer>().material.color = new Color(1, 1, 1, ((timeAnim - time) / timeAnim));
            }

            if (time == 0) {
                for (int i = 0; i < next.Length; ++i) {
                    current[i].SetActive(false);
                    next[i].GetComponent<SpriteRenderer>().sortingOrder = -1 - (next.Length - i);
                }
                current = next;
                next = null;
            }
        }
    }
}
