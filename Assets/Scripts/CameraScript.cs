using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour {

    // Use this for initialization
    public GameObject target;
    bool mute;
    public AudioSource src1;
	void Start () {
	    if(PlayerPrefs.GetFloat("mute") == 1)
        {
            src1.GetComponent<AudioSource>().mute = true;
            mute = true;
        }
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 pos = target.transform.position;
        pos.z -= 5;
        pos.y += 5;
        transform.position = pos;

    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.transform.parent == null)
            Destroy(col.gameObject);
        else
            Destroy(col.gameObject.transform.parent.gameObject);
    }

}
