using UnityEngine;

[RequireComponent(typeof(InputHub)), RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    // REFERENCES
    public CharacterController Controller { get; private set; }
    public InputHub Input { get; private set; }
    public Transform Cam {  get; private set; }

    // MODIFIER PROPERTIES
    [field: SerializeField] public float TurnSpeed { get; private set; }
    [field: SerializeField] public float JumpSpeed {  get; private set; }
    [field: SerializeField] public float Gravity {  get; private set; }
    [field: SerializeField] public float StickForce {  get; private set; }
    [field: SerializeField] public float FallSpeed {  get; private set; }

    // MOVEMENT PROPERTIES
    public float CurrentSpeed { get; set; }
    public float VerticalSpeed {  get; set; }
    public Vector3 Direction { get; set; }
    public Vector3 Velocity { get { return GetVelocity(); } }

    // STATE MACHINE
    public PlayerState CurrentState { get; private set; }
    public PlayerState LastState { get; private set; }
    [field: SerializeField] public PlayerGroundState GroundState { get; private set; } = new PlayerGroundState();
    [field: SerializeField] public PlayerAirState AirState { get; private set; } = new PlayerAirState();

    private void Start()
    {
        Controller = GetComponent<CharacterController>();
        Input = GetComponent<InputHub>();
        Cam = Camera.main.transform;

        SetState(GroundState);
    }

    private void Update()
    {
        if (CurrentState != null)
        {
            CurrentState.UpdateState(this);
            CurrentState.ChangeState(this);
            Controller.Move(Velocity * Time.deltaTime);
        }
    }

    public void SetState(PlayerState newState)
    {
        if (CurrentState != null)
        {
            CurrentState.ExitState(this);
        }

        CurrentState = newState;

        if (CurrentState != null)
        {
            CurrentState.StartState(this);
        }
    }

    public void FaceDirection(Vector3 forward, float speed)
    {
        if (forward == Vector3.zero) return;

        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(forward), speed * Time.deltaTime);
    }

    private Vector3 GetVelocity()
    {
        Vector3 v = CurrentSpeed * Direction;
        v.y = VerticalSpeed;
        return v;
    }

    public bool CheckCollision(Vector3 offset)
    {
        Vector3 origin = transform.position;
        float radius = Controller.radius - 0.1f;
        Vector3 position = offset + origin;

        return Physics.CheckSphere(position, radius, LayerMask.GetMask("Solid"));
    }
}
