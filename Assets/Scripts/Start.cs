using UnityEngine;
using UnityEngine.SceneManagement;

public class Start : MonoBehaviour
{
    public void startGame()
    {
        SceneManager.LoadScene("Opening");
    }
}
