using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour {

    public Text ScoreText;
    public float score;
    public bool purpleitem = false;
    public GameObject TileManager;
    public float speed;
    private float purpleitemdelay;
    public bool pause;
    public float maxScore;
    public GameObject rightWallPrefab;
    public GameObject leftWallPrefab;
    public AudioSource src1;
    public AudioSource src2;
    public AudioSource src3;
    public AudioSource collect;
    public AudioSource collects;
    public Text playthroughScore;
    public AudioSource correctsrc;
    bool mute;
    public AudioSource wrongsrc;
    public AudioSource jumpsrc;

    public GameObject canvas;
    public AudioSource rollingsrc;
    public AudioSource ballLandsrc;
    bool jumped;
    public AudioSource colorchangesrc;
    float timer;
    void Start () {
        score = 0;
        timer = 0;
        speed = 6f;
        ScoreText.text = "Score: " + score;
        purpleitemdelay = 6;
        maxScore = 0;
        playthroughScore.text = "Playthrough Score: " + maxScore;
        if (PlayerPrefs.GetFloat("mute") == 1)
            mute = true;

    }
	
	void Update () {
        var rb = this.gameObject.GetComponent<Rigidbody>();
        //speed = speed + (Time.deltaTime * acc);


        if (!pause)
        {
            int f = 5;

            //rb.AddForce(Vector3.forward * speed, ForceMode.Force);
            transform.position += Vector3.forward * Time.deltaTime * speed;

            //transform.Translate(new Vector3(0, 0, speed));

            timer += 1;
            if (timer >= 700)
            {
                timer = 0;
                speed += 3f;
                //Debug.Log(rb.velocity);
            }
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                rb.AddForce(Vector3.left * f, ForceMode.Impulse);
            }
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                rb.AddForce(Vector3.right * f, ForceMode.Impulse);
            }
            if (Input.GetKeyDown(KeyCode.Space) && this.transform.position.y <= 3)
            {
                rb.AddForce(Vector3.up * f, ForceMode.Impulse);
                if(!mute)
                {
                    jumpsrc.GetComponent<AudioSource>().Play();
                }
               
            }
            //if(this.transform.position.y > 2 || (this.transform.position.y == 2 && jumped))
            //{
            //    rollingsrc.GetComponent<AudioSource>().mute = true;

            //}
            if (this.transform.position.y > 2.1)
                jumped = true;

            if (this.transform.position.y ==2 && jumped && !mute)
            {
                jumped = false;
                ballLandsrc.Play(400);
            }
            //else
            //{
            //    if(!mute)
            //        rollingsrc.GetComponent<AudioSource>().mute = false;

            //}
            if (this.transform.position.y > 1.9 && this.transform.position.y < 2.1 && !mute)
            {
                rollingsrc.GetComponent<AudioSource>().mute = false;
            }
            else
            {
                rollingsrc.GetComponent<AudioSource>().mute = true;
            }
            GameObject[] lights = GameObject.FindGameObjectsWithTag("light");

            if (Input.GetKeyDown(KeyCode.Q))
            {
                this.GetComponent<Renderer>().material.color = Color.red;
                foreach(GameObject l in lights)
                {
                    l.GetComponent<Light>().color = Color.red;
                }
                if(!mute)
                {
                    colorchangesrc.Play();
                }
                

            }
            if (Input.GetKeyDown(KeyCode.W))
            {
                this.GetComponent<Renderer>().material.color = Color.blue;
                foreach (GameObject l in lights)
                {
                    l.GetComponent<Light>().color = Color.blue;
                }
                if (!mute)
                {
                    colorchangesrc.Play();
                }


            }
            if (Input.GetKeyDown(KeyCode.E))
            {
                this.GetComponent<Renderer>().material.color = Color.green;
                foreach (GameObject l in lights)
                {
                    l.GetComponent<Light>().color = Color.green;
                }
                if (!mute)
                {
                    colorchangesrc.Play();
                }
            }
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                pause = true;
                canvas.GetComponent<CanvasScript>().PauseLabel.SetActive(true);
                canvas.GetComponent<CanvasScript>().resumeButton.SetActive(true);
                canvas.GetComponent<CanvasScript>().quitButton.SetActive(true);
                canvas.GetComponent<CanvasScript>().RestartButton.SetActive(true);
                canvas.GetComponent<CanvasScript>().MenuButton.SetActive(true);
                canvas.GetComponent<CanvasScript>().score.SetActive(false);
                canvas.GetComponent<CanvasScript>().pscore.SetActive(false);
                if(PlayerPrefs.GetFloat("mute") == 0)
                {
                    src1.GetComponent<AudioSource>().mute = true;
                    src2.GetComponent<AudioSource>().mute = true;
                    src3.GetComponent<AudioSource>().mute = false;
                    rollingsrc.GetComponent<AudioSource>().mute = true;
                }

            }
        }
        else
        {
            rb.velocity = Vector3.zero;
            rb.isKinematic = true;
        }


    }
    void OnTriggerEnter(Collider other)
    {
        var rb = this.gameObject.GetComponent<Rigidbody>();

        if (other.gameObject.CompareTag("yellowcube"))
        {
            Destroy(other.gameObject);
            //other.gameObject.SetActive(false);
            score += 20;
            if (score > maxScore)
                maxScore = score;
            ScoreText.text = "Score: " + score.ToString();
            playthroughScore.text = "Playthrough Score: " + maxScore.ToString();

            if (PlayerPrefs.GetFloat("mute")==0)
                collect.GetComponent<AudioSource>().Play(50);

        }
        if (other.gameObject.CompareTag("purplecube"))
        {
            Destroy(other.gameObject);
            //other.gameObject.SetActive(false);
            purpleitem = true;
            TileManager.GetComponent<TileManagerScript>().turnGray();
            if (PlayerPrefs.GetFloat("mute") == 0)
            {
                src1.GetComponent<AudioSource>().mute = true;
                src2.GetComponent<AudioSource>().mute = false;
                collects.GetComponent<AudioSource>().Play();
            }

            StartCoroutine(purpleItemEffectDelay());

        }
    }

    IEnumerator purpleItemEffectDelay()
    {
        yield return new WaitForSecondsRealtime(purpleitemdelay);
        if(!pause) {
            TileManager.GetComponent<TileManagerScript>().returnColor();
            purpleitem = false;
            if (PlayerPrefs.GetFloat("mute") == 0)
            {
                src2.GetComponent<AudioSource>().mute = true;
                src1.GetComponent<AudioSource>().mute = false;
            }
        }
        else
        {
            purpleItemEffectDelay();
        }

    }

    void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.CompareTag("tile"))
        {

            Color tc = col.gameObject.GetComponent<Renderer>().material.color;
            if (this.GetComponent<Renderer>().material.color!= tc && tc != Color.gray)
            {
                score = (int)score /2;
                ScoreText.text = "Score: " + score.ToString();
                if (score == 0 && !pause)
                {
                    canvas.GetComponent<CanvasScript>().gameOver();

                }
                if (!mute)
                    wrongsrc.Play();


            }
            else
            {
                if (!mute && tc!= Color.gray && this.GetComponent<Renderer>().material.color == tc)
                    correctsrc.Play(400);
            }


        }

    }


}
