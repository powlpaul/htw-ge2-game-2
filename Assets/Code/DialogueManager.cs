using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public Text nameText;
    public Text dialogueText;
    public Animator animator;
    public Animator ballAnimator;
    public Animator blueBirbAnimator;
    public Animator brownBirbAnimator;

    public Queue<string> sentences;

    // Start is called before the first frame update
    void Start()
    {
        sentences = new Queue<string>();
    }

    public void StartDialogue(Dialogue dialogue)
    {
        animator.SetBool("IsOpen", true);
        ballAnimator.SetBool("ballHasEntered", true);
        blueBirbAnimator.SetBool("blueEntered", true);
        brownBirbAnimator.SetBool("brownEntered", true);
        
        
        sentences.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue (sentence);
        }
        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        dialogueText.text = sentence;
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));

    }

    IEnumerator TypeSentence(string sentence) {
      dialogueText.text = "";
      foreach(char letter in sentence.ToCharArray()) {
        dialogueText.text += letter;
        yield return null;
      }
    }

    void EndDialogue()
    {
        Debug.Log("End of conversation");
        animator.SetBool("IsOpen", false);
        ballAnimator.SetBool("ballHasEntered", false);
        blueBirbAnimator.SetBool("blueEntered", false);
        brownBirbAnimator.SetBool("brownEntered", false);
    }
}
