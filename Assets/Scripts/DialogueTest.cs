using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTest : MonoBehaviour
{
    public DialogueDisplay penelope;
    public DialogueDisplay odysseus;
    public ResponsePicker responsePicker;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DoDialogue());
    }

    IEnumerator DoDialogue() {
        penelope.EnqueueLine("Hi there! I am Penelope!");
        penelope.EnqueueLine("Look, I can talk!");
        penelope.EnqueueLine("I miss Odysseus!");
        yield return new WaitWhile(() => penelope.isTalking);
        odysseus.EnqueueLine("Don't worry, Penelope, it is me, Odysseus!");
        odysseus.EnqueueLine("I know, it might be hard to believe");
        yield return new WaitWhile(() => odysseus.isTalking);
        responsePicker.Prompt(new [] { "I like cheese", "I don't like cheese"});
        yield return new WaitWhile(() => responsePicker.selected == null);
        if (responsePicker.selected == 0) {
            odysseus.EnqueueLine("I like cheese!");
            yield return new WaitWhile(() => odysseus.isTalking);
            penelope.EnqueueLine("Let's go you passed the test, I recoginzed your sign");
        } else {
            odysseus.EnqueueLine("By the way I hate cheese!");
            yield return new WaitWhile(() => odysseus.isTalking);
            penelope.EnqueueLine("Oh almighty Zeus! Get me a better husband! I think I shall marry a suitor!");
        }
    }
}
