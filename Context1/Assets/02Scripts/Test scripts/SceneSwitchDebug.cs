using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using System;

public class SceneSwitchDebug : MonoBehaviour
{
    public static SceneSwitchDebug instance;

    private void Awake()
    {
        instance = this; 
    }

    // let niet op de namen van de variables, dit is letterlijk gwn voor testen

    public GameObject playGround;
    public GameObject bioHorror;

    public bool currentlyPlayground;
    private List<Enemy> enemies = new();
    private List<BalloonFlight> balloons = new();
    private List<Camera> cams = new();
    [SerializeField] private Color bgColorPlayground;
    [SerializeField] private Color bgColorBiohorror;

    [SerializeField] private Color fogColorPlayground;
    [SerializeField] private Color fogColorBiohorror;

    private void Start()
    {
        playGround.SetActive(true);
        bioHorror.SetActive(true);

        RenderSettings.fog = true;
        GameObject[] enemyObjects = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject[] balloonObjects = GameObject.FindGameObjectsWithTag("Balloon");

        GameObject[] cameraObjects = GameObject.FindGameObjectsWithTag("Camera");

        playGround.SetActive(true);
        bioHorror.SetActive(false);
        currentlyPlayground = true;

        foreach(GameObject cam in cameraObjects)
        {
            cams.Add(cam.GetComponent<Camera>());
        }

        // wat dit doet is de enemy/balloon lists sorten zodat ze gepaired zijn
        for(int i = 0; i < enemyObjects.Length; i++)
        {
            enemies.Add(enemyObjects[i].GetComponent<Enemy>());
            bool foundCorresponding = false;
            foreach(GameObject b in balloonObjects)
            {
                if (Vector3.Distance(enemyObjects[i].transform.position, b.transform.position) < 1f)
                {
                    foundCorresponding = true;
                    balloons.Add(b.GetComponent<BalloonFlight>());
                    break;
                }
            }
            if (!foundCorresponding)
            {
                Debug.LogWarning("Could not find a balloon at position " + enemyObjects[i].transform.position + ". \n Please make sure there is a balloon" +
                    " instance at the same location of each eye enemy. \n \n \n (smh)");
            }
        }
    }


    public void AddCam(Camera cam)
    {
        cams.Add(cam);
    }

    public void SwapMood()
    {
        switch (currentlyPlayground)    // Checks if its playground and swaps it with Biohorror or the other way around
        {
            case false: // Its not playground --> becomes playGround
                playGround.SetActive(true);
                bioHorror.SetActive(false);

                foreach(BalloonFlight b in balloons)
                {
                    b.SceneSwap();
                }

                foreach(Camera cam in cams)
                {
                    cam.backgroundColor = bgColorPlayground;
                }
                RenderSettings.fogColor = fogColorPlayground;
                RenderSettings.fogDensity = .005f;

                currentlyPlayground =true; 
                break;
            case true:  // Its playground --> Becomes biohorror
                playGround.SetActive(false);
                bioHorror.SetActive(true);

                foreach(Enemy e in enemies)
                {
                    try
                    {
                        e.SceneSwap();
                    }
                    catch { }
                }

                foreach (Camera cam in cams)
                {
                    cam.backgroundColor = bgColorBiohorror;
                }
                RenderSettings.fogColor = fogColorBiohorror;
                RenderSettings.fogDensity = 0.02f;

                currentlyPlayground =false; 
                break;
        }
        GameManager.instance.UpdatePathfinding();
        UserInterfaceManager.instance.UpdateUI();
        ButtonController.instance.WipeButtons();
    }

    public void RemoveEye(Enemy e)
    {
        enemies.Remove(e);
    }

    public void RemoveBalloon(BalloonFlight f)
    {
        balloons.Remove(f);
    }
}
