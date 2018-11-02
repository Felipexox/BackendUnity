using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameSparks.Api.Responses;
public class ManagerUI : MonoBehaviour {

    [SerializeField]
    private GameObject mLoginGameObjectUI;
    [SerializeField]
    private GameObject mRegisterGameObjectUI;
    [SerializeField]
    private GameObject mMenuGameObjectUI;

    private GameObject mCurrentUI;

    public enum Screen
    {
        LOGIN,
        REGISTER,
        MENU
    }

    private static ManagerUI instance;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        SetScreen(Screen.LOGIN);
    }

    public void SetScreen(Screen screen)
    {
        switch (screen) {
            case Screen.LOGIN:
                EnableScreen(mLoginGameObjectUI);
                break;
            case Screen.REGISTER:
                EnableScreen(mRegisterGameObjectUI);
                break;
            case Screen.MENU:
                EnableScreen(mMenuGameObjectUI);
                break;
        }
    }

    public void EnableScreen(GameObject pScreenGameObject)
    {
        if(mCurrentUI != null)
            mCurrentUI.SetActive(false);
        mCurrentUI = pScreenGameObject;
        mCurrentUI.SetActive(true);
    }

    #region Call Backs
    //Login Sucessful
    public void OnSucessLogin(AuthenticationResponse pAuthResponse)
    {
        //Loading Menu Screen
        SetScreen(Screen.MENU);
    }
    //Login Error
    public void OnErrorLogin(AuthenticationResponse pAuthResponse)
    {
        Debug.Log("Error");
    }

    //Register Sucessful
    public void OnSucessRegister(RegistrationResponse pAuthResponse)
    {
        Debug.Log("Register sucessful");
    }
    //Register Error
    public void OnErrorRegister(RegistrationResponse pAuthResponse)
    {
        Debug.Log("Error Register");
    }

    //Logout Sucessful
    public void OnSucessLogout(EndSessionResponse pLogoutResponse)
    {
        //Loading Login Screen
        SetScreen(Screen.LOGIN);
    }
    //Logout Error
    public void OnErrorLogout(EndSessionResponse pLogoutResponse)
    {
        Debug.Log("Error Logout");
    }
    #endregion

    #region Encapsuled Attributes
    public static ManagerUI Instance
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
    #endregion

}
