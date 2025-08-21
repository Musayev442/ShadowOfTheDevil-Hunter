using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CharacterMover : MonoBehaviour
{
    public float walkSpeed = 2f;
    public float runSpeed = 4f;
    public float sprintSpeed = 6f;
    public float acceleration = 10f;

    private Rigidbody _rb;
    private Animator _anim;
    private Vector3 _velocity;

    void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _anim = GetComponent<Animator>();
    }

    void Update()
    {
        // Raw input
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        bool sprint = Input.GetKey(KeyCode.LeftShift);
        bool walk = Input.GetKey(KeyCode.LeftControl);

        Vector3 inputDir = new Vector3(h, 0, v).normalized;

        // Decide target speed
        float targetSpeed = 0f;
        if (inputDir.sqrMagnitude > 0.01f)
        {
            targetSpeed = sprint ? sprintSpeed :
                          (walk? walkSpeed : runSpeed);
        }

        // Smooth velocity
        _velocity = Vector3.MoveTowards(
            new Vector3(_velocity.x, 0, _velocity.z),
            inputDir * targetSpeed,
            acceleration * Time.deltaTime
        );

        _rb.linearVelocity = new Vector3(_velocity.x, _rb.linearVelocity.y, _velocity.z);

        // Face move direction
        if (inputDir.sqrMagnitude > 0.001f)
        {
            Quaternion targetRot = Quaternion.LookRotation(inputDir);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, Time.deltaTime * 10f);
        }

        // Normalize to feed animator
        float speed01 = Mathf.Clamp01(_velocity.magnitude / sprintSpeed);
        _anim.SetFloat("Speed", speed01, 0.1f, Time.deltaTime);
    }
}
