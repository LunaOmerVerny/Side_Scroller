using UnityEngine;

public class Character : MonoBehaviour
{
    
    [Header("Move variables")]
    [SerializeField] float moveSpeed= 5f;
    [SerializeField] float sprintSpeed = 10f;
    [SerializeField] float acceleration= 20f;

    [Header("Gravity/Jump")]
    [SerializeField] float gravity = -10f;
    [SerializeField] float jumpForce = 1.5f;


    Rigidbody2D rb;
    float InputX;
    public LayerMask groundLayer;

    [Header("Health")]
    public int MaxHealth = 100;
    public float currentHealth;

    [Header("Animation")]
    private Animator marche;
    private string walk = "Marche";

    float currentSpeed;


    void Awake()    
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        currentHealth = MaxHealth;
        InvokeRepeating("DecreaseHealth", 1f, 1f);

        marche = GetComponentInChildren<Animator>();
    }


    // Update is called once per frame
    void Update()
    {
        InputX = Input.GetAxisRaw("Horizontal");

        bool isGrounded = Physics2D.Raycast(transform.position, Vector2.down, 1.1f, groundLayer);

        if (Input.GetButtonDown("Jump") && isGrounded) rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);

       if (Input.GetKey(KeyCode.LeftShift))
           currentSpeed = sprintSpeed;
       else
           currentSpeed = moveSpeed;

        if (InputX != 0)
        {
            marche.SetBool(walk, true);
        }
        else
        {
            marche.SetBool(walk, false);
        }


       //if (currentHealth > 0)
              //currentHealth -= (int)(healthdown * Time.deltaTime);
      //currentHealth = Mathf.Max(currentHealth, 0);
        //Debug.Log("Santé: " + currentHealth);



        //input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        //input.Normalize();
    }

    void FixedUpdate()
    { 
        var v = rb.linearVelocity;
        v.x = InputX * currentSpeed;

        rb.linearVelocity = v;
        // rb.linearVelocity = input * moveSpeed;
    }

    void DecreaseHealth()
    {
        if (currentHealth > 0)
        {
            currentHealth -= 0.2f;
            currentHealth = Mathf.Max(currentHealth, 0f);
           // Debug.Log("Santé : " + currentHealth);
        }
        else
        {
            Debug.Log("Personnage mort !");
            CancelInvoke("DecreaseHealth"); // arrête quand mort
        }
    }
}
