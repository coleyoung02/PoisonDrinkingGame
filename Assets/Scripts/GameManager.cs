using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI tm;
    [SerializeField] private float basePoisionTolerance;
    [SerializeField] private int baseMaxDrops;
    private float poisonTolerance;
    private int maxDrops;
    private int orphansSaved;

    public int poisonSwallowed;

    private void Awake()
    {
        poisonTolerance = basePoisionTolerance;
        maxDrops = baseMaxDrops;
    }

    public void SwallowDrop()
    {
        orphansSaved += 1;
        poisonSwallowed += 1;
        tm.text = "Orphans saved: " + orphansSaved;
        if (poisonSwallowed > poisonTolerance)
        {
            Debug.LogError("you died");
        }

    }

}
