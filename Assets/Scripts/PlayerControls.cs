using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private Rigidbody2D rb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 moveVec = Vector2.zero;
        if (Input.GetKey(KeyCode.A))
        {
            moveVec += Vector2.left;
        }
        if (Input.GetKey(KeyCode.D))
        {
            moveVec += Vector2.right;
        }
        rb.linearVelocity = moveVec * moveSpeed;
    }
}
