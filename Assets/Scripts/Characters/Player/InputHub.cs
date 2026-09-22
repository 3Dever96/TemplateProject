using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class InputHub : MonoBehaviour
{
    // Input Properties
    public Vector2 Move { get; private set; }
    public Vector2 Look { get; private set; }
    public bool Jump { get; private set; }
    public bool Melee { get; private set; }
    public bool Shoot { get; private set; }
    public bool Defend { get; private set; }
    public bool Dash { get; private set; }
    public bool LockOn { get; private set; }
    public bool Interact { get; private set; }
    public bool Crouch { get; private set; }
    public bool Pause { get; private set; }
    public bool Map {  get; private set; }

    // Private References
    private PlayerInput input;

    private void Start()
    {

        input = GetComponent<PlayerInput>();

        input.onActionTriggered += OnAction;
    }

    private void OnEnable()
    {
        if (input != null)
        {
            input.onActionTriggered += OnAction;
        }
    }

    private void OnDisable()
    {
        input.onActionTriggered -= OnAction;
    }

    public void OnAction(InputAction.CallbackContext context)
    {
        switch (context.action.name)
        {
            case "Move": Move = context.ReadValue<Vector2>(); break;
            case "Look": Look = context.ReadValue<Vector2>(); break;
            case "Jump": Jump = context.ReadValue<float>() > 0.5f; break;
            case "Melee": Melee = context.ReadValue<float>() > 0.5f; break;
            case "Shoot": Shoot = context.ReadValue<float>() > 0.5f; break;
            case "Defend": Defend = context.ReadValue<float>() > 0.5f; break;
            case "Dash": Dash = context.ReadValue<float>() > 0.5f; break;
            case "LockOn": LockOn = context.ReadValue<float>() > 0.5f; break;
            case "Interact": Interact = context.ReadValue<float>() > 0.5f; break;
            case "Crouch": Crouch = context.ReadValue<float>() > 0.5f; break;
            case "Pause":
                GameManager.instance.OnPauseGame();
                break;
            case "Map": Map = context.ReadValue<float>() > 0.5f; break;
        }
    }
}
