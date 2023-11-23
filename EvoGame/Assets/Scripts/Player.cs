using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public AudioSource biteAudio; //Player Audiocontrol for bite effect
    //using rigidbody for movement, so no weird physic-fuck-ups
    //TODO: ✔physmat for player object
    //TODO: enemy collision event

    [SerializeField] float movementSpeed = 100f;
    
    Vector2 input;
    Rigidbody2D rb;
    
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    
    void Update()
    {
        input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    }


    void FixedUpdate()
    {
        PlayerMovement(input);
    }


    void PlayerMovement(Vector2 direction)
    {
        rb.velocity = direction * movementSpeed * Time.fixedDeltaTime;
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy") 
        { 
            biteAudio.Play();
        }
    }
}
