using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float baseAcceleration;
    private float acceleration;
    [SerializeField] private Rigidbody2D rb;
    private float velocity;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        velocity = 0f;
        acceleration = baseAcceleration;
    }

    public void BoostAcceleration()
    {
        acceleration *= 1.5f;
    }

    private float UseAcceleration(bool left)
    {
        float acc;
        if (left)
        {
            acc = 2 + velocity / moveSpeed;
        }
        else
        {
            acc = 2 - velocity / moveSpeed;
        }
        return acc * acceleration;
    }

    // Update is called once per frame
    void Update()
    {
        bool inputPressed = false;
        if (Input.GetKey(KeyCode.A))
        {
            velocity -= UseAcceleration(true) * Time.deltaTime;
            inputPressed = true;
        }
        if (Input.GetKey(KeyCode.D))
        {
            velocity += UseAcceleration(false) * Time.deltaTime;
            inputPressed = true;
        }
        velocity = Mathf.Clamp(velocity, -moveSpeed, moveSpeed);
        if (!inputPressed)
        {
            if (velocity > 0f)
            {
                velocity = Mathf.Max(velocity - Time.deltaTime * acceleration * 3, 0);
            }
            else if (velocity < 0f)
            {
                velocity = Mathf.Min(velocity + Time.deltaTime * acceleration * 3, 0);
            }
        }
        rb.linearVelocity = new Vector2(velocity, 0);
    }
}
