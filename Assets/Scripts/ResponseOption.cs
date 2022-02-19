using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ResponseOption : MonoBehaviour {
    public Button button;
    public TextMeshProUGUI label;
    public TextMeshProUGUI shortcutLabel;
    private ResponsePicker picker;
    private int id;
    private int visibleId;

    public void SetInfo(ResponsePicker picker, int id, string content) {
        this.picker = picker;
        this.id = id;
        visibleId = id + 1;
        this.shortcutLabel.text = visibleId.ToString();
        this.label.text = content;
    }

    public void OnClick() {
        picker.OnSelect(id, label.text);
    }

    void Update() {
        if (Input.GetKeyDown(visibleId.ToString())) {
            OnClick();
        }
    }
}