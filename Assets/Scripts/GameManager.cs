using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI tm;
    [SerializeField] private float basePoisionTolerance;
    [SerializeField] private int baseMaxDrops;
    [SerializeField] private PlayerControls pc;
    [SerializeField] private PoisonBarFrontend pbfe;
    [SerializeField] private Rain rain;
    [SerializeField] private SliderScript sliderOne;
    [SerializeField] private AudioClip dropNoise;
    private float poisonTolerance;
    private int maxDrops;
    private int orphansSaved;

    private int poisonSwallowed;
    private int waveNumber;

    [SerializeField] private GameObject upgradeUIHolder;
    [SerializeField] private GameObject newsUpgradeCheck;
    [SerializeField] private GameObject newsArticleHolder;
    [SerializeField] private List<GameObject> newsArticles;

    private bool willGetUpgrade;

    private void Awake()
    {
        willGetUpgrade = false;
        poisonSwallowed = 0;
        poisonTolerance = basePoisionTolerance;
        maxDrops = baseMaxDrops;
    }

    private void Start()
    {
        StartCoroutine(DelayConfig());
    }

    private IEnumerator DelayConfig()
    {
        yield return new WaitForSeconds(.1f);
        sliderOne.ResetSlider(poisonTolerance / maxDrops, true);
    }

    public void SwallowDrop()
    {
        orphansSaved += 1;
        AudioManager.Instance.PlayAudioClip(dropNoise);
        AudioManager.orphansSaved = orphansSaved;
        if (orphansSaved % 25 == 0)
        {
            willGetUpgrade = true;
        }
        poisonSwallowed += 1;
        pbfe.UpdatePoisonValue(poisonSwallowed / (float)maxDrops);
        tm.text = "Orphans saved: " + orphansSaved;
        if (poisonSwallowed > poisonTolerance + 1)
        {
            SceneManager.LoadScene("DeathScene");
        }

    }

    public void Win()
    {
        SceneManager.LoadScene("WinScene");
    }

    public void EndWave()
    {
        pbfe.HideBar();
        ResetPoisonMeter();
        foreach (GameObject g in newsArticles)
        {
            if (g != null)
            {
                g.SetActive(false);
            }
        }
        newsArticleHolder.SetActive(true);
        if (waveNumber < newsArticles.Count && newsArticles[waveNumber] != null)
        {
            newsUpgradeCheck.SetActive(false);
            newsArticles[waveNumber].SetActive(true);
        }
        else
        {
            NewsDone();
        }
        if (willGetUpgrade)
        {
            upgradeUIHolder.SetActive(true);
        }
        waveNumber++;
    }

    public void NewsDone()
    {
        if (!willGetUpgrade)
        {
            StartNextWave();
        }
        else
        {

            TransitionManager.instance.OpenUpgrades();
            upgradeUIHolder.SetActive(false);
        }
    }

    public void UpgradeDone()
    {
        upgradeUIHolder.SetActive(false);
        StartNextWave();
    }

    public void StartNextWave()
    {
        sliderOne.ResetSlider(poisonTolerance / maxDrops);
        willGetUpgrade = false;
    }

    public void StartDripping(float fillRatio)
    {
        pbfe.ShowBar();
        StartCoroutine(WaitAndDrip(fillRatio));
    }

    private IEnumerator WaitAndDrip(float fillRatio)
    {
        pbfe.UpdateDeathValue(poisonTolerance / (maxDrops * fillRatio));
        yield return new WaitForSeconds(1.5f);
        rain.StartDripping(Mathf.RoundToInt(maxDrops * fillRatio));
        
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
