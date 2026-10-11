using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using Unity.VisualScripting;

public class MusicWindow : MonoBehaviour
{
    [SerializeField] private GameObject musicWindow;
    [SerializeField] private Text songTitle;
    [SerializeField] private GameObject clickBlocker;

    private void Start()
    {
        musicWindow.SetActive(false);
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().name != "SampleScene")
            return;
        if (Keyboard.current != null && Keyboard.current.pKey.wasPressedThisFrame)
        {
            ToggleWindow();
        }
    }

    public void ToggleWindow()
    {
        bool showWindow = !musicWindow.activeSelf;
        musicWindow.SetActive(showWindow);

        if(clickBlocker != null)
        {
            clickBlocker.SetActive(showWindow);
        }

        if (showWindow)
        {
            UpdateSongTitle();
        }
    }

    public void CloseWindow()
    {
        musicWindow.SetActive(false);
    }
    public void NextSong()
    {
        if (MusicManager.Instance == null)
            return;

        MusicManager.Instance.NextSong();
        UpdateSongTitle();
    }

    public void PreviousSong()
    {
        if (MusicManager.Instance == null) return;
        MusicManager.Instance.PreviousSong();
        UpdateSongTitle();
    }

    private void UpdateSongTitle()
    {
        if (MusicManager.Instance != null && songTitle != null)
        {
            songTitle.text = MusicManager.Instance.GetCurrentSongName();
        }
    }

}
