using UnityEngine;
using UnityEngine.UI;

public class SliderScript : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private GameManager gm;
    private float poisonRatio = 0;
    private int delta = 1;
    private bool moving = true;

    private float moveRate = 1f;

    public void ResetSlider()
    {
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

        if (moving && Input.GetKeyDown(KeyCode.Space))
        {
            moving = false;
            gm.StartDripping(slider.value);
        }
    }
}
