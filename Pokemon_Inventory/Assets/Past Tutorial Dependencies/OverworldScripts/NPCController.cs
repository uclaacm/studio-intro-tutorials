using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCController : MonoBehaviour, Interactable
{
    [SerializeField] Dialogue dialogue;

    public void Interaction() {
        DialogueManager.Instance.ShowDialogue(dialogue);
    }
}
