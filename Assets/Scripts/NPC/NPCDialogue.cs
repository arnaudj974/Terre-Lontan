using UnityEngine;

[CreateAssetMenu(fileName = "NPCDialogue", menuName = "NPC/NPC Dialogue")]
public class NPCDialogue : ScriptableObject
{
    public string npcName;
    public string[] dialogueLines;
     public bool[] autoProgressLines;
    public float typingSpeed = 0.05f;
    public AudioClip voiceSound;
    public float voicePitch = 1f;
    public float autoProgessDelay;

}
