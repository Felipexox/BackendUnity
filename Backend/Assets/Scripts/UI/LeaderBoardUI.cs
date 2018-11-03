using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GameSparks.Api.Responses;
public class LeaderBoardUI : MonoBehaviour, IScreen
{
    [SerializeField]
    private GameObject mPrefabLeaderBoardItemUI;
    [SerializeField]
    private Transform mRoot;

    private List<GameObject> mLeaderBoardItens;

    public void INIT()
    {
        //IF your root has itens destroy IT // Temporary
        if(mLeaderBoardItens != null)
            for(int i = 0; i < mLeaderBoardItens.Count; i++)
                Destroy(mLeaderBoardItens[i]);

        GameSparksUtility.ActionLeaderBoard = delegate (LeaderboardDataResponse pLeaderBoardDataResponse)
        {
            if (!pLeaderBoardDataResponse.HasErrors)
                CreatePoolLeaderBoard(pLeaderBoardDataResponse);
            else
                Debug.Log("Erro to load leaderboard"); // to do
        };

        GameSparksUtility.GetLeaderBoard("GLB", GameSparksUtility.ActionLeaderBoard);
    }

    public void CreatePoolLeaderBoard(LeaderboardDataResponse pLeaderBoardDataResponse)
    {
        mLeaderBoardItens = new List<GameObject>();
   
        foreach(LeaderboardDataResponse._LeaderboardData pLData in pLeaderBoardDataResponse.Data)
        {
            GameObject pLeaderBoardItemObject = Instantiate(mPrefabLeaderBoardItemUI, mRoot);
            Debug.Log("Create LeaderBoarItens");
            int pLeaderBoardRank = (int) pLData.Rank ;

            string pPlayerDisplayName = pLData.UserName;

            string pScore = pLData.JSONData["SCORE"].ToString();

            SetLeaderBoardItemInfo(pLeaderBoardItemObject, pPlayerDisplayName, pScore, pLeaderBoardRank);

            mLeaderBoardItens.Add(pLeaderBoardItemObject);
        }
    }

    void SetLeaderBoardItemInfo(GameObject pLeaderBoardItemObject, string pPlayerDisplayName, string pScore, int pLeaderBoardRank)
    {
        Text pTextUI = pLeaderBoardItemObject.GetComponent<Text>();
        pTextUI.text = pLeaderBoardRank + " - " + pPlayerDisplayName + " - " + pScore;
    }
}
