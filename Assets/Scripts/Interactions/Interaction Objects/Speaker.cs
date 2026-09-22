using System.Collections.Generic;
using UnityEngine;

public class Speaker : InteractionObject
{
    [SerializeField] public Dictionary<string, SpeakerTracker> storyBeats = new Dictionary<string, SpeakerTracker>();

    public override void OnInteract()
    {
        
    }
}
