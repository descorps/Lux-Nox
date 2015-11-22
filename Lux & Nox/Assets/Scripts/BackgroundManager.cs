using UnityEngine;

public class BackgroundManager : MonoBehaviour {
    float time, timeAnim = .4f, nextAlpha, currentAlpha;
    public GameObject idle, lux, nox;
    GameObject current, next;
    void OnEnable() { MusicManager.OnIdle += OnIdle; MusicManager.OnLux += OnLux; MusicManager.OnNox += OnNox; }
    void Start() { current = lux; time = 0; }
    void OnIdle() { if (current != idle) Change(idle); }
    void OnLux() { if (current != lux) Change(lux); }
    void OnNox() { if (current != nox) Change(nox); }
    void Change(GameObject next) {
        next.GetComponent<SpriteRenderer>().sortingOrder = -2;
        next.SetActive(true);
        this.next = next;
        time = timeAnim;

        // TODO: when object is recupered under timeAnim ms
        nextAlpha = next.GetComponent<SpriteRenderer>().material.color.a;
        currentAlpha = current.GetComponent<SpriteRenderer>().material.color.a;
    }
    void Update() {
        if (next != null) {
            time = Mathf.Max(0, time - Time.deltaTime);

            Color cc = current.GetComponent<SpriteRenderer>().material.color;
            Color cn = next.GetComponent<SpriteRenderer>().material.color;

            current.GetComponent<SpriteRenderer>().material.color = new Color(cc.r, cc.g, cc.b, (time / timeAnim));
            next.GetComponent<SpriteRenderer>().material.color = new Color(cn.r, cn.g, cn.b, ((timeAnim - time) / timeAnim));

            if (time == 0) { current.SetActive(false); current = next; next = null; current.GetComponent<SpriteRenderer>().sortingOrder = -1; }
        }
    }
}
