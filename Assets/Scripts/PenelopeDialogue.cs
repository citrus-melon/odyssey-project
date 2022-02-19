using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PenelopeDialogue : MonoBehaviour {
    public DialogueDisplay penelope;
    public DialogueDisplay odysseus;
    public ResponsePicker responsePicker;
    public SelfEsteemBar esteemBar;
    public EndScreen endScreen;
    public Animator anim;

    void Start() {
        StartCoroutine(MainDialogue());
    }

    IEnumerator SpeakAndWait(DialogueDisplay display, params string[] lines) {
        odysseus.gameObject.SetActive(false);
        penelope.gameObject.SetActive(false);
        foreach (string line in lines) display.EnqueueLine(line);
        yield return new WaitWhile(() => display.isTalking);
    }

    IEnumerator AskAndWait(params string[] options) {
        responsePicker.Prompt(options);
        yield return new WaitWhile(() => responsePicker.selected == null);
    }

    IEnumerator MainDialogue() {
        yield return SpeakAndWait(penelope, "Stranger, first I want to ask what people you have come from. Who are your parents? Where is your home town?");
        yield return AskAndWait("Refuse", "Tell a lie", "Tell the truth");
        if (responsePicker.selected == 0) { // Refuse
            yield return SpeakAndWait(odysseus, "You have the right to question me, but please do not ask about my family or native land.", "The memory will fill my heart with pain. I am a man of sorrow.");
            yield return SpeakAndWait(penelope, "Stranger, do not waste my time.");
            anim.SetBool("Crying", true);
            esteemBar.Increment(1);
            yield return SpeakAndWait(penelope, "I have suffered so much, and I miss my dear Odysseus.");
            anim.SetBool("Crying", false);
            yield return SpeakAndWait(penelope, "Tell me who you are, or leave my halls.");
            yield return AskAndWait("Refuse again", "Tell a lie", "Tell the truth");
            if (responsePicker.selected == 0) { // Refuse
                yield return SpeakAndWait(odysseus, "Penelope, wise daughter of Laertes, I do not see why you are so insistent on knowing my family.");
                yield return SpeakAndWait(penelope, "I have been hurt by evil men who spin lies to trick me and gain my favor.", "why are you so protective about your history?", "I must know why I should trust you.", "So, you must reveal your ancestry.");
                yield return AskAndWait("Refuse again", "Tell a lie", "Tell the truth");
                if (responsePicker.selected == 0) { // Refuse
                    yield return SpeakAndWait(odysseus, "Wise Penelope, wife of Odysseus", "ask me of anything else", "but do not ask about my homeland.");
                    yield return SpeakAndWait(penelope, "Beggar, you are a guest in my home.", "Since you are unwilling to cooperate with me,", "leave my home and do not waste my time.", "I am too busy grieving for my lost Odysseus.");
                    endScreen.Lose(); yield break;
                }
            }
        }
        if (responsePicker.selected == 2) { // Tell the truth
            yield return SpeakAndWait(odysseus, "Penelope, I am Odysseus!", "I have returned from Troy!");
            esteemBar.Increment(15);
            yield return SpeakAndWait(penelope, "You think you can fool me!", "Leave my home! Do not add to my pain with your twisted lies!");
            endScreen.Lose(); yield break;
        }

        yield return SpeakAndWait(odysseus, "I am from spacious Crete...");
        esteemBar.Increment(-1);
        yield return AskAndWait("...a fertile island out at sea.", "...the people there specialize in ships and fleets.", "...where shaking forest hides Mount Neriton.", "...where giants roam the land.");
        yield return SpeakAndWait(odysseus, responsePicker.selectedContent);
        if (responsePicker.selected != 0) { // not fertile island
            yield return SpeakAndWait(penelope, "That doesn't sound like Crete.", "Leave, do not lie to me.");
            endScreen.Lose(); yield break;
        }

        yield return AskAndWait("I am the son of wealth Castor Hylacides...", "My father, Deacalion, was the son of Minos...", "On the fields of Crete I killed Orsilochus...");
        yield return SpeakAndWait(odysseus, responsePicker.selectedContent);
        int? lastResponse = responsePicker.selected;
        esteemBar.Increment(-1);

        int[] key = {2, 0, 1};
        yield return AskAndWait("...the son of Idomeneus, the king.", "...The Cretan people held him in high honor as if he were a god, since he was rich and had such noble sons.", "...the intimate of Zeus, who was king for nine years.");
        yield return SpeakAndWait(odysseus, responsePicker.selectedContent);
        if (lastResponse != key[(int)responsePicker.selected]) {
            yield return SpeakAndWait(penelope, "stranger, do not wish to gain my favor through cunning stories", "what you say is not true."); 
            endScreen.Lose(); yield break;
        }

        yield return AskAndWait("Years ago, Odysseus stayed as a guest at my fathers' home", "I remember seeing Odysseus during the Greeks' war against Troy.");
        if (responsePicker.selected == 0) { // Odysseus stay at our house
            yield return SpeakAndWait(odysseus, "Many years ago,", "Odysseus rested at my home for twelve days.");
            
            yield return AskAndWait("He had the face of a noble man, a king.", "A storm had driven him off course.");
            if (responsePicker.selected == 0) {
                yield return SpeakAndWait(odysseus, "He had the face of a noble man, a king.");
                esteemBar.Increment(2);
            }
            else yield return SpeakAndWait(odysseus, "A storm had driven him off course from Troy, dragging him to Crete instead.");

            yield return AskAndWait("We exchanged gifts and stories, plenty of wine.", "Our stores were ample, I had the slaves make them a feast.");
            if (responsePicker.selected == 0) yield return SpeakAndWait(odysseus, "We exchanged gifts and stories, plenty of wine.");
            else yield return SpeakAndWait(odysseus, "Our stores were ample.", "I had the slaves bring barley, red wine, and bulls", "they ate untill their hearts were satisfied.");

            yield return AskAndWait("On the fifth day, he and his crew left.", "On the twelfth day, he and his crew left.", "On the tenth day, he and his crew left.");
            yield return SpeakAndWait(odysseus, responsePicker.selectedContent);
            if (responsePicker.selected != 1) {
                yield return SpeakAndWait(penelope, "Earlier, you said that my dear Odysseus stayed for twelve days.", "How come you now claim otherwise?", "I think you are lying to me.", "Leave my home, do not try to fool me and increase my grief.");
            }
        } else { // We go to the war
            yield return SpeakAndWait(odysseus, "I remember seeing Odysseus when I was fighting alongside all the other Greeks to take Troy.");

            yield return AskAndWait("He was skilled in tactics and strategy.", "I saw his magestic purple cloak flapping in the wind.", "I didn't know much about him at the time.");
            if (responsePicker.selected == 0) {
                yield return SpeakAndWait(odysseus, "He was skilled in tactics and strategy, just as with his cunning lies.");
                esteemBar.Increment(2);
            } else if (responsePicker.selected == 1) {
                yield return SpeakAndWait(odysseus, "His magestic purple cloak flapped in the wind.", "Were I a Trojan, I would have fleed right then at the sight of him.");
                esteemBar.Increment(1);
            } else {
                yield return SpeakAndWait(odysseus, "I heard that he was a bit of a selfish commander.");
                esteemBar.Increment(-3);

            }
        }

        anim.SetBool("Crying", true);
        esteemBar.Increment(1);
        yield return SpeakAndWait(penelope, "It seems you really do know Odysseus.", "Oh, how I miss him, but he will never come back.");
        anim.SetBool("Crying", false);
        yield return SpeakAndWait(penelope, "Stranger, I want to test you to make sure. What did he look like?");
        yield return AskAndWait("He wore a purple cloak, tied around his waist with a velvet rope.", "I don't quite remember, it's been a long time.", "He wore a purple cloak, fastened with a golden brooch.");
        yield return SpeakAndWait(odysseus, responsePicker.selectedContent);
        if (responsePicker.selected != 2) {
            yield return SpeakAndWait(penelope, "Well, you've failed my test.", "Go find someone else to bother.");
            endScreen.Lose(); yield break;
        }

        yield return SpeakAndWait(odysseus, "The brooch was elaborately engraved with...");
        yield return AskAndWait("A sharp golden arrow shooting out of a curved bow.", "A dog clutching a spotted fawn in its front paws.", "A golden olive tree with uncountable branches and leaves.");
        yield return SpeakAndWait(odysseus, responsePicker.selectedContent,
            new[] { "All who saw it marveled at how the arrow seemed to soar forwards whenever his body turned.",
            "The dog gripped tightly while the fawn kicked and struggled, even though they were both made of gold.",
            "The more I gazed at it, the more the branches seemed to sway in the wind."}[(int)responsePicker.selected]
        );
        if (responsePicker.selected != 1) {
            yield return SpeakAndWait(penelope, "No.", "The brooch was engraved with a dog, clutching a struggling dappled fawn.", "I clasped that brooch for him the day he left for Evillium, the town I will not name.", "You do not truly know my Odysseus. Do not try to fool me.", "You failed.");
            endScreen.Lose(); yield break;
        }
        esteemBar.Increment(1);

        yield return SpeakAndWait(penelope, "You know!", "You have met my dear Odysseus!", "You are not just a beggar, you are an honored guest and friend here in my home.");
        anim.SetBool("Crying", true);
        esteemBar.Increment(1);
        yield return SpeakAndWait(penelope, "Those...", "those were the clothes I gave to him on the day he and his gleaming army sailed off to the town I will not name.", "Now, he will never come back.");
        anim.SetBool("Crying", false);

        yield return AskAndWait("Yeah.. That's unfortunate.", "Wise Penelope, stop ruining your pretty skin with tears.");
        yield return SpeakAndWait(odysseus, responsePicker.selectedContent);
        yield return AskAndWait("They say your husband was a godlike hero.", "I do not blame you, you must be in much grief.");
        yield return SpeakAndWait(odysseus, responsePicker.selectedContent);
        if (responsePicker.selected == 0) esteemBar.Increment(1);

        yield return AskAndWait("Perhaps it is time to move on.", "Odysseus is coming home");
        if (responsePicker.selected == 0) {
            yield return SpeakAndWait(odysseus, "Perhaps it is time that you move on and find a new husband.");
            esteemBar.Increment(-1);

            yield return SpeakAndWait(penelope,
                "Stranger, now that you speak of this, I have something to confide in you.",
                "I had a dream...",
                "There were twenty geese, and I was glad to see them.",
                "Then a swift eagle swooped from the mountain and killed my geese.",
                "I cried.",
                "The eagle flew back to me, and spoke to me in human language.",
                "\"Penelope, cheer up. These geese are the suitors, and I am your husband. This dream will soon come true.\""
            );
            esteemBar.Increment(-1);
            yield return SpeakAndWait(penelope, "I think the day of doom coming that will take me from the house of Odysseus.",
                "I will arrange a contest with his axes. ", "From a distance he could shoot through all twelve of them using his bow.",
                "I will assign this contest to the suitors.");

            yield return AskAndWait("Why are you doing this?", "Odysseus is coming home soon");
            if (responsePicker.selected == 0) {
                yield return SpeakAndWait(odysseus, "Why are you doing this?", "Do you wish to be married to a suitor?");
                anim.SetBool("Crying", true);
                esteemBar.Increment(1);
                yield return SpeakAndWait(penelope, "Well, no.", "I still wish for Odysseus to return.");
                anim.SetBool("Crying", false);
                yield return SpeakAndWait(penelope, "My son is grown-up now, and he is concerned that they are eating up his property.", "He urges me to go.");
                yield return SpeakAndWait(odysseus, "I see.");
            }
            yield return SpeakAndWait(odysseus, "I have heard that Odysseus is coming home.", "I am certain of it.", "He will arrive by this lunar month.");
            yield return AskAndWait("Do not postpone the contest.", "You should hold off a little longer.");
            if (responsePicker.selected == 0) {
                yield return SpeakAndWait(odysseus, "Honored wife of great Odysseus, do not postpone this contest.", "Odysseus will arrive while they are competing.");
                esteemBar.Increment(-1);
                yield return SpeakAndWait(penelope, "Yes. Shh... I actually want that to happen.");
                esteemBar.Increment(3);
            } else {
                yield return SpeakAndWait(odysseus, "Perhaps if you wait a bit longer, he will come home and kill the suitors.");
            }

        } else {
            yield return SpeakAndWait(odysseus, 
                "Listen. I will tell you a certainty.", "I have heard that Odysseus is coming home.", "I swear this by Zeus:",
                " Odysseus will arrive this very lunar month.");
            esteemBar.Increment(1);
            yield return SpeakAndWait(penelope, "Well, stranger, I sure hope that you're right.",
            "If so, I'll reward you with so much generosity.",
            "But that's simply not going to happen. Odysseus will not come home.");
        }
        yield return SpeakAndWait(penelope, "You could entertain me forever, guest, but humans cannot stay awake forever.");
        anim.SetBool("Crying", true);
        esteemBar.Increment(2);
        yield return SpeakAndWait(penelope,
            "I will go up and lie down on my bed,",
            "a bed of grief, all stained with tears,",
            "and cry for my dear husband.");
        anim.SetBool("Crying", false);
        endScreen.Win();
    }
}