using UnityEngine;

public class Gravity : MonoBehaviour 
{
    private const float G = 6.67f;

    public Rigidbody player;
    private Rigidbody rb;

	void Start () 
	{
        rb = GetComponent<Rigidbody>();
	}
	
	void FixedUpdate () 
	{
        Vector3 position = Player.position;
        position.y = transform.position.y;

        rb.MovePosition(position);

        Vector3 direction = rb.position - player.position;
        float distance = direction.magnitude / 4 + 6.5f;

        float forceMagnitude = G * (rb.mass * player.mass) / (distance * distance);
        Vector3 force = direction.normalized * forceMagnitude;

        player.AddForce(force);
	}
}
