using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class Client : MonoBehaviour
{
    public Telepathy.Client client = new Telepathy.Client(1920 * 1080 + 1024);


    private void Awake()
    {
        DontDestroyOnLoad(gameObject);

        // update even if window isn't focused, otherwise we don't receive.
        Application.runInBackground = true;

        // use Debug.Log functions for Telepathy so we can see it in the console
        Telepathy.Log.Info = Debug.Log;
        Telepathy.Log.Warning = Debug.LogWarning;
        Telepathy.Log.Error = Debug.LogError;

        // hook up events
        client.OnConnected = () => Debug.Log("Client Connected");
        client.OnData = (message) => ReceiveMessage(message);
        client.OnDisconnected = () => Debug.Log("Client Disconnected");
    }
    private void Update()
    {
        if (client.Connected)
        {
            // tick to process messages
            // (even if not connected so we still process disconnect messages)
            client.Tick(10);
        }
        else
        {
            client.Connect("127.0.0.1", 45604);
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            RequestPassword();
        }
    }

    private void OnApplicationQuit()
    {
        // the client/server threads won't receive the OnQuit info if we are
        // running them in the Editor. they would only quit when we press Play
        // again later. this is fine, but let's shut them down here for consistency
        client.Disconnect();
    }
    public void ReceiveMessage(ArraySegment<byte> message)
    {
        // clear previous message
        byte[] messageBytes = new byte[message.Count];
        for (int i = 0; i < messageBytes.Length; i++)
        {
            messageBytes[i] = message.Array[i];
        }

        byte[] commandBytes = new byte[4];
        Array.Copy(messageBytes, 0, commandBytes, 0, 4);
        int command = BitConverter.ToInt32(commandBytes);

        switch (command)
        {
            case ConstantValues.CMD_RESPONSE_PASSWORD:
                ReceivePassword(message: ref messageBytes);
                break;
            default:
                break;
        }
    }

    public void RequestPassword()
    {
        byte[] message = BitConverter.GetBytes(ConstantValues.CMD_REQUEST_PASSWORD);
        client.Send(message);

        Debug.Log("RequestPassword");
    }
    public void ReceivePassword(ref byte[] message)
    {
        byte[] passwordBytes = new byte[4];
        Array.Copy(message, 4, passwordBytes, 0, 4);

        StaticValues.password = BitConverter.ToInt32(passwordBytes);

        Debug.Log("ReceivePassword::" + StaticValues.password);

        SendStudioData();
    }
    public void SendStudioData()
    {
        List<byte> messages = new List<byte>();
        messages.AddRange(BitConverter.GetBytes(ConstantValues.CMD_SEND_STUDIO_DATA));
        messages.AddRange(BitConverter.GetBytes(StaticValues.password));
        messages.AddRange(BitConverter.GetBytes(StaticValues.textureBytes.Length));
        messages.AddRange(StaticValues.textureBytes);

        client.Send(messages.ToArray());

        Debug.Log("SendStudioData");
    }
}
