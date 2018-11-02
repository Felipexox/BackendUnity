using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameSparks.Core;
using GameSparks.Api.Requests;
using GameSparks.Api.Responses;
using System;
public class GameSparksUtility {

    public static void RegisterUser(string pDisplayName, string pLoginName, string pPassword, Action<RegistrationResponse> sucessEvent)
    {
        // register player by game spark
        new RegistrationRequest()
           .SetUserName(pDisplayName)
           .SetPassword(pPassword)
           .SetDisplayName(pDisplayName).Send((response) =>
           {
                sucessEvent(response);
           });
    }

    public static void LoginUser(string pLoginName, string pPassword, Action<AuthenticationResponse> sucessEvent)
    {
        //login player by Game Spark
        new AuthenticationRequest()
            .SetUserName(pLoginName)
            .SetPassword(pPassword).Send((response) =>
            {
                sucessEvent(response);
            });
    }

    public static void GetInfoPlayer(ref string pDisplayName, ref string teste, Action<AccountDetailsResponse> sucessEvent)
    {
        // Get Account info
        new AccountDetailsRequest().Send((response) =>
        {
            sucessEvent(response);
        });
    }

    #region Actions  

    public static Action<AuthenticationResponse> ActionLogin;

    public static Action<RegistrationResponse> ActionRegister;

    #endregion

}
