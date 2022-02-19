using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class ResponsePicker : MonoBehaviour
{
    public ResponseOption optionPrefab;
    public int? selected = null;
    public string selectedContent = "";

    public void Prompt(params string[] options) {
        selected = null;

        foreach (Transform existingOption in this.transform) {
            Destroy(existingOption.gameObject);
        }

        for (int i = 0; i < options.Length; i++) {
            ResponseOption optionObject = Instantiate(optionPrefab, this.transform);
            optionObject.SetInfo(this, i, options[i]);
        }

        this.gameObject.SetActive(true);
    }

    public void OnSelect(int index, string content) {
        selected = index;
        selectedContent = content;
        this.gameObject.SetActive(false);
    }
}
