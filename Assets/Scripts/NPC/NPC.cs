using System.Collections;
using Newtonsoft.Json.Converters;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class NPC : MonoBehaviour
{
    public NPCDialogue dialogueData;
    public GameObject dialoguePanel;
    public TextMeshProUGUI dialogueText, nameNPC;

    private int dialogueIndex;
    private bool isTyping, isDialogueActive;

    public bool CanInteract()
    {
        return !isDialogueActive;
    }

    public void Interact()
    {
        if(dialogueData == null || !isDialogueActive) return;
        if (isDialogueActive)
        {

        }
        else
        {
            
        }
    }

    void StartDialogue()
    {
        isDialogueActive = true;
        dialogueIndex = 0;
        dialoguePanel.SetActive(true);
    }

    IEnumerator TypeLine()
    {
        isTyping = true;
        dialogueText.SetText("");
        foreach(char letter in dialogueData.dialogueLines[dialogueIndex])
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(dialogueData.typingSpeed);
        }
        isTyping = false;
        if(dialogueData.autoProgressLines.Length > dialogueIndex && dialogueData.autoProgressLines[dialogueIndex])
        {
            yield return new WaitForSeconds(dialogueData.autoProgessDelay);
        }
    }
}
