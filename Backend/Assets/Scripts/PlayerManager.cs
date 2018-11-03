using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameSparks.Api.Responses;

public class PlayerManager : MonoBehaviour {
    private static PlayerManager instance;

    static string mKeyDisplayName = "PlayerDisplayName";

    static string mKeyScore = "PlayerScore";

    private void Awake()
    {
        Instance = this;
    }

    public void ClearCache()
    {
        PlayerPrefs.DeleteAll();
    }

    public void SavePlayerDisplayName(string pPlayerDisplayName)
    {
        PlayerPrefs.SetString(mKeyDisplayName, pPlayerDisplayName);
        PlayerPrefs.Save();
    }

    public void SavePlayerScore(int pScore)
    {

        //Server save player score
        GameSparksUtility.ActionPostScore = delegate (LogEventResponse pPostScoreResponse)
        {
            if (!pPostScoreResponse.HasErrors)
                ManagerUI.Instance.OnSucessPostScore(pPostScoreResponse);
            else
                ManagerUI.Instance.OnErrorPostScore(pPostScoreResponse);
        };

        GameSparksUtility.PostScoreEvent("GLB_SCORE", pScore, GameSparksUtility.ActionPostScore);

        CachePlayerScore(pScore);
    }

    public void CachePlayerScore(int pScore)
    {
        // cache player score
        PlayerPrefs.SetInt(mKeyScore, pScore);
        PlayerPrefs.Save();
    }

    public int PlayerScore
    {
        get
        {
            return PlayerPrefs.GetInt(mKeyScore);
        }
    }

    public string DisplayName
    {
        get
        {
           return PlayerPrefs.GetString(mKeyDisplayName);
        }
    }

    public static PlayerManager Instance
    {
        get
        {
            return instance;
        }

        set
        {
            instance = value;
        }
    }

}
