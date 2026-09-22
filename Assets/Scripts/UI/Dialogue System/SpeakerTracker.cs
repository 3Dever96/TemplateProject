using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SpeakerTracker
{
    [SerializeField] public Dictionary<string, Conversation> conversations = new Dictionary<string, Conversation>();
}
