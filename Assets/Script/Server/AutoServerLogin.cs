using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameJamLibrary;
using Nettention.Proud;

public class AutoServerLogin : MonoBehaviour
{

    private void Awake()
    {

        //GPGSLib.OnLoginSuccess.AddListener(
        //    () =>
        //    {

        //        NetManager.ConnectServer();

        //    });

    }

    public void Login()
    {

        NetManager.ConnectServer();

        NetManager.m_proxy
            .RequestLogin(
            HostID.HostID_Server, RmiContext.ReliableSend, "TestID");

    }

}
