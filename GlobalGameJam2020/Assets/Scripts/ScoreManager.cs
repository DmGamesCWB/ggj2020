using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    [Header("Icon Settings")]
    public Sprite[] driverEmojis;
    public GameObject scoreIcon;

    [Header("Level Settings")]
    public float levelLengthInSec = 60.0f;
    public float minScoreToSucceed = 0.5f;
    
    [Header("Score - for debug purposes")]
    public float globalScore = 1.0f;

    private Dictionary<int, float> scoreDict = new Dictionary<int, float>();
    private float elapsedTimeInLevel;

    // Start is called before the first frame update
    void Start()
    {
        elapsedTimeInLevel = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        elapsedTimeInLevel += Time.deltaTime;
        if(elapsedTimeInLevel > levelLengthInSec)
        {
            CheckLevelSuccess();
        }

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
        float totalSumScore = 0;
        foreach (float carScore in scoreDict.Values)
        {
            totalSumScore += carScore;
        }
        int cars = scoreDict.Count;
        if (cars > 0) { 
            globalScore = totalSumScore / cars;
            //Debug.Log("Average:" + globalScore + "Size:" + scoreDict.Count);
            //Debug.Log("Index:" + getEmojiIndex());
            scoreIcon.GetComponent<Image>().sprite = driverEmojis[GetEmojiIndex()];
        }
    }

    private int GetEmojiIndex()
    {
       return (driverEmojis.Length - (int)Mathf.Round(globalScore * driverEmojis.Length));
    }

    private void CheckLevelSuccess()
    {
        if(globalScore > minScoreToSucceed)
        {
            Debug.Log("Greetings to User and Load Next Level");
        }
        else
        {
            Debug.Log("Better Luck Next Time and Reload Level");
        }
    }
}
