using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreKeeper : MonoBehaviour
{
    private int score = 0;

    static ScoreKeeper instance;

      void Awake()
    {
        ManageSingleton();
    }

    private void ManageSingleton()
    {
        if(instance != null)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public int GetScore()
    {
        return score;
    }

    public void ModifyScore(int scoreModification)
    {
        score += scoreModification;
        Mathf.Clamp(score, 0, int.MaxValue);
    }
    
    public void ResetScore()
    {
        score = 0;
    }
}
