using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;


public class NPC : MonoBehaviour
{
    public NPCDialogue dialogueData;
    public GameObject dialoguePanel;
    public GameObject ShopPanel;
    public TextMeshProUGUI dialogueText, nameNPC;

    private int dialogueIndex;
    private bool isTyping, isDialogueActive, isShopActive = false;
    private bool playerInZone = false;

    public void Interact(InputAction.CallbackContext ctx)
    {
        if (ctx.started)
        {
            if (playerInZone && isShopActive)
            {
                ShopPanel.SetActive(false);
                isShopActive = false;
                return;
            }
            if(!playerInZone || dialogueData == null) return;
            if(isDialogueActive && !isShopActive)
            {
                NextLine();
            }
            else
            {
                StartDialogue();
            }
        }
    }

    void StartDialogue()
    {
        isDialogueActive = true;
        dialogueIndex = 0;
        nameNPC.SetText("");
        nameNPC.SetText(dialogueData.npcName);
        dialoguePanel.SetActive(true);
        
        StartCoroutine(TypeLine());
    }

    void NextLine()
    {
        if (isTyping)
        {
            StopAllCoroutines();
            dialogueText.SetText(dialogueData.dialogueLines[dialogueIndex]);
            isTyping = false;
        }else if(++dialogueIndex < dialogueData.dialogueLines.Length)
        {
            StartCoroutine(TypeLine());
        }else
        {
            EndDialgue();
            ShopPanel.SetActive(true);
            isShopActive = true;
        }
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
            NextLine();
        }
    }

    public void EndDialgue()
    {
        StopAllCoroutines();
        isDialogueActive = false;
        dialogueText.SetText("");
        dialoguePanel.SetActive(false);
    }
    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInZone = true;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInZone = false;
            ShopPanel.SetActive(false);
            isShopActive = false;
        }
    }
}
