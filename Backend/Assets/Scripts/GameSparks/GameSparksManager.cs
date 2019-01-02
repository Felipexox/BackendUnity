using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameSparks;
using GameSparks.RT;
using GameSparks.Api.Requests;
using GameSparks.Core;
using GameSparks.Api.Responses;
public class GameSparksManager : MonoBehaviour {
    private static GameSparksManager instance;

    public GameSparksRTUnity gameSparksRTUnity;

    [SerializeField]
    private RTSessionInfo m_pSessionInfo;


    public void Awake()
    {
        instance = this;
    }



    public static GameSparksManager Instance
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

    public RTSessionInfo PSessionInfo
    {
        get
        {
            return m_pSessionInfo;
        }

        set
        {
            m_pSessionInfo = value;
        }
    }

    public void StartNewRTSession(RTSessionInfo _info)
    {
        Debug.Log("GSM| Creating New RT Session Instance...");
        m_pSessionInfo = _info;
        // In order to create a new RT game we need a 'FindMatchResponse' //
        // This would usually come from the server directly after a successful MatchmakingRequest //
        // However, in our case, we want the game to be created only when the first player decides using a button //
        // therefore, the details from the response is passed in from the gameInfo and a mock-up of a FindMatchResponse //
        // is passed in. //
        GSRequestData mockedResponse = new GSRequestData()
                                            .AddNumber("port", (double)_info.PortID)
                                            .AddString("host", _info.HostURL)
                                            .AddString("accessToken", _info.GetAccessToken); // construct a dataset from the game-details

        FindMatchResponse response = new FindMatchResponse(mockedResponse); // create a match-response from that data and pass it into the game-config
        // So in the game-config method we pass in the response which gives the instance its connection settings //
        // In this example, I use a lambda expression to pass in actions for 
        // OnPlayerConnect, OnPlayerDisconnect, OnReady and OnPacket actions //
        // These methods are self-explanatory, but the important one is the OnPacket Method //
        // this gets called when a packet is received //

        gameSparksRTUnity.Configure(response,
            (peerId) => { OnPlayerConnectedToGame(peerId); },
            (peerId) => { OnPlayerDisconnected(peerId); },
            (ready) => { OnRTReady(ready); },
            (packet) => { OnPacketReceived(packet); });
        gameSparksRTUnity.Connect(); // when the config is set, connect the game

    }

    private void OnPlayerConnectedToGame(int _peerId)
    {
        Debug.Log("GSM| Player Connected, " + _peerId);
    }

    private void OnPlayerDisconnected(int _peerId)
    {
        Debug.Log("GSM| Player Disconnected, " + _peerId);
    }

    private void OnRTReady(bool _isReady)
    {
        if (_isReady)
        {
            Debug.Log("GSM| RT Session Connected...");
        }

    }

    private void OnPacketReceived(RTPacket _packet)
    {
        Debug.Log("Package: " + _packet);
        ChatUI.instance.m_pText.text += _packet;
    }

}
