using UnityEngine;

public class DeathFloor : MonoBehaviour
{
    [SerializeField] private AudioClip deathNoise;
    bool used = false;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!used)
        {
            AudioManager.Instance.PlayAudioClip(deathNoise);
            used = true;
        }
    }
}
