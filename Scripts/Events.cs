using UnityEngine.SceneManagement;
using UnityEngine;

public class Events : MonoBehaviour
{
  public void ReplayGame()
    {
        SceneManager.LoadScene("Level1");
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
