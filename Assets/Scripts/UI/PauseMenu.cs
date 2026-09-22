using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    private UIManager manager;

    private void OnEnable()
    {
        if (manager == null)
        {
            manager = GetComponentInParent<UIManager>();
        }
    }

    public void OnResume()
    {
        GameManager.instance.ResumeGame();
    }

    public void OnOptions()
    {

    }

    public void OnQuit()
    {

    }
}
