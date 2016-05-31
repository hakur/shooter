using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
namespace Shooter{
public class NetManager : NetworkManager {
    [HideInInspector]
    public NetworkConnection conn;
    [HideInInspector]
    public short playerControllerId;
    public override void OnServerAddPlayer(NetworkConnection c, short pcid) {
        conn = c;
        playerControllerId = pcid;
        Debug.Log(888);
        GlobalVars.isServer = true;
    }
	public override void OnServerAddPlayer(NetworkConnection c, short pcid,NetworkReader msg) {
        Debug.Log(msg);
        //Debug.Log();
        //Transform trans = GetStartPosition();
        //Vector3 pos = trans.position;
        //Quaternion rot = trans.rotation;
        //GameObject obj = GameObject.Instantiate(playerPrefab,pos,rot) as GameObject;
        
        //NetworkServer.AddPlayerForConnection( conn, obj, playerControllerId );
    }
     public override void OnClientConnect(NetworkConnection c) {
         conn = c;
         if(!GlobalVars.isServer) {
             
         }
     }
    public void RespawnPlayer(bool showPlayer) {
        Transform trans = GetStartPosition();
        Vector3 pos = trans.position;
        Quaternion rot = trans.rotation;
        GameObject obj = GameObject.Instantiate(playerPrefab,pos,rot) as GameObject;
        if(GlobalVars.isServer) {
            NetworkServer.AddPlayerForConnection( conn, obj, playerControllerId );
        } else {
            ClientScene.AddPlayer(conn,1);
        }
        Debug.Log(999);
        obj.gameObject.SetActive(showPlayer);
    }
}
}
