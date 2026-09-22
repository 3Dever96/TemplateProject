using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Dictionary<string, UIScreen> screens = new Dictionary<string, UIScreen>();
    [SerializeField] private string defaultScreen;

    private void Start()
    {
        ChangeScreen(defaultScreen);
    }

    public void ChangeScreen(string key)
    {
        if (!screens.ContainsKey(key)) return;

        foreach (UIScreen screen in screens.Values)
        {
            screen.screenObject.SetActive(false);
        }

        screens[key].screenObject.SetActive(true);
        if (screens[key].defaultSelection != null)
        {
            EventSystem.current.SetSelectedGameObject(screens[key].defaultSelection);
        }
        else
        {
            EventSystem.current.SetSelectedGameObject(null);
        }
    }

    private void OnEnable()
    {
        GameManager.instance.OnPause.AddListener(OnPause);
        GameManager.instance.OnResume.AddListener(OnResume);
        GameManager.instance.OnDialogue.AddListener(OnDialogue);
    }

    private void OnDisable()
    {
        GameManager.instance.OnPause.RemoveListener(OnPause);
        GameManager.instance.OnResume.RemoveListener(OnResume);
        GameManager.instance.OnDialogue.RemoveListener(OnDialogue);
    }

    public void OnPause()
    {
        ChangeScreen("Pause Menu");
    }

    public void OnResume()
    {
        ChangeScreen("Player HUD");
    }

    public void OnDialogue()
    {
        ChangeScreen("Dialogue");
    }
}
