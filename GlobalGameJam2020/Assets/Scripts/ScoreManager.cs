using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public Dictionary<int, float> scoreDict = new Dictionary<int, float>();

    public Sprite[] driverEmojis;

    public float globalScore = 0.0f;

    public GameObject scoreIcon;

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
            //Debug.Log("ADD Score:" + car.GetInstanceID());
            AddScore(car.GetInstanceID(), car.GetComponent<Car>().score);
        }
        CalcAverageScore();
    }

    public void AddScore(int id, float value)
    {
        if (scoreDict.ContainsKey(id))
        {
            //Debug.Log("Contains Key. Update:" + id + "-->" + value);
            scoreDict[id] = value;
        }
        else
        {
            //Debug.Log("NOT Contains Key. ADD:" + id + "-->" + value);
            scoreDict.Add(id, value);
        }
    }

    private void CalcAverageScore()
    {
        globalScore = 0;
        foreach (float carScore in scoreDict.Values)
        {
            globalScore += carScore;
        }
        int count = scoreDict.Count;
        if (count > 0) { 
            globalScore = globalScore / count;
            //Debug.Log("Average:" + globalScore + "Size:" + scoreDict.Count);
            //Debug.Log("Index:" + getEmojiIndex());
            scoreIcon.GetComponent<Image>().sprite = driverEmojis[getEmojiIndex()];
        }
    }

    private int getEmojiIndex()
    {
       return (driverEmojis.Length - (int)Mathf.Round(globalScore * driverEmojis.Length));
    }
}
