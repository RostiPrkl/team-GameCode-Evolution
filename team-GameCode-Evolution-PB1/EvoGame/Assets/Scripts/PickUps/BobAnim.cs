using UnityEngine;

public class BobAnim : MonoBehaviour
{
    [SerializeField] float bobSpeed = 20f;
    [SerializeField] float bobJump = 10f;
    [SerializeField] Vector3 direction;
    Vector3 initPos;
    Pickup pickup;


    void Start()
    {
        pickup = GetComponent<Pickup>();
        initPos = transform.position;
    }


    void Update()
    {
        if (pickup && !pickup.isCollected)
            transform.position = initPos + direction * Mathf.Sin(Time.time * bobSpeed) * bobJump;
    }
}
