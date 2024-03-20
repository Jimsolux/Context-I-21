using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class SceneSwitchDebug : MonoBehaviour
{
    // let niet op de namen van de variables, dit is letterlijk gwn voor testen

    public GameObject playGround;
    public GameObject bioHorror;

    bool currentlyPlayground;
    private List<Enemy> enemies = new();
    private List<BalloonFlight> balloons = new();
    private void Start()
    {
        GameObject[] enemyObjects = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject[] balloonObjects = GameObject.FindGameObjectsWithTag("Balloon");

        playGround.SetActive(true);
        bioHorror.SetActive(false);
        currentlyPlayground = true;

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

                currentlyPlayground =true; 
                break;
            case true:  // Its playground --> Becomes biohorror
                playGround.SetActive(false);
                bioHorror.SetActive(true);

                foreach(Enemy e in enemies)
                {
                    e.SceneSwap();
                }

                currentlyPlayground =false; 
                break;
        }
        GameManager.instance.UpdatePathfinding();
    }
}
