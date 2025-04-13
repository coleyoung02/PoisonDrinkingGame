using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI tm;
    public void Quit()
    {
        Application.Quit();
    }

    public void StartGame()
    {

        TransitionManager.instance.StartGame();
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    private void Start()
    {
        if (tm != null)
        {
            tm.text = "You saved " + AudioManager.orphansSaved + " orphans.";
        }
    }
}
