using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using GameSparks;
using GameSparks.RT;
using UnityEngine.UI;
public class ChatUI : MonoBehaviour, IScreen {
    public Text m_pText;
    public static ChatUI instance;

    public void Awake()
    {
        instance = this;
    }

    public void INIT()
    {

    }

    public void Start()
    {
        GameSparks.Api.Messages.MatchFoundMessage.Listener += OnMatch;

        GameSparks.Api.Messages.ScriptMessage.Listener += (message) =>
        {
            m_pText.text += message.Data.BaseData["INFO"]  + "\n\n";
            RTSessionInfo tSession = GameSparksManager.Instance.PSessionInfo = Newtonsoft.Json.JsonConvert.DeserializeObject<RTSessionInfo>(message.Data.BaseData["INFO"] as string);
            //GameSparksManager.Instance.StartNewRTSession(tSession);
            GameSparksManager.Instance.gameSparksRTUnity.Configure(tSession.HostURL, tSession.PortID, tSession.GetAccessToken, null, null, null, OnPacketReceived);
            GameSparksManager.Instance.gameSparksRTUnity.Connect();
        };

        GameSparks.Api.Messages.MatchNotFoundMessage.Listener = (message) =>
        {
            Debug.Log("Error to find match");
        };

      //  gameSparksRTUnity.Configure(response, )
        
    }
    public void SendMessage()
    {
        using (RTData data = new RTData())
        {
            data.SetString(1, "hello friend");
            GameSparksManager.Instance.gameSparksRTUnity.SendData(1, GameSparksRT.DeliveryIntent.RELIABLE, data); // send the RTData with op-cdoe '120' and to all players.
        }
    }
    public void OnPacketReceived(RTPacket _packet)
    {
      
        string text = _packet.Data.GetString(5); // get string at key 5    
        Debug.Log("\nPackage Received:"+_packet+"\n");
        m_pText.text += "\nPackage Received:" + _packet + "\n";

    }
    public void ConnectToMatch()
    {
           GameSparksUtility.CreateMatchByName(new string[] { "5c28b98fd2478c4bac62b31a", "5c291100330c316902ba9594"});
       // GameSparksUtility.ConnectToLobby();
    }
    public void GenerateRTSessionJSON()
    {
        List < RTSessionInfo.RTPlayer > players = new List<RTSessionInfo.RTPlayer>();
        players.Add(new RTSessionInfo.RTPlayer("DisplayName1", "id1", 100));
        players.Add(new RTSessionInfo.RTPlayer("DisplayName2", "id2", 200));
        RTSessionInfo rTSessionInfo = new RTSessionInfo("HostURLR", "AcessTokenR", 1000, "MatchIDR", players);

       Debug.Log(  Newtonsoft.Json.JsonConvert.SerializeObject(rTSessionInfo, Newtonsoft.Json.Formatting.Indented) );
    }
    public void OnMatch(GameSparks.Api.Messages.MatchFoundMessage pMessage)
    {
        Debug.Log("Match Found!...");
        System.Text.StringBuilder sBuilder = new System.Text.StringBuilder();
        sBuilder.AppendLine("Match Found...");
        sBuilder.AppendLine("Host URL:" + pMessage.Host);
        sBuilder.AppendLine("Port:" + pMessage.Port);
        sBuilder.AppendLine("Access Token:" + pMessage.AccessToken);
        sBuilder.AppendLine("MatchId:" + pMessage.MatchId);
        //sBuilder.AppendLine("Opponents:" + pMessage.Participants.Count());
        sBuilder.AppendLine("_________________");
        sBuilder.AppendLine(); // we'll leave a space between the player-list and the match data
        foreach (GameSparks.Api.Messages.MatchFoundMessage._Participant player in pMessage.Participants)
        {
            sBuilder.AppendLine("Player:" + player.PeerId + " User Name:" + player.DisplayName); // add the player number and the display name to the list
        }
        Debug.Log("INFO MATCH: ");
        Debug.Log(sBuilder.ToString()); // set the string to be the player-list field
    }

 
}
