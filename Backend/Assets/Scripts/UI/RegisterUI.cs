using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GameSparks.Api.Responses;
public class RegisterUI : MonoBehaviour {
    [SerializeField]
    private InputField mDisplayNameField;
    [SerializeField]
    private InputField mLoginField;
    [SerializeField]
    private InputField mPasswordField;
    [SerializeField]
    private Button mRegisterButton;
    [SerializeField]
    private Button mLoginLink;

    private void Awake()
    {
        INIT();
    }

    void INIT()
    {
        mRegisterButton.onClick = new Button.ButtonClickedEvent();
        mLoginLink.onClick = new Button.ButtonClickedEvent();

        mRegisterButton.onClick.AddListener( Register );
        mLoginLink.onClick.AddListener( SetLoginScreen );

    }

    public void Register()
    {
        string pDisplayName = mDisplayNameField.text;
        string pLogin = mLoginField.text;
        string pPassword = mPasswordField.text;

        GameSparksUtility.ActionRegister = delegate( RegistrationResponse registerResponse) {
            if (!registerResponse.HasErrors)
                ManagerUI.Instance.OnSucessRegister(registerResponse);
            else
                ManagerUI.Instance.OnErrorRegister(registerResponse);
        };

        GameSparksUtility.RegisterUser(pDisplayName, pLogin, pPassword, GameSparksUtility.ActionRegister);
    }

    public void SetLoginScreen()
    {
        ManagerUI.Instance.SetScreen(ManagerUI.Screen.LOGIN);
    }

}
