using UnityEngine;

public class Player : MonoBehaviour
{
    //using rigidbody for movement, so no weird physic-fuck-ups
    //TODO: ✔ physmat for player object
    //TODO: ✔ enemy collision event
    //TODO: ✔ xp pick up system
    //TODO: ✔ player health system
    //TODO: ✔ lvlup event
    //TODO: ✔ separate damage boosts
    //TODO: ✔ health boost
    //TODO: ✔ recovery boost
    //TODO: ✔ revamp controller (mouse input)
    //TODO: Animations
    //TODO: MORE UPGRADES
    //TODO: ✔ attack controller spawn positions
    //TODO: Balancing the numbers

    float lastHorizontal;
    float lastVertical;
    public Animator animator;
    Vector2 input;
    Rigidbody2D rb;
    bool facingDir = true;
    [HideInInspector] public Vector2 moveDirection;
    [HideInInspector] public Vector2 lastMoveDirection;
    public PlayerStats playerStats;

    public Dash dashEvolution;
    
    
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerStats = GetComponent<PlayerStats>();
        //dashEvolution = GetComponent<Dash>();
    }

    
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            if (dashEvolution != null)
                dashEvolution.ActivateDash();
            else 
                return;
                
        }
        FlipController();
        MovementController();
    }


    void FixedUpdate()
    {
        rb.MovePosition(rb.position + input * playerStats.CurrentMovementSpeed * Time.fixedDeltaTime);

    }


    void MovementController()
    {
        if (GameManager.instance.isGameOver || GameManager.instance.isPaused)
            return;

        input.x = Input.GetAxisRaw("Horizontal");
        input.y = Input.GetAxisRaw("Vertical");

        //needed for the attack scripts
        if (input.x != 0 || input.y != 0)
            {
                animator.SetBool("isMoving", true);
                lastHorizontal = input.x;
                lastMoveDirection = new Vector2(lastHorizontal, lastVertical);
            }
        else
            animator.SetBool("isMoving", false);
        

        //moveDirection = new Vector2(input.x, input.y); 
    }


    void FlipController()
    {
        if (GameManager.instance.isGameOver || GameManager.instance.isPaused)
            return;

        if (input.x < 0 && facingDir)
            Flip();
        else if (input.x > 0 && !facingDir)
            Flip();
    }


    void Flip()
    {
        facingDir = !facingDir;
        transform.Rotate(0,180,0);
    }
  
}
