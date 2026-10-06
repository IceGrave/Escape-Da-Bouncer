using UnityEngine;

public class Scroll : MonoBehaviour
{
    private Transform player;
    public float cameraFollow = 6f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = FindAnyObjectByType<player>().transform;
    }

    // Update is called once per frame
    void Update()
    {
        // The camera stops moving to the left when the player is at an x value of -2 or less.
        if (player.position.x <= -2)
        {
            return;
        }
        Vector3 targetPosition = new Vector3(player.position.x, 0, -10);
        transform.position = Vector3.Lerp(transform.position, targetPosition, cameraFollow * Time.deltaTime);
    }
}
