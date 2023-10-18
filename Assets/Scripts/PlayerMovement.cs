using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private Animator PlayerAnimator;
    [SerializeField]
    private Joystick joystick;
    [SerializeField]
    private CharacterController Controller; 
    private float MovementSpeed = 2f;
    private float RotationSpeed = 5f;
    private int isWalkingForwardHash;
    private int isWalkingBackwardHash;
    private float MouseY;
    private float RotationY;
    [SerializeField]
    private Particles ParticlesInstance; 
    private void Awake()
    {
        isWalkingForwardHash = Animator.StringToHash("isWalkingForward");
        isWalkingBackwardHash = Animator.StringToHash("isWalkingBackward");
    }
    private void Update()
    { 
        AnimationSetup();
        MovementSetup();
        RotationSetup();  
    }

    private void AnimationSetup()
    {
        bool isMovingForward = joystick.Vertical > 0f;
        bool isForwardPressed = PlayerAnimator.GetBool(isWalkingForwardHash);
        bool isMovingBackward = joystick.Vertical < 0f;
        bool isBackwardPressed = PlayerAnimator.GetBool(isWalkingBackwardHash);

        if (!isForwardPressed && isMovingForward)
        {
            PlayerAnimator.SetBool(isWalkingForwardHash, true);
        }

        else if (isForwardPressed && !isMovingForward)
        {
            PlayerAnimator.SetBool(isWalkingForwardHash, false);
        }

        else if (!isBackwardPressed && isMovingBackward)
        {
            PlayerAnimator.SetBool(isWalkingForwardHash, true);
            PlayerAnimator.SetBool(isWalkingBackwardHash, true);
        }

        else if (isBackwardPressed && !isMovingBackward)
        {
            PlayerAnimator.SetBool(isWalkingBackwardHash, false);
        }
    }

    private void MovementSetup()
    {
        if (transform.localEulerAngles.y < 90f || transform.localEulerAngles.y > 270f)
        { 
            Vector3 input = new Vector3(joystick.Horizontal, 0f, joystick.Vertical);
            Vector3 Velocity = input.normalized * MovementSpeed * Time.deltaTime;
            Controller.Move(Velocity);
        }
        else if (transform.localEulerAngles.y > 90f)
        {
            Vector3 input = new Vector3(joystick.Horizontal, 0f, joystick.Vertical);
            Vector3 Velocity = input.normalized * -MovementSpeed * Time.deltaTime;
            Controller.Move(Velocity);
        }
    }

    private void RotationSetup()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            MouseY = Input.GetTouch(0).deltaPosition.x;
            ParticlesInstance.Fire();
        }

        MouseY = MouseY * RotationSpeed * Time.deltaTime;
        RotationY += MouseY;

        transform.localRotation = Quaternion.Euler(0f, RotationY, 0f);
    }
}
