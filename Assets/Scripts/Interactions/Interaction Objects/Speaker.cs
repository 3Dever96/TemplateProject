using System.Collections.Generic;
using UnityEngine;

public class Speaker : InteractionObject
{
    [SerializeField] private Conversation testConversation;

    public override void OnInteract()
    {
        DialogueManager.instance.SetConversation(testConversation);
        GameManager.instance.OnDialogueStart();
    }

    protected override void Update()
    {
        
    }
}
