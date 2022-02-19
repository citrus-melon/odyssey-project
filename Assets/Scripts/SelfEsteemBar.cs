using UnityEngine;
using UnityEngine.UI;

public class SelfEsteemBar : MonoBehaviour {
    public Slider bar;
    public EndScreen endScreen;
    public int value = 1;

    void Start() {
        Refresh();
    }

    private void Refresh() {
        bar.value = value;
        if (value <= 0) {
            endScreen.Lose();
        }
    }

    public int Increment(int amount) {
        value += amount;
        Refresh();
        return value;
    }

    public void Set(int value) {
        this.value = value;
        Refresh();
    }
}