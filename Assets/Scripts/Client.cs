using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

public class Client : MonoSingleton<Client>
{
    public Telepathy.Client client = new Telepathy.Client(1920 * 1080 + 1024);


    private void Awake()
    {
        // update even if window isn't focused, otherwise we don't receive.
        Application.runInBackground = true;

        // use Debug.Log functions for Telepathy so we can see it in the console
        Telepathy.Log.Info = Debug.Log;
        Telepathy.Log.Warning = Debug.LogWarning;
        Telepathy.Log.Error = Debug.LogError;

        // hook up events
        client.OnConnected = () => OnConnected();
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
            RequestGetPassword();
        }
    }

    private void OnApplicationQuit()
    {
        // the client/server threads won't receive the OnQuit info if we are
        // running them in the Editor. they would only quit when we press Play
        // again later. this is fine, but let's shut them down here for consistency
        client.Disconnect();
    }
    private void OnConnected()
    {
        Debug.Log("Client Connected");

        using (MemoryStream ms = new MemoryStream())
        using (BinaryWriter bw = new BinaryWriter(ms))
        {
            bw.Write(ConstantValues.CMD_REQUEST_CONNECT_STUDIO);

            client.Send(ms.ToArray());
        }
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
        Buffer.BlockCopy(messageBytes, 0, commandBytes, 0, 4);
        int command = BitConverter.ToInt32(commandBytes);

        switch (command)
        {
            case ConstantValues.CMD_RESPONSE_CONNECT_RESULT:
                ReceiveConnectResult(message: ref messageBytes);
                break;
            case ConstantValues.CMD_RESPONSE_GET_PASSWORD:
                ReceiveGetPassword(message: ref messageBytes);
                break;
            case ConstantValues.CMD_RESPONSE_ADD_STUDIO_DATA_RESULT:
                ReceiveResult(message: ref messageBytes);
                break;
            default:
                break;
        }
    }

    public void RequestGetPassword()
    {
        byte[] message = BitConverter.GetBytes(ConstantValues.CMD_REQUEST_GET_PASSWORD);
        client.Send(message);

        Debug.Log($"Request Get Password");
    }
    public void ReceiveGetPassword(ref byte[] message)
    {
        byte[] passwordBytes = new byte[4];
        Buffer.BlockCopy(message, 4, passwordBytes, 0, 4);

        int password = BitConverter.ToInt32(passwordBytes);
        StaticValues.password = password;

        Debug.Log($"Receive Get Password::{password}");

        RequestAddStudioData();
    }
    public void RequestAddStudioData()
    {
        using (MemoryStream ms = new MemoryStream())
        using (BinaryWriter bw = new BinaryWriter(ms))
        {
            bw.Write(ConstantValues.CMD_REQUEST_ADD_STUDIO_DATA);
            bw.Write(StaticValues.password);
            bw.Write(StaticValues.textureBytes.Length);
            bw.Write(StaticValues.textureBytes);

            client.Send(ms.ToArray());
        }

        Debug.Log($"Request Add Studio Data::{StaticValues.password}");
    }
    public void ReceiveResult(ref byte[] message)
    {
        byte[] bResult = new byte[1];
        Buffer.BlockCopy(message, 4, bResult, 0, 1);

        bool result = BitConverter.ToBoolean(bResult);

        Debug.Log($"Receive Add Studio Data Result::{result}");

        if (result)
        {
            Ctrl_SelectBase.instance.LoadNext();
        }
        else
        {
            Ctrl_SelectBase.instance.LoadError();
        }
    }
    public void ReceiveConnectResult(ref byte[] message)
    {
        byte[] resultBytes = new byte[1];
        Buffer.BlockCopy(message, 4, resultBytes, 0, 1);

        bool result = BitConverter.ToBoolean(resultBytes);

        Debug.Log($"Receive Connect Result::{result}");
    }
}
