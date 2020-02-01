using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public Dictionary<int, float> scoreDict = new Dictionary<int, float>();

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        fillScore();
    }

    private void fillScore()
    {
        GameObject[] allObjects = GameObject.FindGameObjectsWithTag("ScoreCar");
        foreach (GameObject car in allObjects)
        {
            Debug.Log("ADD Score:" + car.GetInstanceID());
            AddScore(car.GetInstanceID(), car.GetComponent<Car>().score);
        }

    }

    public void AddScore(int id, float value)
    {
        if (scoreDict.ContainsKey(id))
        {
            Debug.Log("Contains Key. Update:" + id + "-->" + value);
            scoreDict[id] = value;
        }
        else
        {
            Debug.Log("NOT Contains Key. ADD:" + id + "-->"+ value);
            scoreDict.Add(id, value);
        }
    }
}
