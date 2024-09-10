//using System.Collections;
//using System.Collections.Generic;
//using System.Threading;
using System;
using System.IO.Ports;
using UnityEngine;
using UniRx;
//using System.Diagnostics;
//using UnityEditor.VersionControl;

public class Serial : MonoBehaviour
{

    [SerializeField] private string portName;
    [SerializeField] private int baurate;

    public bool conect=false,connect_char=false;
    public SerialPort serial;
    private bool isLoop = true;
    public float xfl=10000f, zfl=10000f;
    public string cntx,cntz,x="10",z="10";
    void Start()
    {
        this.serial = new SerialPort(portName, baurate, Parity.None, 8, StopBits.One);
        serial.DtrEnable = true;
        try
        {
            UnityEngine.Debug.Log("catch");
            this.serial.Open();
            //別スレッドで実行  
            Scheduler.ThreadPool.Schedule(() => ReadData()).AddTo(this);
            conect = true;
        }
        catch (Exception e)
        {
            UnityEngine.Debug.Log("ポートが開けませんでした。設定している値が間違っている場合があります");
            conect = false;
        }
    }
    private void Update()
    {
        
        //if (!conect)
        //{
        //    this.serial = new SerialPort(portName, baurate, Parity.None, 8, StopBits.One);

        //    try
        //    {
        //        UnityEngine.Debug.Log("catch");
        //        this.serial.Open();
        //        //別スレッドで実行  
        //        Scheduler.ThreadPool.Schedule(() => ReadData()).AddTo(this);
        //        conect = true;
        //    }
        //    catch (Exception e)
        //    {
        //        //UnityEngine.Debug.Log("ポートが開けませんでした。設定している値が間違っている場合があります");
        //        conect = false;
        //    }
        //}
    }

    //データ受信時に呼ばれる
    public void ReadData()
    {
        while (this.isLoop)
        {
            Debug.Log("whilestart");
            cntx = this.serial.ReadLine();
            cntz = this.serial.ReadLine();
            string cntx1 = cntx.Substring(0, 1);
            string cntz1 = cntz.Substring(0, 1);
            //Debug.Log("cntx1" + cntx1);
            //UnityEngine.Debug.Log("cntx1" + cntx1);
            //UnityEngine.Debug.Log("cntz2" + cntz1);
            if (cntx1 == "X")
            {
                x = cntx.Substring(1);
                z = cntz.Substring(1);
            }
            else if (cntx1 == "Y")
            {
                x = cntz.Substring(1);
                z = cntx.Substring(1);
            }
            Debug.Log("x" + x);
            Debug.Log("y" + z);
            //x = this.serial.ReadLine();
            //z = this.serial.ReadLine();
            //UnityEngine.Debug.Log(message);
            //UnityEngine.Debug.Log("x="+x);
            //UnityEngine.Debug.Log("z="+z);
            connect_char = true;
            Debug.Log("whileEnd");
            
            if (float.TryParse(x, out xfl) && float.TryParse(z, out zfl))
            {
                
            }
        }

        void OnDestroy()
        {
            if (serial != null)
            {
                this.isLoop = false;
                this.serial.Close();
            }
        }
    }
}