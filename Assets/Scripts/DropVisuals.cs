using UnityEngine;
using UnityEngine.UI;
using Rive;
using Rive.Components;
using System.Collections;
public class DropVisuals : MonoBehaviour
{
    [SerializeField] RiveWidget rW;
    [SerializeField] Canvas c;
    Rigidbody2D rb;
    SMINumber v;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        v = rW.StateMachine.GetNumber("Velocity");
        c.worldCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        v.Value = Mathf.InverseLerp(-5f, -15f, rb.linearVelocity.y) * 18f;
    }
    IEnumerator eat()
    {
        rW.StateMachine.GetTrigger("Splash").Fire();

        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        yield return new WaitForSeconds(0.7f);
        Destroy(gameObject);
    }
    public void Eat()
    {
        StartCoroutine(eat());
    }
}
