using UnityEngine;
using System.Collections;
using System;

public class TileManagerScript : MonoBehaviour {

    public GameObject panelPrefab;
    public GameObject currentPanel;
    public GameObject rightWallPrefab;
    public GameObject leftWallPrefab;
    public GameObject Player;
    public GameObject Camera;
    public GameObject yellowCubePrefab;
    public GameObject purpleCubePrefab;

    static int spawnCounter;
    //ArrayList pcolors;
    static int rareitemcounter;
    static int itemcounter; 

    Color[] colors = { Color.red, Color.blue, Color.green, Color.gray };

    void Start () {
        currentPanel.transform.GetChild(0).GetComponent<Renderer>().material.color = Color.gray;
        spawnCounter = 0;
        rareitemcounter = 0;
        itemcounter = 0;
        
    }

    void Update () {
        bool pause = Player.GetComponent<PlayerScript>().pause;
        if (!pause)
        {


            SpawnTile();

            GameObject[] panels = GameObject.FindGameObjectsWithTag("tile");
            float cz = Camera.transform.position.z;
            for (int i = 0; i < panels.Length; i++)
            {
                if (panels[i].GetComponent<Renderer>().transform.position.z < cz - 5)
                {
                    //panels[i].SetActive(false);
                    Destroy(panels[i].transform.parent.gameObject);
                    //panels[i].GetComponent<Rigidbody>().isKinematic = false;

                }
            }
            GameObject[] walls = GameObject.FindGameObjectsWithTag("wall");

            for (int i = 0; i < walls.Length; i++)
            {
                if (walls[i].GetComponent<Renderer>().transform.position.z < cz - 5)
                {
                    //walls[i].SetActive(false);
                    Destroy(walls[i].transform.parent.gameObject);
                    //walls[i].GetComponent<Rigidbody>().isKinematic = false;
                }
            }
            GameObject[] yellowCubes = GameObject.FindGameObjectsWithTag("yellowcube");
            for (int i = 0; i < yellowCubes.Length; i++)
            {
                if (yellowCubes[i].GetComponent<Renderer>().transform.position.z < cz - 5)
                {
                    Destroy(yellowCubes[i]);
                }
            }
            GameObject[] purpleCubes = GameObject.FindGameObjectsWithTag("purplecube");
            for (int i = 0; i < purpleCubes.Length; i++)
            {
                if (purpleCubes[i].GetComponent<Renderer>().transform.position.z < cz - 5)
                {
                    Destroy(purpleCubes[i]);
                }
            }



        }



    }

    public void SpawnTile()
    {
        int index = UnityEngine.Random.Range(0, 4);
        currentPanel = (GameObject)Instantiate(panelPrefab, currentPanel.transform.GetChild(0).transform.GetChild(0).position, Quaternion.identity);

        Instantiate(rightWallPrefab, currentPanel.transform.GetChild(0).GetChild(2).position, Quaternion.identity);
        Instantiate(leftWallPrefab, currentPanel.transform.GetChild(0).GetChild(1).position, Quaternion.identity);
        itemcounter += 1;
        if (itemcounter == 5)
        {
            itemcounter = 0;
            int yitemindex = UnityEngine.Random.Range(1, 1000);
            Vector3 pos = currentPanel.transform.GetChild(0).position;
            if (yitemindex % 2 == 0)
                Instantiate(yellowCubePrefab, new Vector3(pos.x - 1.5f, pos.y + 1.5f, pos.z), Quaternion.identity);
            else if (yitemindex % 5 == 0)
            {
                Instantiate(yellowCubePrefab, new Vector3(pos.x, pos.y + 1.5f, pos.z), Quaternion.identity);

            }
            else
            {
                Instantiate(yellowCubePrefab, new Vector3(pos.x + 1.5f, pos.y + 1.5f, pos.z), Quaternion.identity);

            }
            rareitemcounter += 1;

        }
        int pitemindex = UnityEngine.Random.Range(1, 5000);
        if (rareitemcounter == 20)
        {
            rareitemcounter = 0;
            pitemindex = UnityEngine.Random.Range(1, 1000);
            Vector3 pos = currentPanel.transform.GetChild(0).position;
            if (pitemindex % 2 == 0)
                Instantiate(purpleCubePrefab, new Vector3(pos.x - 1.5f, pos.y + 1.5f, pos.z), Quaternion.identity);
            else if (pitemindex % 5 == 0)
            {
                Instantiate(purpleCubePrefab, new Vector3(pos.x, pos.y + 1.5f, pos.z), Quaternion.identity);

            }
            else
            {
                Instantiate(purpleCubePrefab, new Vector3(pos.x + 1.5f, pos.y + 1.5f, pos.z), Quaternion.identity);

            }

        }


        currentPanel.transform.GetChild(0).GetComponent<Renderer>().material.color = colors[index];
    }

    public void turnGray()
    {
        GameObject[] panels = GameObject.FindGameObjectsWithTag("tile");
        foreach (GameObject panel in panels)
        {
            panel.GetComponent<Renderer>().material.color = Color.gray;
        }

    }

    public void returnColor()
    {
        int index;
        GameObject[] panels = GameObject.FindGameObjectsWithTag("tile");
        foreach (GameObject panel in panels)
        {
            index = UnityEngine.Random.Range(0, 4);
            panel.GetComponent<Renderer>().material.color = colors[index];
        }

    }


}
