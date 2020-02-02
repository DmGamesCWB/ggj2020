using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    [Header("Icon Settings")]
    public Sprite[] driverEmojis;
    public Sprite[] progressEmojis;

    [Header("Level Settings")]
    public float levelLengthInSec = 60.0f;
    public float minScoreToSucceed = 0.5f;
    
    [Header("Score - for debug purposes")]
    public float globalScore = 1.0f;
    public float elapsedTimeInLevel;
    public float progress;
    public int progressIndex;

    private Dictionary<int, float> scoreDict = new Dictionary<int, float>();
    private bool theEnd;
    private float theEndSplashScreenSec = 5.0f;

    // Start is called before the first frame update
    void Start()
    {
        elapsedTimeInLevel = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        elapsedTimeInLevel += Time.deltaTime;
        UpdateLevelProgress();

        if (theEnd)
        {
            return;
        }

        if(elapsedTimeInLevel > levelLengthInSec)
        {
            theEnd = true;
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
            GameObject scoreIcon = GameObject.FindGameObjectWithTag("Canvas").transform.GetChild(1).gameObject;
            scoreIcon.GetComponent<Image>().sprite = driverEmojis[GetEmojiIndex()];
        }
    }

    private int GetEmojiIndex()
    {
       return (driverEmojis.Length - (int)Mathf.Round(globalScore * driverEmojis.Length));
    }

    private void UpdateLevelProgress()
    {
        progress = elapsedTimeInLevel / levelLengthInSec;
        progress *= 4;
        progressIndex = Mathf.CeilToInt(progress)-1;
        GameObject progressObj = GameObject.FindGameObjectWithTag("Canvas").transform.GetChild(3).gameObject;
        progressObj.GetComponent<Image>().sprite = progressEmojis[progressIndex];
    }

    private void CheckLevelSuccess()
    {
        GameObject splashScreenObj = GameObject.FindGameObjectWithTag("Canvas").transform.GetChild(2).gameObject;
        splashScreenObj.SetActive(true);
        if (globalScore > minScoreToSucceed)
        {
            Debug.Log("Greetings to User and Load Next Level");
            StartCoroutine(LoadNextLevel());
        }
        else
        {
            //Thumbs Down
            splashScreenObj.transform.Rotate(new Vector3(0, 0, 1), 180f);
            Debug.Log("Better Luck Next Time and Reload Level");
            StartCoroutine(ReloadLevel());
        }
    }

    IEnumerator LoadNextLevel()
    {
        int nextLevelIndex = SceneManager.GetActiveScene().buildIndex + 1;
        // Remember: Zero based count
        if (nextLevelIndex < SceneManager.sceneCountInBuildSettings)
        {
            AudioManager.instance.PlayFxSound(Sound.SoundTypes.Applause);
            yield return new WaitForSeconds(theEndSplashScreenSec);
            AudioManager.instance.StopAllFxSound();
            SceneManager.LoadScene(nextLevelIndex);
        }
        else
        {
            // Should not get in here because the Credits Level is the last in Scene Managers
            yield return new WaitForSeconds(theEndSplashScreenSec);
            AudioManager.instance.StopAllFxSound();
            SceneManager.LoadScene("MainMenu");
        }
    }

    IEnumerator ReloadLevel()
    {
        AudioManager.instance.PlayFxSound(Sound.SoundTypes.Boo);
        yield return new WaitForSeconds(theEndSplashScreenSec);
        AudioManager.instance.StopAllFxSound();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
