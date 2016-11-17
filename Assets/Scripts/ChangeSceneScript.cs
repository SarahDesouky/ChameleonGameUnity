using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ChangeSceneScript : MonoBehaviour {

    public AudioSource src;
    public GameObject MuteButton;
    bool mute;
    public AudioSource bclick;
    // Use this for initialization
    void Start() {
        if(!PlayerPrefs.HasKey("mute"))
        {
            mute = false;
            PlayerPrefs.SetFloat("mute", 0f);
        }
        if(PlayerPrefs.GetFloat("mute")== 1)
        {
            mute = true;
            src.GetComponent<AudioSource>().mute = true;
            MuteButton.GetComponent<Button>().GetComponentInChildren<Text>().text = "Un-Mute Sounds";
        }
        else
        {
            mute = false;
            src.GetComponent<AudioSource>().mute = false;
            MuteButton.GetComponent<Button>().GetComponentInChildren<Text>().text = "Mute Sounds";
        }

    }

    // Update is called once per frame
    void Update() {

    }

    public void ChangeScene(int sceneToChangeTo)
    {
        if (!mute)
            bclick.GetComponent<AudioSource>().Play();
        Application.LoadLevel(sceneToChangeTo);
    }

    public void Quit()
    {
        PlayerPrefs.DeleteAll();
        if(!mute)
            bclick.GetComponent<AudioSource>().Play();
        Application.Quit();
    }
    public void Menu()
    {
        if (!mute)
            bclick.GetComponent<AudioSource>().Play();
        Application.LoadLevel(0);
    }

    public void Mute()
    {
        if (mute == false)
        {
            bclick.GetComponent<AudioSource>().Play();
            mute = true;

            src.GetComponent<AudioSource>().mute = true;
            MuteButton.GetComponent<Button>().GetComponentInChildren<Text>().text = "Un-Mute Sounds";
            PlayerPrefs.SetFloat("mute", 1f);
        }
        else
        {
            mute = false;
            src.GetComponent<AudioSource>().mute = false;
            MuteButton.GetComponent<Button>().GetComponentInChildren<Text>().text = "Mute Sounds";
            PlayerPrefs.SetFloat("mute", 0f);

        }

    }
}
