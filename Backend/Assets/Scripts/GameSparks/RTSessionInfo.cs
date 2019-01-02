using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class RTSessionInfo
{
    [SerializeField]
    private string hostURL;
    public string HostURL{ get { return this.hostURL; } set { this.hostURL = value; } }
    [SerializeField]
    private string acccessToken;
    public string GetAccessToken { get { return this.acccessToken; } set { this.acccessToken = value; } }
    [SerializeField]
    private int portID;
    public int PortID { get { return this.portID; } set { this.portID = value; } }
    [SerializeField]
    private string matchID;
    public string MatchID { get { return this.matchID; } set { this.matchID = value; } }
    [SerializeField]
    private List<RTPlayer> playerList = new List<RTPlayer>();
    public List<RTPlayer> GetPlayerList
    {
        get
        {
            return playerList;
        }
        set
        {
            playerList = value;
        }
    }

    /// <summary>
    /// Creates a new RTSession object which is held until a new RT session is created
    /// </summary>
    /// <param name="_message">Message.</param>
 //  public RTSessionInfo(string _message)
   // {
      /*  portID = (int)_message.Port;
        hostURL = _message.Host;
        acccessToken = _message.AccessToken;
        matchID = _message.MatchId;
        // we loop through each participant and get their peerId and display name //
        foreach (MatchFoundMessage._Participant p in _message.Participants)
        {
            playerList.Add(new RTPlayer(p.DisplayName, p.Id, (int)p.PeerId));
        }*/
//    }

    public RTSessionInfo(string hostURL, string getAccessToken, int portID, string matchID, List<RTPlayer> getPlayerList)
    {
        HostURL = hostURL;
        GetAccessToken = getAccessToken;
        PortID = portID;
        MatchID = matchID;
        GetPlayerList = getPlayerList;
    }

    [System.Serializable]
    public class RTPlayer
    {
        public RTPlayer(string _displayName, string _id, int _peerId)
        {
            this.displayName = _displayName;
            this.id = _id;
            this.peerId = _peerId;
        }

        public string displayName;
        public string id;
        public int peerId;
        public bool isOnline;
    }
}