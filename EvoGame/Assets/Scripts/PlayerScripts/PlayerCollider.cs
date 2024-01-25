
using UnityEngine;

public class PlayerCollider : MonoBehaviour
{

    CircleCollider2D pickup;
    PlayerStats playerStats;
    [SerializeField] float pull;

    private Transform playerTransform;


    void Start()
    {
        playerStats = FindObjectOfType<PlayerStats>();
        pickup = GetComponent<CircleCollider2D>();

    }


    void Update()
    {
        pickup.radius = playerStats.CurrentPickupRadius;
        playerTransform = playerStats.transform;
    }


    void OnTriggerStay2D(Collider2D collider)
    {
        if(collider.gameObject.TryGetComponent(out ICollectible collectible))
        {
            Rigidbody2D rb = collider.gameObject.GetComponent<Rigidbody2D>();
            Vector2 forceDir = (playerTransform.position - collider.transform.position).normalized;
            rb.AddForce(forceDir * pull);
            collectible.Collect();
        }
    }


    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.TryGetComponent(out Rigidbody2D rb))
        {
            rb.velocity = Vector2.zero;
        }
    }
}
