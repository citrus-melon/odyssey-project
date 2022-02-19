using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class EndScreen : MonoBehaviour
{
    public Color winColor;

    public Color loseColor;
    public TextMeshProUGUI title;
    public TextMeshProUGUI score;
    public SelfEsteemBar esteem;
    public void Win() {
        title.color = winColor;
        score.color = loseColor;
        title.text = "You Win!";
        score.text = "Your final glory: " + esteem.value;
        gameObject.SetActive(true);
    }

    public void Lose() {
        title.color = loseColor;
        score.color = loseColor;
        title.text = "Game Over!";
        if (esteem.value <= 0) {
            score.text = "You ran out of glory!";
        } else {
            score.text = "Your final glory: " + esteem.value;
        }
        gameObject.SetActive(true);
    }

    public void Restart() {
        SceneManager.LoadScene(0);
    }
}
