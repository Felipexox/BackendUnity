using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameSparks.Core;
using GameSparks.Api.Requests;
using GameSparks.Api.Responses;
using System;
public class GameSparksUtility {

    public static void RegisterUser(string pDisplayName, string pLoginName, string pPassword, Action<RegistrationResponse> pCallBack)
    {
        // register player by game spark
        new RegistrationRequest()
           .SetUserName(pDisplayName)
           .SetPassword(pPassword)
           .SetDisplayName(pDisplayName).Send((response) =>
           {
                pCallBack(response);
           });
    }

    public static void LoginUser(string pLoginName, string pPassword, Action<AuthenticationResponse> pCallBack)
    {
        //login player by Game Spark
        new AuthenticationRequest()
            .SetUserName(pLoginName)
            .SetPassword(pPassword).Send((response) =>
            {
                pCallBack(response);
            });
    }

    public static void LogoutUser(Action<EndSessionResponse> pCallBack)
    {
        new EndSessionRequest()
            .Send((pLogoutResponse) => {
                pCallBack(pLogoutResponse);
            });
    }

    public static void PostScoreEvent(string pEventShortCode, int pScore, Action<LogEventResponse> pCallBack)
    {
        new LogEventRequest().SetEventKey(pEventShortCode).SetEventAttribute("SCORE", pScore).Send((pPostScore) =>
        {
            pCallBack(pPostScore);
        });
    }

    public static void GetLeaderBoard(string pLeaderBoardShortCode, Action<LeaderboardDataResponse> pCallBack)
    {
        new LeaderboardDataRequest().SetLeaderboardShortCode(pLeaderBoardShortCode).SetEntryCount(10).Send((pLeaderBoardDataResponse) =>
        {
            pCallBack(pLeaderBoardDataResponse);
        });
    }

    public static void GetInfoPlayer(ref string pDisplayName, ref string teste, Action<AccountDetailsResponse> pCallBack)
    {
        // Get Account info
        new AccountDetailsRequest().Send((response) =>
        {
            pCallBack(response);
        });
    }

    #region Actions  

    public static Action<AuthenticationResponse> ActionLogin;

    public static Action<RegistrationResponse> ActionRegister;

    public static Action<EndSessionResponse> ActionLogout;

    public static Action<LogEventResponse> ActionPostScore;

    public static Action<LeaderboardDataResponse> ActionLeaderBoard;

    #endregion

}
