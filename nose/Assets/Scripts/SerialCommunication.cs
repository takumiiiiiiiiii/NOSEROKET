using System;
using System.IO.Ports;
using UnityEngine;

public class SerialCommunication : MonoBehaviour
{
    private SerialPort serialPort;
    public string portName = "/dev/cu.usbmodem101";  // MacOSでは「/dev/tty.usbmodemXXXX」などを使用
    public int baudRate = 9600;
    private bool isReading = true;

    void Start()
    {
        // シリアルポートの初期化
        serialPort = new SerialPort(portName, baudRate);
        serialPort.ReadTimeout = 1000;

        try
        {
            serialPort.Open();  // ポートを開く
            Debug.Log("Serial Port Opened");
        }
        catch (Exception e)
        {
            Debug.LogError("Could not open the serial port: " + e.Message);
        }

        // 非同期でデータ受信を開始
        if (serialPort.IsOpen)
        {
            InvokeRepeating("ReadFromPort", 0f, 0.1f);  // 0.1秒ごとに読み込みを行う
        }
    }

    void ReadFromPort()
    {
        if (serialPort.IsOpen)
        {
            try
            {
                string message = serialPort.ReadLine();  // 一行を読み取る
                Debug.Log("Received: " + message);
            }
            catch (TimeoutException) { }  // 読み込みタイムアウト時は無視
        }
    }

    void OnApplicationQuit()
    {
        // アプリケーション終了時にポートを閉じる
        if (serialPort != null && serialPort.IsOpen)
        {
            serialPort.Close();
            Debug.Log("Serial Port Closed");
        }
    }
}