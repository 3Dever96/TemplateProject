using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public UnityEvent OnPause;

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
}

public enum GameState
{
    Play,
    Pause
}
