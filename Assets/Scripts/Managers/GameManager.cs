using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameState state;

    public UnityEvent OnResume;
    public UnityEvent OnPause;
    public UnityEvent OnDialogue;

    private void Awake()
    {
        if (instance == null) instance = this;
        if (instance != this) Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    public void OnPauseGame()
    {
        OnPause?.Invoke();
        Time.timeScale = 0f;
    }

    public void ResumeGame()
    {
        state = GameState.Play;
        Time.timeScale = 1f;
        OnResume?.Invoke();
    }

    public void OnDialogueStart()
    {
        state = GameState.Dialogue;
        OnDialogue?.Invoke();
    }
}

public enum GameState
{
    Play,
    Pause,
    Dialogue
}
