using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    private RectTransform myTransform;
    [SerializeField] private float moveSpeed;

    [Header("UI Elements")]
    [SerializeField] private TMPro.TMP_Text speakerName;
    [SerializeField] private TMPro.TMP_Text dialogue;
    [SerializeField] private Image portrait;
    [SerializeField] private RectTransform optionsBox;
    [SerializeField] private GameObject nextButton;
    [SerializeField] private GameObject dialogueOptionsButton;

    [SerializeField] private Conversation conversation;
    private DialogueNode currentNode;

    private void Start()
    {
        SetConversation(conversation);
    }

    private void Update()
    {
        MoveTransform(conversation != null);
    }

    public void OnEnable()
    {
        if (myTransform == null)
        {
            myTransform = GetComponent<RectTransform>();
        }
    }

    public void SetConversation(Conversation newConversation)
    {
        conversation = newConversation;
        if (conversation != null)
        {
            SetDialogue(conversation.dialogue["A. Intro"]);
        }
    }

    public void SetDialogue(DialogueNode newNode)
    {
        currentNode = newNode;
        nextButton.SetActive(true);

        if (currentNode.responses.Count == 0)
        {
            optionsBox.gameObject.SetActive(false);

            EventSystem.current.SetSelectedGameObject(nextButton);
        }
        else
        {
            optionsBox.gameObject.SetActive(true);
            optionsBox.localPosition = new Vector3(0f, -350f + (32.5f * (currentNode.responses.Count - 1)), 0f);

            for (var i = 0; i < currentNode.responses.Count; i++)
            {
                Button btn = Instantiate(dialogueOptionsButton, optionsBox).GetComponent<Button>();

                TMPro.TMP_Text bText = btn.GetComponentInChildren<TMPro.TMP_Text>();
                bText.text = currentNode.responses[i].option;

                int index = i;
                btn.onClick.AddListener(() => OnOption(index));
            }
        }

        dialogue.text = currentNode.dialogue;
        dialogue.maxVisibleCharacters = 0;
    }

    public void OnNext()
    {
        if (dialogue.maxVisibleCharacters == dialogue.text.Length)
        {
            if (currentNode.nextKey != "")
            {
                SetDialogue(conversation.dialogue[currentNode.nextKey]);
            }
            else
            {
                if (currentNode.responses.Count == 0)
                {
                    conversation = null;
                    currentNode = null;
                }
            }
        }
        else
        {
            dialogue.maxVisibleCharacters = dialogue.text.Length;
            if (currentNode.responses.Count > 0)
            {
                nextButton.SetActive(false);
                EventSystem.current.SetSelectedGameObject(optionsBox.GetChild(0).gameObject);
            }
        }
    }

    public void OnOption(int response)
    {
        print(response);

        if (optionsBox.childCount > 0)
        {
            for (var i = optionsBox.childCount - 1; i >= 0; i--)
            {
                Destroy(optionsBox.GetChild(i).gameObject);
            }
        }

        SetDialogue(conversation.dialogue[currentNode.responses[response].key]);
    }

    private void MoveTransform(bool hasConversation)
    {
        if (hasConversation)
        {
            if (myTransform.localPosition != Vector3.zero)
            {
                if (Vector3.Distance(myTransform.localPosition, Vector3.zero) > 0.5f)
                {
                    myTransform.localPosition = Vector3.Lerp(myTransform.localPosition, Vector3.zero, moveSpeed * Time.deltaTime);
                }
                else
                {
                    myTransform.localPosition = Vector3.zero;
                }
            }
        }
        else
        {
            Vector3 offscreen = new Vector3(0f, -1080f, 0f);

            if (myTransform.localPosition != offscreen)
            {
                if (Vector3.Distance(myTransform.localPosition, offscreen) > 0.5f)
                {
                    myTransform.localPosition = Vector3.Lerp(myTransform.localPosition, offscreen, moveSpeed * Time.deltaTime);
                }
                else
                {
                    myTransform.localPosition = offscreen;
                }
            }
        }
    }
}
