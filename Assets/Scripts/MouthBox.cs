using UnityEngine;

public class MouthBox : MonoBehaviour
{

    private GameManager gameManager;

    private void Start()
    {
        gameManager = FindAnyObjectByType<GameManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(collision.gameObject);
        gameManager.SwallowDrop();
    }
}
