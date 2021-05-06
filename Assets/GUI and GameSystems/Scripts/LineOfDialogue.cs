using UnityEngine;

[System.Serializable]
public class LineOfDialogue
{
    [TextArea(3, 5)]
    public string topic, response;

    public Dialogue nextDialogue;

    //void Update()
}
