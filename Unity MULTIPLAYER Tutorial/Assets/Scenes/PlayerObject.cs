using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerObject : NetworkBehaviour {

	void Start () {
        // Is this actually my own local PlayerObject?
        if( isLocalPlayer == false)
        {
            // This object belongs to another pplayer.
            return;
        }

        // Sine the PlayerObjcet  is invisible and  not part of the world,
        // give me something physical to move around!

        Debug.Log("PlayerObject::Start  -- Spawning my own personal unit.");

        // Instantiate() only creates an object on the LOCAL COMPUTER.
        // Even if it has a NEtworkIdentity is still willl NOT exists on
        // the network (and therefore not on any other client) UNLESS
        // NetworkServer.Spawn() is called on this object.

        //Instantiate(PlayerUnitPrefab);

        // Command the server to SPAWN our unit

        CmdSpawnMyUnit();
	}

    public GameObject PlayerUnitPrefab;

    // Update is called once per frame
    void Update()
    {
    } 
    /////////////////////////  COMMANDS
    // Commands are special functions that only get executed on the server.

    [Command]
    void CmdSpawnMyUnit()
    {
    GameObject go = Instantiate(PlayerUnitPrefab);

    // Now that the object exists on the server, propagate it to all
    // the clients (and aslo wire up the NetworkIdentity)
    NetworkServer.Spawn(go);
    }
}
