using UnityEngine;
using UnityEngine.SceneManagement;


public class Intro : MonoBehaviour
{

    public UnityEngine.Rendering.Universal.Light2D light2D;
    public AudioSource intro;
    private float elapsedTime;

    // Start is called before the first frame update
    void Start()
    {
        //Checks if it's the first time the player is logging. Case yes, create the PlayerPref standard attributes.
        if (!PlayerPrefs.HasKey("windowed") || !PlayerPrefs.HasKey("language") || !PlayerPrefs.HasKey("musicVolume") || !PlayerPrefs.HasKey("soundVolume"))
        {
            PlayerPrefs.SetInt("windowed", 1); //0 for fullscreen, 1 for windowed
            PlayerPrefs.SetInt("language", 0); //0 for English, 1 for Portuguese
            PlayerPrefs.SetFloat("musicVolume", 0.5f); // from 0 to 1
            PlayerPrefs.SetFloat("soundVolume", 0.5f); // from 0 to 1
            PlayerPrefs.SetInt("isMuted", 0); //0 for not muted, 1 for muted
            PlayerPrefs.SetInt("savedGame", 0); //0 for no saved game, 1 for saved game
            PlayerPrefs.Save();
        }

        intro.volume = PlayerPrefs.GetFloat("musicVolume");
        if(PlayerPrefs.GetInt("isMuted") == 1)
        {
            intro.mute = true;
        }
        else
        {
            intro.mute = false;
        }
        intro.Play();
        elapsedTime = 0;
        light2D.intensity = 0.2f;

        if(PlayerPrefs.GetInt("windowed") == 0)
        {
            Screen.fullScreen = true;
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKey)
        {
            SceneManager.LoadScene("Main Game Scene");
        }

        if(light2D.intensity < 0.8f)
        {
            light2D.intensity += 0.003f;
        }

        if(elapsedTime > 5)
        {
            SceneManager.LoadScene("Main Game Scene");
        }

        elapsedTime += Time.deltaTime;

    }
}
