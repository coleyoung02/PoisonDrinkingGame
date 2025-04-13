using UnityEngine;
using Rive;
using Rive.Components;
using UnityEngine.SceneManagement;
public class TransitionManager : MonoBehaviour
{
    bool cancelOut = false;
    [SerializeField] RiveWidget rW;
    public static TransitionManager instance;
    SMITrigger open;
    SMITrigger close;
    SMINumber icon;
    GameManager gm;
    void Start()
    {
        rW.OnRiveEventReported += OnEvent;
        open = rW.StateMachine.GetTrigger("In");
        icon = rW.StateMachine.GetNumber("Icon");
        close = rW.StateMachine.GetTrigger("Out");
        icon.Value = 1;
        open.Fire();



    }
    public void OnEvent(ReportedEvent et)
        {
        Debug.Log(et.Name);
        
        if (et.Name == "ResetButtons")
        {
            cancelOut = false;
            return;
        }
        
        if (gm == null)
        {
            close.Fire();
            return;
        }
        if (cancelOut) return;
        if (et.Name == "LeakierPipes")
        {
            cancelOut = true;
            gm.BoostDrips();
            close.Fire();
        }
        if (et.Name == "StomachOfIron")
        {
            cancelOut = true;
            gm.BoostTolerance();
            close.Fire();
        }
        if (et.Name == "Quicker")
        {
            cancelOut = true;
            gm.BoostAcceleration();
            close.Fire();
        }

    }
    void OnSceneLoaded(Scene s)
    {
        gm = FindFirstObjectByType<GameManager>();
    }
    void Awake()
    {
        if (instance == null) instance = this;
        if (instance == this)
        {
            DontDestroyOnLoad(this);
            
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
