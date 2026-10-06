using UnityEngine;
using UnityEngine.SceneManagement;

public class TrueStart : MonoBehaviour
{
    public Transform player;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    public void startGame()
    {
        player.transform.position = new Vector2(7.96f, 0.58f);
        SceneManager.LoadScene("Level1");
    }
}
