using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using GameSparks.Api.Responses;
public class LoginUI : MonoBehaviour {
    [SerializeField]
    private InputField mPassawordField;
    [SerializeField]
    private InputField mLoginField;
    [SerializeField]
    private Button mLoginButton;
    [SerializeField]
    private Button mRegisterLink;

    private void Awake()
    {
        INIT();
    }

    void INIT()
    {
        mLoginButton.onClick = new Button.ButtonClickedEvent();
        mRegisterLink.onClick = new Button.ButtonClickedEvent();

        mLoginButton.onClick.AddListener( Login );
        mRegisterLink.onClick.AddListener( RegisterScreen );
    }

    public void Login()
    {
        string pPassword = mPassawordField.text;
        string pLogin = mLoginField.text;

        GameSparksUtility.ActionLogin = delegate (AuthenticationResponse pAuthResponse)
        {
            if (!pAuthResponse.HasErrors)
                ManagerUI.Instance.OnSucessLogin(pAuthResponse);
            else
                ManagerUI.Instance.OnErrorLogin(pAuthResponse);
        };

        GameSparksUtility.LoginUser(pLogin, pPassword, GameSparksUtility.ActionLogin);
    }

    public void RegisterScreen()
    {
        ManagerUI.Instance.SetScreen(ManagerUI.Screen.REGISTER);
    }

}
