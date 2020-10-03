using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;
using Photon.Realtime;
//using UnityEngine.SceneManagement;    //we are not using this

public class Launcher : MonoBehaviourPunCallbacks
{
    public static Launcher instanceLauncher;

    [SerializeField] TMP_InputField roomNameInput;
    [SerializeField] TextMeshProUGUI errorMassage;
    [SerializeField] TextMeshProUGUI roomNameDisplay;
    [SerializeField] Transform roomListContent;
    [SerializeField] GameObject roomListItemPrefab;
    [SerializeField] GameObject playerListItemPrefab;
    [SerializeField] Transform playerListContent;
    [SerializeField] TMP_InputField playerNickname;
    [SerializeField] GameObject startGamebutton;

    private void Awake()
    {
        instanceLauncher = this;
    }

    // Start is called before the first frame update
    void Start()
    {
       //menumanager = FindObjectOfType<MenuManager>();
        PhotonNetwork.ConnectUsingSettings();       //this allow the code to connect using settings in the PUP setting
        Debug.Log("connecting");
    }

    public override void OnConnectedToMaster()      //excuted when connected to server
    {
        PhotonNetwork.JoinLobby();                  //lobby is something from where you join the room
        Debug.Log("joining");

        PhotonNetwork.AutomaticallySyncScene = true;
    }

    public override void OnJoinedLobby()
    {
        Debug.Log("joined to lobby");
        //SceneManager.LoadScene(1);     //i found a better and optimized way to make this happens    
        MenuManager.Instance.OpenMenu("title");
        PhotonNetwork.NickName = "Player - " + Random.Range(1, 1000).ToString();
     }
    public void CreateRoom()
    {
        if(string.IsNullOrEmpty(roomNameInput.text))
        {
            return;
        }
        PhotonNetwork.CreateRoom(roomNameInput.text);
        MenuManager.Instance.OpenMenu("loading");
        
    }

    public override void OnJoinedRoom()
    {
        MenuManager.Instance.OpenMenu("room");
        roomNameDisplay.text = "Your Room Name - " + PhotonNetwork.CurrentRoom.Name;


        Player[] players = PhotonNetwork.PlayerList;
        for (int i = 0; i < players.Length; i++)
        {
            Instantiate(playerListItemPrefab, playerListContent).GetComponent<PlayerListItem>().Setup(players[i]);
        }

        startGamebutton.SetActive(PhotonNetwork.IsMasterClient);
    }

    public override void OnMasterClientSwitched(Player newMasterClient)
    {
        startGamebutton.SetActive(PhotonNetwork.IsMasterClient);
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        MenuManager.Instance.OpenMenu("error");
        errorMassage.text = "Room creation failed : " + message;
    }

    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
        MenuManager.Instance.OpenMenu("loading");
    }

    public override void OnLeftRoom()
    {
        MenuManager.Instance.OpenMenu("title");
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        foreach(Transform transform_ in roomListContent)   
        {
            Destroy(transform_.gameObject);             //so that we can destroy the list once used
        }

        for( int i = 0; i<roomList.Count; i++)
        {
            Instantiate(roomListItemPrefab, roomListContent).GetComponent<RoomListItem>().Setup(roomList[i]);
            //Here we are cloning a copy of RoomlistButton prefab and then put it in the roomListContent position
            //Then calling the Setup function to display the button name in the button
        }
    }

    public void JoinRoom(RoomInfo info_1)
    {
        PhotonNetwork.JoinRoom(info_1.Name);
        MenuManager.Instance.OpenMenu("loading");

    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Instantiate(playerListItemPrefab, playerListContent).GetComponent<PlayerListItem>().Setup(newPlayer);
    }

    public void StartGame()
    {
        PhotonNetwork.LoadLevel(1);
    }

}
