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

    public void INIT()
    {
        mLogoutButton.onClick = new Button.ButtonClickedEvent();

        mLogoutButton.onClick.AddListener(Logout);
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
        //to do
    }
}
