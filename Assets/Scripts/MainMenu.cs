using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void Play()
    {
        SceneManager.LoadScene("Plains");
    }

    public void Quit()
    {
        Debug.Log("QUIT");
        Application.Quit();
    }
}
