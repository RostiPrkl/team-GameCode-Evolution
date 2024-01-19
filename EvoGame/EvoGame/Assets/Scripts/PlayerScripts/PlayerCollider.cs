
using UnityEngine;

public class PlayerCollider : MonoBehaviour
{

    CircleCollider2D pickup;
    PlayerStats playerStats;
    [SerializeField] float pull;


    void Start()
    {
        playerStats = FindObjectOfType<PlayerStats>();
        pickup = GetComponent<CircleCollider2D>();
    }


    void Update()
    {
        pickup.radius = playerStats.CurrentPickupRadius;
    }


    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.TryGetComponent(out ICollectible collectible))
        {
            //Animate the pickup to move towards player
            Rigidbody2D rb = collider.gameObject.GetComponent<Rigidbody2D>();
            Vector2 forceDir = (transform.position - collider.transform.position).normalized;
            rb.AddForce(forceDir * pull);
            collectible.Collect();
        }
    }
}
