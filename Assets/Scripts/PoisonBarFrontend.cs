using UnityEngine;
using Rive;
using Rive.Components;

public class PoisonBarFrontend : MonoBehaviour
{

    [SerializeField] float poisonValue;
    [SerializeField] float deathThreshold;
    [SerializeField] RiveWidget rW;
    float maxPoisonValue = 1f;
    SMINumber difference;
    SMINumber death;
    SMINumber poison;

    //Shows the bar
    public void ShowBar()
    {
        gameObject.SetActive(true);
        rW.StateMachine.GetTrigger("Open").Fire();
        UpdateDeathValue(deathThreshold);
        UpdateMaxPoisonValue(maxPoisonValue);
        UpdatePoisonValue(poisonValue);
    }

    //Hides the bar
    public void HideBar()
    {
        gameObject.SetActive(false);
    }
    //Takes the raw poison value, the bar adjusts based on the max poison that the player can consume.
    public void UpdatePoisonValue(float value)
    {
        poisonValue = value;
        poison.Value = (poisonValue / maxPoisonValue) * 100f;
        difference.Value = Mathf.Abs(poison.Value - death.Value);
    }
    //Used to set the death threshold
    public void UpdateDeathValue(float value)
    {
        deathThreshold = value;
        death.Value =(deathThreshold/ maxPoisonValue) * 100f;
        difference.Value = Mathf.Abs(poison.Value - death.Value);
    }
    //Updates the max poison that the player can consume.
    public void UpdateMaxPoisonValue(float value)
    {
        maxPoisonValue = value;
        UpdatePoisonValue(poisonValue);
        UpdateDeathValue(deathThreshold);
    }

    void Start()
    {
        difference = rW.StateMachine.GetNumber("Difference");
        death = rW.StateMachine.GetNumber("Death");
        poison = rW.StateMachine.GetNumber("Poison");
        
    }
}
