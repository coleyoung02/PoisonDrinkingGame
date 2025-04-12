using UnityEngine;
using Rive;
using Rive.Components;

public class PoisonBarFrontend : MonoBehaviour
{

    [SerializeField] int poisonValue;
    [SerializeField] int deathThreshold;
    [SerializeField] RiveWidget rW;
    int maxPoisonValue = 100;
    SMINumber difference;
    SMINumber death;
    SMINumber poison;

    //Shows the bar
    public void ShowBar()
    {
        gameObject.SetActive(false);
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
    public void UpdatePoisonValue(int value)
    {
        poisonValue = value;
        poison.Value = ((float)(((float)poisonValue / (float)maxPoisonValue) * 100f));
        difference.Value = Mathf.Abs(poison.Value - death.Value);


    }
    //Used to set the death threshold
    public void UpdateDeathValue(int value)
    {
        deathThreshold = value;
        death.Value = ((float)(((float)deathThreshold/ (float)maxPoisonValue) * 100f));
        difference.Value = Mathf.Abs(poison.Value - death.Value);
    }
    //Updates the max poison that the player can consume.
    public void UpdateMaxPoisonValue(int value)
    {
        maxPoisonValue = value;
        UpdatePoisonValue(poisonValue);
        UpdateDeathValue(deathThreshold);
    }
    void Update()
    {
        UpdatePoisonValue(poisonValue);
    }
    void Start()
    {
        difference = rW.StateMachine.GetNumber("Difference");
        death = rW.StateMachine.GetNumber("Death");
        poison = rW.StateMachine.GetNumber("Poison");
        
    }
}
