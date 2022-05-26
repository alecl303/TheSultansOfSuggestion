using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemySpawner : MonoBehaviour
{
    public float xMin;
    public float xMax;
    public float yMin;
    public float yMax;

    public float enemyCount;

    public float liveEnemies;

    public GameObject[] enemyPrefabs;

    private List<GameObject> enemies;
    
    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < this.enemyCount; i++)
        {
            var randomX = Random.Range(this.xMin, this.xMax);
            var randomY = Random.Range(this.yMin, this.yMax);

            while(CheckOutOfBounds(randomX, randomY))
            {
                randomX = Random.Range(this.xMin, this.xMax);
                randomY = Random.Range(this.yMin, this.yMax);
            }

            var randomIndex = Random.Range(0, this.enemyPrefabs.Length);
            


            GameObject newEnemy = Instantiate(this.enemyPrefabs[randomIndex], new Vector3(randomX, randomY, this.gameObject.transform.position.z), new Quaternion());
            //this.enemies.Add(newEnemy);
        }
    }

    // Update is called once per frame
    void LateUpdate()
    {
        //Debug.Log(this.liveEnemies);
        if(this.liveEnemies <= 0)
        {
            Debug.Log("All done");
            FindObjectOfType<PlayerController>().GetStats().mana = 100;
            FindObjectOfType<DontDestroyOnLoad>().IncrementScene();
        }
    }

    private bool CheckOutOfBounds(float x, float y)
    {
        var currentScene = SceneManager.GetActiveScene().buildIndex;

        //if (currentScene == 2)
        //{
        //    float xMinOffLimit = 4.5f;
        //    float xMaxOffLimit = 8.5f;

        //    float yMinOffLimit = 2;
        //    float yMaxOffLimit = 8;

        //    if ((x > xMinOffLimit && x < xMaxOffLimit) && (y > yMinOffLimit && y < yMaxOffLimit))
        //    {
        //        return true;
        //    }
        //}

        if (currentScene == 3)
        {
            float xMinOffLimit = -9;
            float xMaxOffLimit = 7;

            float yMinOffLimit = -6;
            float yMaxOffLimit = 8;

            if((x > xMinOffLimit && x < xMaxOffLimit) && (y > yMinOffLimit && y < yMaxOffLimit))
            {
                return true;
            }
        }

        //if (currentScene == 4)
        //{
        //    float xMinOffLimit = -17;
        //    float xMaxOffLimit = 15;

        //    float yMinOffLimit = -20;
        //    float yMaxOffLimit = 9;

        //    if ((x > xMinOffLimit && x < xMaxOffLimit) && (y > yMinOffLimit && y < yMaxOffLimit))
        //    {
        //        return true;
        //    }

        //    yMinOffLimit = -17;

        //    var xMax = 9;
        //    var xMin = -10;

        //    if ((y < yMinOffLimit) && (x > xMax || x < xMin))
        //    {
        //        return true;
        //    }

        //    yMaxOffLimit = 4;

        //    xMax = 9;
        //    xMin = -10;

        //    if ((y > yMaxOffLimit) && (x > xMax || x < xMin))
        //    {
        //        return true;
        //    }
        //}

        return false;
    }
}
