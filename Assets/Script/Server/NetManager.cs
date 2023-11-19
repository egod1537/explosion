using System.Text;
using Nettention.Proud;
using GameJamLibrary;
using UnityEngine;

public class NetManager : MonoBehaviour
{

    public enum NetState
    {

        Disconnect,
        Connecting,
        Connect

    }

    private static NetManager _instance;
    public static NetManager instance
    {

        get
        {

            if (_instance == null)
            {

                GameObject go = new GameObject();
                go.name = "NetManager";
                _instance = go.AddComponent<NetManager>();

            }

            return _instance;

        }

    }

    public static System.Guid Version 
        = new System.Guid ("{ 0x6ba86ed0, 0xbf12, 0x4f11, { 0xa6, 0xbe, 0x7f, 0xd1, 0xb0, 0xf6, 0x23, 0xe6 } }");

    public const string ServerIp = "116.120.154.214";
    public const ushort ServerPort = 33589;

    public static NetClient m_netClient;
    public static NetState m_state;

    public static Main.Proxy m_proxy = new Main.Proxy();
    public static Main.Stub m_stub = new Main.Stub();

    bool m_disconnectNow;

    private void Awake()
    {

        DontDestroyOnLoad(gameObject);

    }

    private void Start()
    {

        m_netClient = new NetClient();

        ConnectServer();

        m_netClient.JoinServerCompleteHandler =
            (ErrorInfo info, ByteArray _byteArray) =>
            {

                if (info.errorType == ErrorType.Ok)
                    m_state = NetState.Connecting;
                else
                    m_state = NetState.Disconnect;

            };

        m_netClient.LeaveServerHandler =
            (ErrorInfo info) =>
            {
                m_state = NetState.Disconnect;
            };

        m_netClient.ReceivedUserMessageHandler =
            (HostID sender, RmiContext rmiContext, ByteArray payload) =>
            {

                Debug.Log(Encoding.Default.GetString(payload.data));

            };

        m_stub.NotifyLoginSuccess = 
            (HostID remote, RmiContext rmiContext) => 
            {

                m_state = NetState.Connect;

                return true;

            };

        m_stub.NotifyLoginFailed =
            (HostID remoet, RmiContext rmiContext, string text) => 
            {

                m_state = NetState.Disconnect;
                Debug.Log(text);

                return true;

            };

    }

    private void Update()
    {

        if (m_netClient != null) m_netClient.FrameMove();

        if (m_disconnectNow)
        {

            m_netClient.Disconnect();
            m_state = NetState.Disconnect;
            m_disconnectNow = false;

        }

    }

    private void OnApplicationQuit()
    {

        m_netClient.Disconnect();

    }

    public static void ConnectServer()
    {

        if (m_state != NetState.Disconnect) return;

        NetConnectionParam connectParam = new NetConnectionParam();

        connectParam.protocolVersion = new Guid();
        connectParam.protocolVersion.Set(Version);

        connectParam.serverIP = ServerIp;
        connectParam.serverPort = ServerPort;

        m_netClient.AttachProxy(m_proxy);
        m_netClient.AttachStub(m_stub);

        m_netClient.Connect(connectParam);
        m_state = NetState.Connecting;

    }

    public static void SendServerMessage(string _msg)
    {

        if (m_state != NetState.Connect) return;

        ByteArray bA = new ByteArray();

        bA.AddRange(Encoding.UTF8.GetBytes(_msg));

        m_netClient.SendUserMessage
        (HostID.HostID_Server, RmiContext.ReliableSend, bA);

    }

}
