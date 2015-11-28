using UnityEngine;

public class BackgroundManager : MonoBehaviour {

    float time, timeAnim = .4f, nextAlpha, currentAlpha;

    public GameObject BGIdle, BGLux, BGNox;

    GameObject current, next;

    void OnEnable() { MusicManager.OnIdle += OnIdle; MusicManager.OnLux += OnLux; MusicManager.OnNox += OnNox; }
    void OnDestroy() { MusicManager.OnIdle -= OnIdle; MusicManager.OnLux -= OnLux; MusicManager.OnNox -= OnNox; }

    void Start()
    {
        BGIdle = GameObject.Find("BG_Idle");
        BGLux = GameObject.Find("BG_Lux");
        BGNox = GameObject.Find("BG_Nox");

        BGLux.SetActive(false);
        BGNox.SetActive(false);

        current = BGIdle;
        time = 0;
    }

    void OnIdle() { if (current != BGIdle) Change(BGIdle); }
    void OnLux() { if (current != BGLux) Change(BGLux); }
    void OnNox() { if (current != BGNox) Change(BGNox); }

    void Change(GameObject next) {
        next.SetActive(true);
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

            foreach (SpriteRenderer sprite in current.GetComponentsInChildren<SpriteRenderer>())
                sprite.material.color = new Color(1, 1, 1, (time / timeAnim));

            foreach (SpriteRenderer sprite in next.GetComponentsInChildren<SpriteRenderer>())
                sprite.material.color = new Color(1, 1, 1, ((timeAnim - time) / timeAnim));

            if (time == 0) {
                current.SetActive(false);
                foreach (SpriteRenderer sprite in next.GetComponentsInChildren<SpriteRenderer>())
                    sprite.GetComponent<SpriteRenderer>().sortingOrder = -1;
                current = next;
                next = null;
            }
        }
    }
}
