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
        manager.ChangeScreen("Player HUD");
    }

    public void OnOptions()
    {

    }

    public void OnQuit()
    {

    }
}
