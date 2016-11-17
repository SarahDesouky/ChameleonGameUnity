using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Audio;

public class CanvasScript : MonoBehaviour {

    public GameObject PauseLabel;
    public GameObject resumeButton;
    public GameObject quitButton;
    public GameObject Player;
    public GameObject GameOverLabel;
    public GameObject FinalScoreLabel;
    public GameObject RestartButton;
    public GameObject MenuButton;
    public GameObject score;
    public AudioSource srcMenu;
    public AudioSource srcMain;
    public AudioSource srcPurple;
    public AudioSource srcbtnclick;
    public GameObject pscore;
    public AudioSource gameOversrc;
    public AudioMixerGroup mixer;
    public AudioSource rollingsrc;


    bool mute;
    void Start () {
        if (PlayerPrefs.GetFloat("mute") == 1)
        {
            mute = true;
            rollingsrc.GetComponent<AudioSource>().mute = true;
        }
        else
        {
            rollingsrc.GetComponent<AudioSource>().mute = false;


        }
    }
	
	void Update () {

	
	}
    public void Resume()
    {
        Player.GetComponent<Rigidbody>().isKinematic = false;
        if(!mute)
            srcbtnclick.GetComponent<AudioSource>().Play();
        PauseLabel.SetActive(false);
        resumeButton.SetActive(false);
        quitButton.SetActive(false);
        RestartButton.SetActive(false);
        MenuButton.SetActive(false);
        Player.GetComponent<PlayerScript>().pause = false;
        score.SetActive(true);
        pscore.SetActive(true);
        if(PlayerPrefs.GetFloat("mute")==0)
        {
            mute = false;
            if (Player.GetComponent<PlayerScript>().purpleitem)
                srcPurple.GetComponent<AudioSource>().mute = false;
            else
                srcMain.GetComponent<AudioSource>().mute = false ;
            srcMenu.GetComponent<AudioSource>().mute = true; 
        }
        else
        {
            mute = true;
        }
    }

    public void Menu()
    {
        if (!mute)
        {
            srcbtnclick.GetComponent<AudioSource>().Play();
        }
        Application.LoadLevel(0);
    }

    public void Quit()
    {
        if (!mute)
        {
            srcbtnclick.GetComponent<AudioSource>().Play();
        }
        PlayerPrefs.DeleteAll();
        Application.Quit();

    }

    public void gameOver()
    {
        MenuButton.SetActive(true);
        RestartButton.SetActive(true);
        GameOverLabel.SetActive(true);
        FinalScoreLabel.SetActive(true);
        MenuButton.SetActive(true);
        pscore.SetActive(false);
        Player.GetComponent<PlayerScript>().pause = true;
        rollingsrc.GetComponent<AudioSource>().mute = true;
        FinalScoreLabel.GetComponent<Text>().text = "Playthrough Score: " + Player.GetComponent<PlayerScript>().maxScore.ToString();
        score.SetActive(false);
        if(!mute)
        {
            srcMain.GetComponent<AudioSource>().mute = true;
            srcPurple.GetComponent<AudioSource>().mute = true;
            //srcbtnclick.GetComponent<AudioSource>().Play();
            StartCoroutine(gameOverAudio());

        }
    }
    public IEnumerator gameOverAudio()
    {
        gameOversrc.GetComponent<AudioSource>().Play();
        yield return new WaitForSeconds(gameOversrc.GetComponent<AudioSource>().clip.length);
        srcMenu.GetComponent<AudioSource>().mute = false;
    }

    public void Restart()
    {
        if (!mute)
        {
            srcbtnclick.GetComponent<AudioSource>().Play();
        }
        Application.LoadLevel(1);
    }

    //public void Mute()
    //{
    //    if(musicToggle.isOn)
    //    {
    //        PlayerPrefs.SetFloat("mute", 0);
    //        mixer.audioMixer.SetFloat("Volume", 0);
    //    }
    //    else
    //    {
    //        PlayerPrefs.SetFloat("mute", 1);
    //        mixer.audioMixer.SetFloat("Volume", -80);
    //    }
        
    //}



   

}
