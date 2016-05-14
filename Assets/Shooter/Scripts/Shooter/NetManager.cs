using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class NetManager : NetworkManager {
    [HideInInspector]
    public NetworkConnection conn;
    [HideInInspector]
    public short playerControllerId;
	public override void OnServerAddPlayer(NetworkConnection c, short pcid) {
        conn = c;
        playerControllerId = pcid;
        //Debug.Log();
        //Transform trans = GetStartPosition();
        //Vector3 pos = trans.position;
        //Quaternion rot = trans.rotation;
        //GameObject obj = GameObject.Instantiate(playerPrefab,pos,rot) as GameObject;
        
        //NetworkServer.AddPlayerForConnection( conn, obj, playerControllerId );
    }
     public override void OnClientConnect(NetworkConnection conn) {
         
     }
    public void RespawnPlayer() {
        Transform trans = GetStartPosition();
        Vector3 pos = trans.position;
        Quaternion rot = trans.rotation;
        GameObject obj = GameObject.Instantiate(playerPrefab,pos,rot) as GameObject;
        NetworkServer.AddPlayerForConnection( conn, obj, playerControllerId );
    }
}
