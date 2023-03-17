namespace Assets.Scripts.Player
{
    using UnityEngine;

    /// <summary>
    /// This object controls the movement of the holdingPoint:
    /// rotation, position and Velocity.
    /// </summary>
    public class PlayerMovement : MonoBehaviour
    {
        private const string HorizontalAxis = "Horizontal";
        private const string VerticalAxis = "Vertical";
        private const string AxisMouseX = "Mouse X";
        private const string IsRunningAnimationName = "isRunning";
        private const string IsSideRunningAnimationName = "isSideRun";

        private bool isScaleToRight;
        private float playerRotation;
        private Vector3 playerVelocity;
        private CharacterController controller;
        private Animator playerAnimator;

        [SerializeField]
        private Vector3 m_VerticalDir;
        [SerializeField]
        private Vector3 m_HorizontalDir;
        [SerializeField]
        private float mouseSensetivity = 1000;
        [SerializeField]
        [Range(5,100)]
        private float playerSpeed = 2.0f;
        [SerializeField]
        [Range(2, 70)]
        private float jumpHeight = 1.0f;
        [SerializeField]
        private float gravityValue = -9.81f;

        private bool GroundedPlayer => this.controller.isGrounded;

        private bool RunningAnimation
        {
            get => this.playerAnimator.GetBool(IsRunningAnimationName);
            set => this.playerAnimator.SetBool(IsRunningAnimationName, value);
        }

        private bool RunningSideAnimation
        {
            get => this.playerAnimator.GetBool(IsSideRunningAnimationName);
            set => this.playerAnimator.SetBool(IsSideRunningAnimationName, value);
        }

        private bool IsVerticalMovement => IsNotZeroVector3(this.m_VerticalDir); 

        private bool IsHorizontalMovement => IsNotZeroVector3(this.m_HorizontalDir);

        private static bool IsKeysPress(KeyCode first, params KeyCode[] rest)
        {
            bool res = Input.GetKey(first);

            if (!res)
            {
                foreach (KeyCode key in rest)
                {
                    res |= Input.GetKey(key);
                }
            }

            return res;
        }

        private static bool IsNotZeroVector3(Vector3 playerVerticalDirection)
        {
            return playerVerticalDirection.x != 0 ||
                playerVerticalDirection.y != 0 ||
                playerVerticalDirection.z != 0;
        }

        private void Start()
        {
            this.controller = this.gameObject.GetComponent<CharacterController>();
            this.playerAnimator = this.GetComponentInChildren<Animator>();
        }

        private void Update()
        {
            this.UpdateVectorsRotation();
            this.UpdateVectorsVelocity();
            this.UpdateVectorsDirection();
        }

        private void LateUpdate()
        {
            this.UpdateVelocity();
            this.UpdateRotation();
            this.UpdatePosition();
            this.UpdateAnimation();
        }

        private void UpdateAnimation()
        {
            if (this.IsHorizontalMovement)
            {
                this.RunningAnimation = false;
                this.RunningSideAnimation = true;
            }
            else if (this.IsVerticalMovement)
            {
                this.RunningSideAnimation = false;
                this.RunningAnimation = true;
            }
            else
            {
                this.RunningAnimation = false;
                this.RunningSideAnimation = false;
            }

            SpinPleyarForRunningSide();
        }

        private void UpdateRotation()
        {
            this.gameObject.transform.Rotate(Vector3.up, this.playerRotation);
        }

        private void UpdateVectorsRotation()
        {
            this.playerRotation = this.GetMouseMove();
        }

        private float GetMouseMove()
        {
            return Input.GetAxis(AxisMouseX) * this.mouseSensetivity * Time.deltaTime;
        }

        private void UpdatePosition()
        {
            this.controller.Move(this.m_HorizontalDir * Time.deltaTime * this.playerSpeed);
            this.controller.Move(this.m_VerticalDir * Time.deltaTime * this.playerSpeed);
        }

        private void UpdateVectorsDirection()
        {
            this.m_HorizontalDir = this.transform.TransformDirection(new Vector3(Input.GetAxis(HorizontalAxis), 0, 0));
            this.m_VerticalDir = this.transform.TransformDirection(new Vector3(0, 0, Input.GetAxis(VerticalAxis)));
        }

        private void SpinPleyarForRunningSide()
        {
            if (this.m_HorizontalDir.x < 0)
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
            if (this.m_HorizontalDir.x > 0)
            {
                transform.localScale = Vector3.one;
            }
        }

        private void UpdateVelocity()
        {
            this.controller.Move(this.playerVelocity * Time.deltaTime);
        }

        private void UpdateVectorsVelocity()
        {
            if (this.GroundedPlayer)
            {
                if (this.playerVelocity.y < 0)
                {
                    this.playerVelocity.y = 0f;
                }

                if (Input.GetButtonDown("Jump"))
                {
                    this.playerVelocity.y += Mathf.Sqrt(this.jumpHeight * -3.0f * this.gravityValue);
                }
            }
            this.playerVelocity.y += this.gravityValue * Time.deltaTime;
        }
    }
}