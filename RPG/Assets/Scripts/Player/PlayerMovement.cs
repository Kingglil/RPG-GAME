using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public static Rigidbody rb;
    
    private float multipliers;
    private float deltaTime;

    private bool jumping = false;

    void Start()
    {
        transform.position = Player.position;
        rb = GetComponent<Rigidbody>();
        Time.fixedDeltaTime = 0.016f;
    }

    void FixedUpdate()
    {
        rb.velocity = new Vector3(0, rb.velocity.y, 0);

        deltaTime = AbilitiesManager.stopTime ? Time.unscaledDeltaTime : Time.deltaTime;

        multipliers = Player.moveSpeed;
        multipliers *= AbilitiesManager.stopTime ? Player.speedBoost * deltaTime : deltaTime;

        if (Input.GetButton("Jump") && !jumping)
        {
            jumping = true;
            
            rb.AddForce(Vector3.up * Player.jumpForce * deltaTime * 1000);
        }

        rb.MovePosition(transform.position + transform.forward * Input.GetAxis("Vertical") * multipliers + transform.right * Input.GetAxis("Horizontal") * multipliers);
        Player.position = transform.position;        
    }

    private void OnCollisionStay(Collision collision)
    {
        if (rb.velocity.y <= 0.05f)
        {
            jumping = false;
        }
    }
}
