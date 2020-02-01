using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public float score = 0;
    //List<KeyValuePair<string, float>> scoreList = new List<KeyValuePair<string, float>>();
    Dictionary<string, float> scoreDict = new Dictionary<string, float>();

    // Start is called before the first frame update
    void Start()
    {
        score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void fillScore()
    {
        GameObject[] allObjects = GameObject.FindGameObjectsWithTag("Car");
        foreach (GameObject car in allObjects)
        {
            //AddScore(car.GetComponent<>);
        }
    }

    public void AddScore(string name, float value)
    {
        scoreDict.Add(name, value);
       
    }
}
