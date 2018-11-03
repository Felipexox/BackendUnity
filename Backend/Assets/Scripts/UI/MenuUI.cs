using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GameSparks.Api.Responses;

public class MenuUI : MonoBehaviour, IScreen {
    [SerializeField]
    private Text mDisplayPlayerInfo;
    [SerializeField]
    private Button mStartButton;
    [SerializeField]
    private Button mLogoutButton;
    [SerializeField]
    private LeaderBoardUI mLeaderBoardUI;

    public void INIT()
    {
        mLeaderBoardUI.INIT();

        //Info
        mDisplayPlayerInfo.text = "Bem vindo, " + PlayerManager.Instance.DisplayName + ".";

        //Buttons
        mLogoutButton.onClick = new Button.ButtonClickedEvent();
        mStartButton.onClick = new Button.ButtonClickedEvent();

        mLogoutButton.onClick.AddListener(Logout);
        mStartButton.onClick.AddListener(StartGamePlay);
    }

    public void Logout()
    {
        GameSparksUtility.ActionLogout = delegate (EndSessionResponse pLogoutResponse) {
            if (!pLogoutResponse.HasErrors)
                ManagerUI.Instance.OnSucessLogout(pLogoutResponse);
            else
                ManagerUI.Instance.OnErrorLogout(pLogoutResponse);
        };

        GameSparksUtility.LogoutUser(GameSparksUtility.ActionLogout);
    }

    public void StartGamePlay()
    {
        ManagerUI.Instance.SetScreen(ManagerUI.Screen.GAMEPLAY);
    }
}
