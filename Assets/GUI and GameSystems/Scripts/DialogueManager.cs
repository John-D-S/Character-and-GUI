using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    [SerializeField]
    Transform buttonPanel;

    GameObject dialoguePanel;

    [SerializeField]
    GameObject buttonPrefab;

    [SerializeField]
    TextMeshProUGUI responsePanel;

    public static DialogueManager theManager;

    private Dialogue currentDialogue;

    private void Awake()
    {
        dialoguePanel = transform.Find("Scroll View").gameObject;
        dialoguePanel.SetActive(false);

        if (!theManager)
        {
            theManager = this;     
        }
        else
        {
            Destroy(this);
        }
        
        CleanUpButtons();
    }

    void CleanUpButtons()
    {
        foreach (Transform child in buttonPanel)
        {
            Destroy(child.gameObject);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        if (responsePanel.text != "")
        {
            StartCoroutine(ClearDialogueResponse());
        }
    }

    public void LoadDialogue(Dialogue dialogue)
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        dialoguePanel.SetActive(true);
        Button spawnedButton;

        CleanUpButtons();

        currentDialogue = dialogue;

        Respond(dialogue.greeting);

        //spawn a button for each dialogue option inside dialogue
        int i = 0;
        foreach (LineOfDialogue lineOfDialogue in dialogue.linesOfDialogue)
        {
            spawnedButton = Instantiate(buttonPrefab, buttonPanel).GetComponent<Button>();
            spawnedButton.GetComponentInChildren<TMP_Text>().text = lineOfDialogue.topic;
            int j = i;
            spawnedButton.onClick.AddListener(delegate { ButtonClicked(j); });
            i++;
        }

        //spawn the bye button
        spawnedButton = Instantiate(buttonPrefab, buttonPanel).GetComponent<Button>();
        spawnedButton.GetComponentInChildren<TMP_Text>().text = dialogue.bye.topic;
        spawnedButton.onClick.AddListener(EndConversation);
    }

    IEnumerator ClearDialogueResponse()
    {
        yield return new WaitForSeconds(5);
        responsePanel.text = "";
    }


    void Respond(string _response)
    {
        responsePanel.text = _response;
    }

    void ButtonClicked(int dialogueNum)
    {
        Respond(currentDialogue.linesOfDialogue[dialogueNum].response);
    }

    void EndConversation()
    {
        Respond(currentDialogue.bye.response);
        CleanUpButtons();
        dialoguePanel.SetActive(false);
    }
}
