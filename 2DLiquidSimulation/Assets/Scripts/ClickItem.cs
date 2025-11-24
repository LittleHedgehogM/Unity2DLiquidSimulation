using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;
public class ClickItem : MonoBehaviour
{
    public string NodeName;
    private DialogueRunner myDialogueRunner;
    private void Start()
    {
        myDialogueRunner = FindObjectOfType<DialogueRunner>();
    }
    public void ClickOnItem()
    {
        // Debug.Log("OnMouse Up" + NodeName);
        if (!myDialogueRunner.IsDialogueRunning)
        {
            myDialogueRunner.StartDialogue(NodeName);
            this.gameObject.SetActive(false);
        }

    }

}
