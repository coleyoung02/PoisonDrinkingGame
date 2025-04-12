using System.Collections;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI tm;
    [SerializeField] private float basePoisionTolerance;
    [SerializeField] private int baseMaxDrops;
    [SerializeField] private PlayerControls pc;
    [SerializeField] private PoisonBarFrontend pbfe;
    [SerializeField] private Rain rain;
    private float poisonTolerance;
    private int maxDrops;
    private int orphansSaved;

    private int poisonSwallowed;

    private void Awake()
    {
        poisonSwallowed = 0;
        poisonTolerance = basePoisionTolerance;
        maxDrops = baseMaxDrops;
    }

    public void SwallowDrop()
    {
        orphansSaved += 1;
        poisonSwallowed += 1;
        pbfe.UpdatePoisonValue(poisonSwallowed / (float)maxDrops);
        tm.text = "Orphans saved: " + orphansSaved;
        if (poisonSwallowed > poisonTolerance)
        {
            Debug.LogError("you died");
        }

    }

    public void StartDripping(float fillRatio)
    {
        StartCoroutine(WaitAndDrip(fillRatio));
        pbfe.ShowBar();
    }

    private IEnumerator WaitAndDrip(float fillRatio)
    {
        yield return new WaitForSeconds(1.5f);
        rain.StartDripping(Mathf.RoundToInt(maxDrops * fillRatio));
        pbfe.UpdateDeathValue(poisonTolerance / (maxDrops * fillRatio));
    }

    public void ResetPoisonMeter()
    {
        poisonSwallowed = 0;
        pbfe.UpdatePoisonValue(0);
    }

    public void BoostTolerance()
    {
        poisonTolerance *= 1.25f;
        // set on poison bar here
        pbfe.UpdateDeathValue(poisonTolerance / (float)maxDrops);
    }

    public void BoostDrips()
    {
        maxDrops = Mathf.RoundToInt(maxDrops * 1.25f);
    }

    public void BoostAcceleration()
    {
        pc.BoostAcceleration();
    }


}
