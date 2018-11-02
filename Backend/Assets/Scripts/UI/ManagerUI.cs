using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameSparks.Api.Responses;
public class ManagerUI : MonoBehaviour {

    [SerializeField]
    private GameObject mLoginGameObjectUI;
    [SerializeField]
    private GameObject mRegisterGameObjectUI;


    private GameObject mCurrentUI;

    public enum Screen
    {
        LOGIN,
        REGISTER
    }

    private static ManagerUI instance;

    private void Awake()
    {
        Instance = this;
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
        }
    }

    public void EnableScreen(GameObject pScreenGameObject)
    {
        mCurrentUI.SetActive(false);
        mCurrentUI = pScreenGameObject;
        mCurrentUI.SetActive(true);
    }

    #region Call Backs
    //Login
    public void OnSucessLogin(AuthenticationResponse pAuthResponse)
    {
        Debug.Log("Logged");
    }
    public void OnErrorLogin(AuthenticationResponse pAuthResponse)
    {
        Debug.Log("Error");
    }

    //Register
    public void OnSucessRegister(RegistrationResponse pAuthResponse)
    {
        Debug.Log("Register sucessful");
    }
    public void OnErrorRegister(RegistrationResponse pAuthResponse)
    {
        Debug.Log("Error Register");
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
