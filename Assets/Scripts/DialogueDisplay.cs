using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueDisplay : MonoBehaviour
{
    private Queue<string> lineQueue = new Queue<string>();
    public TextMeshProUGUI text;
    public TextMeshProUGUI hint;
    public bool isTalking = false;
    public float animationSpeed = 0.05f;
    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown) AdvanceLine();
    }

    void OnEnable() {
        AdvanceLine();
    }

    private void AdvanceLine() {
        if (lineQueue.Count == 0) {
            isTalking = false;
        } else {
            isTalking = true;
            StopAllCoroutines();
            text.text = "";
            StartCoroutine(AnimateText(lineQueue.Dequeue()));
        }
    }

    public void EnqueueLine(string line) {
        lineQueue.Enqueue(line);
        this.gameObject.SetActive(true);
    }

    IEnumerator AnimateText(string line) {
        hint.color = new Color(0, 0, 0, 0);
        foreach (char c in line) {
			text.text += c;
			yield return new WaitForSeconds(animationSpeed);
		}
        hint.color = new Color(0, 0, 0, 1);
    }
}
