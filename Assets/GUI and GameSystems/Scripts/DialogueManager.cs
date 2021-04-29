using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    [SerializeField]
    Transform buttonPanel;

    [SerializeField]
    GameObject buttonPrefab;

    public static DialogueManager theManager;

    private Dialogue currentDialogue;

    private void Awake()
    {
        if (!theManager)
        {
            theManager = this;     
        }
        else
        {
            Destroy(this);
        }
    }

    void CleanUpButtons()
    {
        foreach (Transform child in buttonPanel)
        {
            Destroy(child.gameObject);
        }
    }

    public void LoadDialogue(Dialogue dialogue)
    {
        CleanUpButtons();

        currentDialogue = dialogue;

        print(dialogue.greeting);

        //spawn a button for each dialogue option inside dialogue
        int i = 0;
        foreach (LineOfDialogue lineOfDialogue in dialogue.linesOfDialogue)
        {
            Button spawnedButton = Instantiate(buttonPrefab, buttonPanel).GetComponent<Button>();
            spawnedButton.GetComponentInChildren<TMP_Text>().text = lineOfDialogue.topic;
            int j = i;
            spawnedButton.onClick.AddListener(delegate { ButtonClicked(j); });
            i++;
        }
    }

    void ButtonClicked(int dialogueNum)
    {
        print(currentDialogue.linesOfDialogue[dialogueNum].response);
    }
}
