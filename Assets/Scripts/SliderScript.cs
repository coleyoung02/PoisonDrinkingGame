using UnityEngine;
using UnityEngine.UI;
using System.Collections;
public class SliderScript : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private GameManager gm;
    [SerializeField] private OtherPoisonBarFrontend opb;
    private float poisonRatio = 0;
    private int delta = 1;
    private bool moving = true;

    private float moveRate = 1f;

    public void ResetSlider(float deathThresh, bool initial=false)
    {
        StartCoroutine(bandaid(deathThresh, initial));
    }
    IEnumerator bandaid(float deathThresh, bool initial = false)
    {
        yield return new WaitForSeconds(2.5f);
        if (initial)
        {
            opb.UpdateDeathValue(deathThresh);
            opb.ShowBar();
            yield break;
        }
        opb.ShowBar();
        opb.UpdateDeathValue(deathThresh);
        moving = true;
        delta = 1;
        poisonRatio = 0;
    }
    // Update is called once per frame
    void Update()
    {
        if (moving)
        {
            poisonRatio += Time.deltaTime * delta * moveRate;
            
        }
        if (poisonRatio > 1)
        {
            poisonRatio = 1;
            delta = -1;
        }
        else if (poisonRatio < 0)
        {
            poisonRatio = 0;
            delta = 1;
        }
        slider.value = poisonRatio;
        opb.UpdatePoisonValue(poisonRatio);

        if (moving && Input.GetKeyDown(KeyCode.Space))
        {
            moving = false;
            gm.StartDripping(slider.value);
            opb.HideBar();
        }
    }
}
