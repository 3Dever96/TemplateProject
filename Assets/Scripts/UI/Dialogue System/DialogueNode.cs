using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DialogueNode
{
    public string dialogue;
    public List<Response> responses = new List<Response>();
    public string nextKey;
}
