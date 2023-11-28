using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour
{
    //using rigidbody for movement, so no weird physic-fuck-ups
    //TODO: ✔ physmat for player object
    //TODO: ✔ enemy collision event
    //TODO:   xp pick up system
    //TODO:   player health system
    //TODO:   lvlup event

    float lastHorizontal;
    float lastVertical;
    Vector2 input;
    Rigidbody2D rb;
    bool facingDir = true;

    [HideInInspector] public Vector2 moveDirection;
    [HideInInspector] public Vector2 lastMoveDirection;

    PlayerStats playerStats;
    
    
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerStats = GetComponent<PlayerStats>();
    }

    
    void Update()
    {
        MovementController();
        FlipController();
    }


    void FixedUpdate()
    {
        rb.MovePosition(rb.position + input * playerStats.currentMovementSpeed * Time.fixedDeltaTime);
    }


    void MovementController()
    {
        input.x = Input.GetAxisRaw("Horizontal");
        input.y = Input.GetAxisRaw("Vertical");

        //needed for the attack scripts
        if (input.x != 0)
            {
                lastHorizontal = input.x;
                lastMoveDirection = new Vector2(lastHorizontal, 0);
            }
        if (input.y != 0)
            {
                lastVertical = input.y;
                lastMoveDirection = new Vector2(0, lastVertical);
            }
        //fixes bug in ranged weapon while moving
        if (input.x != 0 && input.y != 0)
            lastMoveDirection = new Vector2(lastHorizontal, lastVertical);

        //moveDirection = new Vector2(input.x, input.y); 
    }


    void FlipController()
    {
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
