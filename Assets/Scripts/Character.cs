using UnityEngine;

public class Character : MonoBehaviour
{
    [Header("Move variables")]
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float sprintSpeed = 10f;
    [SerializeField] float acceleration = 20f;

    [Header("Gravity/Jump")]
    [SerializeField] float gravity = -10f;
    [SerializeField] float jumpForce = 1.5f;

    Rigidbody2D rb;
    float InputX;
    public LayerMask groundLayer;

    [Header("Ground Check")]
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    private bool isGrounded;
    private bool wasGrounded;



    [Header("Health")]
    public int MaxHealth = 100;
    public float currentHealth;

    [Header("No Battery UI")]
    public GameObject noBatteryScreen;
    private bool noBattery = false;

    [Header("Respawn")]
    public Transform respawnPoint;


    [Header("Animation")]
    private Animator animator;
    private string walk = "Marche";
    private bool isJumping = false;


    float currentSpeed;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        currentHealth = MaxHealth;
        InvokeRepeating("DecreaseHealth", 1f, 1f);
        animator = GetComponentInChildren<Animator>();
        noBatteryScreen.SetActive(false);
    }

    void Update()
    {
        if (currentHealth <= 0 && !noBattery)
            BatteryEmpty();

        if (noBattery)
            return;

        InputX = Input.GetAxisRaw("Horizontal");


        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        // SAUT
        if (Input.GetButtonDown("Jump") && isGrounded && !isJumping)
        {
            isJumping = true;
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            animator.SetBool("Saut", true);
        }

        if (isGrounded && rb.linearVelocity.y <= 0)
        {
            isJumping = false;
            animator.SetBool("Saut", false);
        }


        // CHUTE
        if (rb.linearVelocity.y < -0.1f && !isGrounded)
            animator.SetBool("Saut", true);

        // VITESSE
        if (Input.GetKey(KeyCode.LeftShift))
            currentSpeed = sprintSpeed;
        else
            currentSpeed = moveSpeed;

        // MARCHE 
        if (!isJumping)
            animator.SetBool(walk, InputX != 0);
        else
            animator.SetBool(walk, false);
    }

    

    void FixedUpdate()
    {
        var v = rb.linearVelocity;
        v.x = InputX * currentSpeed;
        rb.linearVelocity = v;
    }

    void DecreaseHealth()
    {
        if (currentHealth > 0)
        {
            currentHealth -= 0.2f;
            currentHealth = Mathf.Max(currentHealth, 0f);
        }
        else
        {
            Debug.Log("Personnage mort !");
            CancelInvoke("DecreaseHealth");
        }
    }

public void Retry()
{
    Time.timeScale = 1f;

    currentHealth = MaxHealth;

    noBattery = false;

    noBatteryScreen.SetActive(false);

    transform.position = respawnPoint.position;
      
}

    public void QuitGame()
    {
        Application.Quit();
    }

    void BatteryEmpty()
    {
        noBattery = true;
        Debug.Log("BATTERY EMPTY");
        noBatteryScreen.SetActive(true);
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}