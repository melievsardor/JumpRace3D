using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameStates", menuName = "Dates/GameStates")]
public class GameStates : ScriptableObject
{
    private int level = 1;
    public int Level { get { return level; } set { level = value; } }

    [SerializeField]
    private int levelItemCount = 15;
    public int LevelItemCount { get { return levelItemCount; } }

    [SerializeField]
    private string key = "GameStates";

    private void OnEnable()
    {
        if (key == string.Empty)
        {
            key = name;
        }

        JsonUtility.FromJsonOverwrite(PlayerPrefs.GetString(key), this);
    }

    public void LevelCompleted()
    {
        level++;

        levelItemCount += 5;

        levelItemCount = Mathf.Clamp(levelItemCount, 15, 100);
    }

    private void OnDisable()
    {
        if (key == string.Empty)
        {
            key = name;
        }

        string jsonData = JsonUtility.ToJson(this, true);
        PlayerPrefs.SetString(key, jsonData);
        PlayerPrefs.Save();
    }





}
