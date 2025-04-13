using UnityEngine;
using System.Collections;
public class MouthBox : MonoBehaviour
{

    [SerializeField] Animator ani;
    private GameManager gameManager;

    private void Start()
    {
        gameManager = FindAnyObjectByType<GameManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.GetComponent<DropVisuals>().Eat();

        StartCoroutine(bandaid());
        Destroy(collision);
        gameManager.SwallowDrop();
    }
    IEnumerator bandaid()
    {
        ani.Play("New Animation");
        yield return null;
        ani.Play("eat");
    }
}


