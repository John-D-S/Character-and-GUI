using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialogue : MonoBehaviour
{
    public string greeting;

    public LineOfDialogue[] linesOfDialogue;

    public LineOfDialogue bye;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            DialogueManager.theManager.LoadDialogue(this);
        }
    }
}
