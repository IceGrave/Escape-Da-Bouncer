using UnityEngine;
using UnityEngine.SceneManagement;

public class Logic : MonoBehaviour
{
    public GameObject caughtText;
    public void gameOver()
    {
        caughtText.SetActive(true);
        Invoke("restartGame", 1f);
    }
    public void restartGame()
    {
        caughtText.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
