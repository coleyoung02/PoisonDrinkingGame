using System.Collections;
using UnityEngine;

public class Rain : MonoBehaviour
{
    [SerializeField] private GameObject poisonDrop;
    [SerializeField] private Transform leftBound;
    [SerializeField] private Transform rightBound;
    [SerializeField] private GameManager gm;
    [SerializeField] private float acceleration;
    [SerializeField] private float difficultyScale;
    private float velocity;
    private float position;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        acceleration = 2f;
        velocity = 0f;
        position = leftBound.position.x + (rightBound.position.x - leftBound.position.x) / 4;
    }

    public void StartDripping(int count)
    {
        StartCoroutine(DripRoutine(count));
        StartCoroutine(JoinkRoutine(2f / difficultyScale));
    }

    // Update is called once per frame
    void Update()
    {
        velocity += acceleration * Time.deltaTime;
        position += velocity * Time.deltaTime;
        transform.position = new Vector3(position, transform.position.y, transform.position.z);
        if (position > rightBound.position.x)
        {
            position = rightBound.position.x;
            velocity = Mathf.Min(velocity, 0);
            acceleration = Mathf.Min(acceleration, -UnityEngine.Random.Range(2, 4));
        }
        else if (position < leftBound.position.x)
        {
            position = leftBound.position.x;
            velocity = Mathf.Max(velocity, 0);
            acceleration = Mathf.Max(acceleration, UnityEngine.Random.Range(2, 4));
        }
    }

    private IEnumerator DripRoutine(int count)
    {
        Instantiate(poisonDrop, transform.position, Quaternion.identity);
        yield return new WaitForSeconds(.25f);
        if (count - 1 > 0)
        {
            StartCoroutine(DripRoutine(count - 1));
        }
        else
        {
            yield return new WaitForSeconds(2f);
            gm.EndWave();
        }
    }

    private IEnumerator JoinkRoutine(float f)
    {
        yield return new WaitForSeconds(f);
        if (UnityEngine.Random.Range(0, 6) > 2f)
        {
            acceleration = UnityEngine.Random.Range(2f * difficultyScale, 5f * difficultyScale) * (UnityEngine.Random.Range(0, 2) * 2 - 1);
        }
        else
        {
            acceleration = UnityEngine.Random.Range(2f * difficultyScale, 5f * difficultyScale) * -Mathf.Sign(acceleration);
        }
        StartCoroutine(JoinkRoutine(UnityEngine.Random.Range(4.5f / difficultyScale, 12f / difficultyScale)));
    }
}
