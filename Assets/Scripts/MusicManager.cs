using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AudioSource))]
public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance { get; private set; }

    [Header("Music Settings")]
    [SerializeField] private AudioClip[] songs;

    private AudioSource audioSource;
    private int currentSongIndex = 0;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        DontDestroyOnLoad(gameObject);

        audioSource = GetComponent<AudioSource>();
        audioSource.playOnAwake = false;

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void Start()
    {
        if (songs != null && songs.Length > 0)
        {
            int randomSong = Random.Range(0, songs.Length);
            PlaySong(randomSong);
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (audioSource == null) return;

        if(scene.name == "TitleScreen" && !audioSource.isPlaying)
        {
            PlayMusic();
        }
        else if (scene.name == "GameOverScreen")
        {
            StopMusic();
        }
        else if (scene.name == "SampleScene" && !audioSource.isPlaying)
        {
            PlayMusic();
        }
    }


    public void PlaySong(int index)
    {
        if (songs == null || songs == null || songs.Length == 0) return;

        currentSongIndex = (index + songs.Length) % songs.Length;

        audioSource.clip = songs[currentSongIndex];
        audioSource.Play();
    }

    public void NextSong()
    {
        PlaySong(currentSongIndex + 1);
    }

    public void PreviousSong()
    {
        PlaySong(currentSongIndex - 1);
    }

    public void StopMusic()
    {
        if (audioSource != null)
        {
            audioSource.Stop();
        }
    }

    public void PlayMusic()
    {
        if (audioSource == null) return;

        if (audioSource.clip != null)
        {
            audioSource.Play();
        }
        else if (songs != null && songs.Length > 0)
        {
            PlaySong(currentSongIndex);
        }
    }

    public string GetCurrentSongName()
    {
        if (audioSource.clip == null)
            return "No song playing";
        return audioSource.clip.name;
    }

    private void OnDestory()
    {
        if (Instance == this)
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
            Instance = null;
        }
    }
}


