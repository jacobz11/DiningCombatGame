using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //public static Animator anim;
    private Rigidbody playerRb;
    private float speed = 7f;
    private float runSpeed = 14f;
    private float jumpSpeed = 7f;
    private float yGravitySpeed;

    public Transform groundCheck;
    private float groundDistance = 0.3f;
    public LayerMask groundMask;

    private PlayerAnimationChannel m_Channel;
    [SerializeField] bool isGrounded;
    void Start()
    {
        //anim = GetComponentInChildren<Animator>();
        playerRb = GetComponent<Rigidbody>();
        m_Channel = GetComponentInChildren<PlayerAnimationChannel>();
        //Enemies = GameObject.FindGameObjectsWithTag("Enemy");
    }

    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticallInput = Input.GetAxis("Vertical");

        //anim.SetFloat("Forward", verticallInput);
        m_Channel.AnimationFloat("Forward", verticallInput);
        //anim.SetFloat("Sides", horizontalInput);
        m_Channel.AnimationFloat("Sides", horizontalInput);

        if (verticallInput != 0)
        {
            transform.Translate(Vector3.forward * Time.deltaTime * speed * verticallInput);
            if (Input.GetKey(KeyCode.LeftShift) && verticallInput > 0)
            {
                m_Channel.AnimationBool("isRunFast", true);
                transform.Translate(Vector3.forward * Time.deltaTime * runSpeed * verticallInput);
            }
            else
            {
                m_Channel.AnimationBool("isRunFast", false);
            }
        }
        else m_Channel.AnimationBool("isRunFast", false);

        if (horizontalInput != 0)
            transform.Translate(Vector3.right * Time.deltaTime * speed * horizontalInput);

        yGravitySpeed += Physics.gravity.y * Time.deltaTime;

        if (isGrounded)
        {
            yGravitySpeed = 0f;
            m_Channel.AnimationBool("isGrounded", true);
            m_Channel.AnimationBool("isJumping", false);
            m_Channel.AnimationBool("isFalling", false);
            if (Input.GetKeyDown(KeyCode.Space))
            {
                yGravitySpeed = jumpSpeed;
                m_Channel.AnimationBool("isJumping", true);
            }
            playerRb.AddForce(Vector3.up * yGravitySpeed, ForceMode.Impulse);
        }
        else
        {
            m_Channel.AnimationBool("isGrounded", false);
            if (yGravitySpeed < 0f || yGravitySpeed < 2f)
            {
                m_Channel.AnimationBool("isFalling", true);
            }
        }
    }

    /*    public static bool fruitInsideTrigger;
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Fruit"))
                fruitInsideTrigger = true;
        }
        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.CompareTag("Fruit"))
                fruitInsideTrigger = false;
        }*/
}

/*Vector3 movement = new Vector3(horizontalInput, 0, verticallInput);
float magnitude = Mathf.Clamp01(movement.magnitude) * speed;
movement.Normalize();
velocity = movement * magnitude;*/
/*    [SerializeField] public static float defaultSpeed = 8f;
    [SerializeField] public static float defaultRunSpeed = 14f;
    [SerializeField] public static float speed = 8f;
    [SerializeField] public static float runSpeed = 14f;
    private float walkSideSpeed = 5f;
    private float jumpForce = 7.1f;
    [SerializeField] bool isOnGround = true;
    private float horizontalInput;
    private float forwardInput;
    private Rigidbody playerRb;
    public static Animator anim;
    private float height;
    RaycastHit hit;
    [SerializeField] Transform ground;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        anim = GetComponentInChildren<Animator>();
    }
    
    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        forwardInput = Input.GetAxis("Vertical");

        //set running animation conditions
        if (forwardInput == 0)
        {
            anim.SetBool("isRun", false);
            anim.SetBool("isRunBack", false);
        }
        if (forwardInput > 0)
        {
            anim.SetBool("isRun", true);
            if (Input.GetKey(KeyCode.LeftShift))
            {
                anim.SetBool("isRunFast", true);
                transform.Translate(Vector3.forward * Time.deltaTime * runSpeed * forwardInput);
            }
            else
            {
                anim.SetBool("isRunFast", false);
                transform.Translate(Vector3.forward * Time.deltaTime * speed * forwardInput);
            }
        }
        if (forwardInput < 0)
        {
            anim.SetBool("isRunBack", true);
            transform.Translate(Vector3.forward * Time.deltaTime * speed * forwardInput);
        }

        if (forwardInput == 0)
        {
            anim.SetBool("isWalkRight", false);
            anim.SetBool("isWalkLeft", false);
        }

        if (horizontalInput > 0)
        {
            anim.SetBool("isWalkRight", true);
            transform.Translate(Vector3.right * Time.deltaTime * walkSideSpeed * horizontalInput);
        }

        if (horizontalInput < 0)
        {
            anim.SetBool("isWalkLeft", true);
            transform.Translate(Vector3.right * Time.deltaTime * walkSideSpeed * horizontalInput);
        }


        //anim.SetBool("isGrounded", true);

        if (Input.GetKey(KeyCode.Space) && isOnGround)
        {
            anim.SetBool("isJumpUp", true);
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isOnGround = false;
            anim.SetBool("isFall", true);
        }
        else if (isOnGround)
        { 
            anim.SetBool("isLand", true);
        }
        else
        {
            anim.SetBool("isJumpUp", false);
            anim.SetBool("isFall", false);
        }

        Ray ray = new Ray(transform.position, Vector3.down);
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.tag == "Ground")
            {
                height = hit.distance;
                //if (height  
            }
        }


        if (EnemyController.isDead)
        {
            StartCoroutine(WinTime());
        }
*//*        if (Input.GetKeyDown(KeyCode.E))
            StartCoroutine(waitForAnimation());*//*
    IEnumerator WinTime()
    {
        anim.SetBool("isWin", true);
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition;
        yield return new WaitForSeconds(3f);
        Time.timeScale = 0f;
    }
        

    }
*/
