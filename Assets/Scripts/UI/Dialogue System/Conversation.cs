using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Conversation", menuName = "Story/Conversation")]
public class Conversation : ScriptableObject
{
    [SerializeField] public Dictionary<string, DialogueNode> dialogue = new Dictionary<string, DialogueNode>();
}
