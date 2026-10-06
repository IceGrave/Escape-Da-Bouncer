using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour
{
    public static MusicManager instance;
    public AudioSource audioSource;
    [SerializeField] private AudioClip Title;
    [SerializeField] private AudioClip Level1;
    [SerializeField] private AudioClip Level2;
    [SerializeField] private AudioClip Level3;
    [SerializeField] private AudioClip End;
    private void Awake()
    {
        // Ensures that the music stays throughout the game and doesn't get destroyed. 
        audioSource = GetComponent<AudioSource>();
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Chooses which background music to play based on what level Player is on.
        if (scene.buildIndex == 1) 
        {
            audioSource.clip = Title;
        }
        if (scene.buildIndex == 2)
        {
            audioSource.clip = Level1;
        }
        if (scene.buildIndex == 3)
        {
            audioSource.clip = Level2;
        }
        if (scene.buildIndex == 4)
        {
            audioSource.clip = Level3;
        }
        if (scene.buildIndex == 5)
        {
            audioSource.clip = End;
        }
        playMusic();
    }
    public void stopMusic()
    {
        if (audioSource.isPlaying)
        {
            audioSource.Stop();
        }
    }
    public void playMusic()
    {
        if (!audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }
}
