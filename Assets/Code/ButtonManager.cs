using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    public Dialogue dialogue;
    [SerializeField] GameObject startButton;
    [SerializeField] string level;

    public void TriggerDialogue()
    {
        startButton.SetActive(false);
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
    }

    public void nextScene(){
      SceneManager.LoadScene(level);
      Time.timeScale = 1;
    }
}
