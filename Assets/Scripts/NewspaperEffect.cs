using System.Collections;
using UnityEngine;

public class NewspaperEffect : MonoBehaviour
{
    [SerializeField] private float effectDuration;
    [SerializeField] private float targetRotation;
    [SerializeField] private float startRotation;
    [SerializeField] private AudioClip newsNoise;

    private void OnEnable()
    {
        StartCoroutine(SpinIn());
        AudioManager.Instance.PlayAudioClip(newsNoise);
    }

    private IEnumerator SpinIn()
    {
        for (float f = 0f; f < effectDuration; f += Time.deltaTime)
        {
            yield return new WaitForEndOfFrame();
            transform.rotation = Quaternion.Euler(0, 0, Mathf.Lerp(startRotation, targetRotation, f / effectDuration));
            transform.localScale = new Vector3(f * 2 / effectDuration, f * 2 / effectDuration, f * 2 / effectDuration);
        }
        transform.rotation = Quaternion.Euler(0, 0, targetRotation);
        transform.localScale = new Vector3(2, 2, 2);
    }
}
