using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public static bool isPaused = false;
    public GameObject pauseMenuUI;
    public PlayerAttack playerCanAttack;
    public Text textButtonPause;
    private bool isMuted = false;
    public AudioSource pauseClip;

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
        playerCanAttack.paused = false;
        AudioManager.instance.StopMusic();
        pauseClip.Stop();
        AudioManager.instance.PlayLastTrack();
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
        playerCanAttack.paused = true;
        AudioManager.instance.StopMusic();
        pauseClip.Play();
    }

    public void MuteUnmute()
    {
        if(isMuted == false)
        {
            isMuted = true;
            textButtonPause.text = "Sound Off";
            AudioListener.volume = 0f;
        }
        else
        {
            isMuted = false;
            textButtonPause.text = "Sound On";
            AudioListener.volume = 1f;
        }
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
