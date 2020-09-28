using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;
//using UnityEngine.SceneManagement;    //we are not using this

public class Launcher : MonoBehaviourPunCallbacks
{
    [SerializeField] TMP_InputField roomNameInput;
    [SerializeField] TextMeshProUGUI errorMassage;

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
    }

    public override void OnJoinedLobby()
    {
        Debug.Log("joined to lobby");
        //SceneManager.LoadScene(1);     //i found a better and optimized way to make this happens    
        MenuManager.Instance.OpenMenu("title");
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
        MenuManager.Instance.OpenMenu("menu");
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        MenuManager.Instance.OpenMenu("error");
        errorMassage.text = "Room creation failed : " + message;
    }

}
