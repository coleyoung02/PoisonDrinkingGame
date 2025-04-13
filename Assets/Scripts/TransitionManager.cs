using UnityEngine;
using Rive;
using Rive.Components;
using System.Collections;
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
    void Open()
    {
        rW.transform.gameObject.SetActive(true);
        open.Fire();

    }
    void Close()
    {
        StartCoroutine(bandaid2());
    }
    IEnumerator bandaid2()
    {
        close.Fire();
        yield return new WaitForSeconds(2.5f);
        rW.transform.gameObject.SetActive(false);

    }
    void Start()
    {
        rW.OnRiveEventReported += OnEvent;
        open = rW.StateMachine.GetTrigger("In");
        icon = rW.StateMachine.GetNumber("Icon");
        close = rW.StateMachine.GetTrigger("Out");
       //icon.Value = 1;
       // open.Fire();



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
            Close();
            return;
        }
        if (cancelOut) return;
        if (et.Name == "LeakierPipes")
        {
            cancelOut = true;
            gm.BoostDrips();
            Close(1);
        }
        if (et.Name == "StomachOfIron")
        {
            cancelOut = true;
            gm.BoostTolerance();
            Close(1);
        }
        if (et.Name == "Quicker")
        {
            cancelOut = true;
            gm.BoostAcceleration();
            Close(1);
        }

    }
    void OnSceneLoaded(Scene s, LoadSceneMode m)
    {
        gm = FindFirstObjectByType<GameManager>();
        if (s.name == "MainMenu")
        {
            rW.transform.gameObject.SetActive(false);
        }
    }
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        if (instance != this)
        {
            Destroy(gameObject);

        }
 
            
        
    }
    public void OpenUpgrades()
    {
        icon.Value = 1;
        Open();
    }
    public void StartGame()
    {
        icon.Value = 0;
        rW.transform.gameObject.SetActive(true);
        StartCoroutine(bandaid());
    }
    IEnumerator bandaid()
    {
        icon.Value = 0;
        Open();
        yield return new WaitForSeconds(2.5f);
        SceneManager.LoadScene("Game");
        Close();
    }
    public void Close(int i)
    {
        gm.StartNextWave();
        StartCoroutine(bandaid2());
    }
}
