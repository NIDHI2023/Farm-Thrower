using UnityEngine;
[RequireComponent(typeof(CharacterController))] //script isnt allowed to go on any gameobject that doesnt have charcontroller attached to it

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;//value 10
    public float mouseSense; // value 5
    public float upDownLimit;
    public float jumpSpeed; //5 to 7

    private float verticalVelocity;
    private float desiredUpDown;
    private float rotYaw;
    private float rotRoll;
    private Camera theCam;

    private float forwardSpeed;
    private float strafeSpeed;
    private Vector3 speed;
    private CharacterController cController;
    public float sprintTimer;
    public float sprintCooldown;
    private float originalSpeed;
    public float limit;
    private bool start;

    public bool canDoubleJump;
    private bool isCrouch;
    private float ogCamera;

    public int enemiesKilled;
    private LevelEnd levelEnd;


    // Start is called before the first frame update
    void Start()
    {
        cController = GetComponent<CharacterController>();
        theCam = Camera.main;
        Cursor.visible = false;
        sprintTimer = 5;
        sprintCooldown = 0;
        originalSpeed = moveSpeed;
        limit = 3;
        start = false;
        isCrouch = false;
        enemiesKilled = 0;
        levelEnd = FindObjectOfType<LevelEnd>();
        ogCamera = theCam.transform.localPosition.y;
    }

    // Update is called once per frame
    void Update()
    {
        MoveAndRotate();
        Sprint();
        Crouch();

        //open ending area
        if (enemiesKilled >= levelEnd.enemyReq)
        {
            Destroy(GameObject.FindWithTag("Finish"));
        }

        //for traps:
        //make a boxCollider, check isTrigger, put it so it covers width of a room enterance. make lighting purple
        ////ontrigger enter set transform.pos to startPos
    }

    void MoveAndRotate()
    {
        rotYaw = Input.GetAxis("Mouse X") * mouseSense;
        transform.Rotate(0f, rotYaw, 0f);

        rotRoll = Input.GetAxis("Mouse Y") * mouseSense;
        desiredUpDown -= rotRoll; //if pos we look down if neg we look up. up is 356º??

        desiredUpDown = Mathf.Clamp(desiredUpDown, -upDownLimit, upDownLimit);

        //cant bend char controller so camera does it
        theCam.transform.localRotation = Quaternion.Euler(desiredUpDown, 0f, 0f);
        forwardSpeed = Input.GetAxis("Vertical") * moveSpeed;
        strafeSpeed = Input.GetAxis("Horizontal") * moveSpeed;
        verticalVelocity += Physics.gravity.y * Time.deltaTime; //vasically v = a * t. normally we would do this when we weren't on the ground, only when we jump

        //if(cController.isGrounded && Input.GetButtonDown("Jump"))
        //{
        //    verticalVelocity = jumpSpeed;
        //}


        if (Input.GetButtonDown("Jump"))
        {
            if (canDoubleJump)
            {
                verticalVelocity = jumpSpeed;
                canDoubleJump = false;
            } else
            {
                if (cController.isGrounded)
                {
                    verticalVelocity = jumpSpeed;
                    canDoubleJump = true;
                }
            }
        }

        speed = new Vector3(strafeSpeed, verticalVelocity, forwardSpeed);

        speed = transform.rotation * speed;

        cController.Move(speed * Time.deltaTime); //no gravity

    }

    void Crouch()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            if (isCrouch)
            {
                theCam.transform.localPosition = new Vector3(0f, ogCamera, 0f);
                isCrouch = false;
            } else
            {
                theCam.transform.localPosition = new Vector3(0f, ogCamera/3.5f, 0f);
                isCrouch = true;
            }
        }
    }

    void Sprint()
    {
        if (Input.GetKey(KeyCode.LeftShift) && sprintTimer > 0f && sprintCooldown == 0)
        {
            start = true;
            //sprintCooldown = 0;

            moveSpeed = originalSpeed * 1.5f;
            forwardSpeed = Input.GetAxis("Vertical") * moveSpeed;
            strafeSpeed = Input.GetAxis("Horizontal") * moveSpeed;
            sprintTimer -= Time.deltaTime;
            //Debug.Log("1");

        }

        else if (sprintCooldown < limit && sprintTimer < 0f)
        {
            moveSpeed = originalSpeed;
            limit = 3;
            sprintCooldown += Time.deltaTime;
            //Debug.Log("2");
        }
        else if (sprintCooldown < limit && sprintTimer > 0f && start)
        {
            moveSpeed = originalSpeed;
            limit = 1.5f;
            sprintCooldown += Time.deltaTime;
            //Debug.Log("3");
        }
        else
        {
            moveSpeed = originalSpeed;
            sprintTimer = 5;
            sprintCooldown = 0;
            start = false;
            //Debug.Log("4");

        }

    }
}
