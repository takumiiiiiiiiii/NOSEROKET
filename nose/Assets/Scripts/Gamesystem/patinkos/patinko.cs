using System;
using System.Security.Policy;
using UnityEngine;
using UnityEngine.Video;
using System.Collections;

public class patinko : MonoBehaviour
{
    public Animator anima;
    public static bool patinkotime=false;

    public VideoPlayer videoPlayer;
    public VideoClip[] videoClips;        // 再生する動画クリップを格納する配列
    public bool[] autoSwitchFlags ;// 各動画に対する自動遷移フラグ
    private int currentClipIndex = 0;//現在の動画番号
    private float x_before = 10000;//センサーの前の値
    private float z_before = 10000;//センサーの前の値
    //boolたち
    private bool Startanime = false;
    private bool patinkoawake = true;//一回だけ呼び出す
    private bool patinkoEnd = false;//パチンコの終了時を洗わす
    public SliderController Sdc;//スライダーを参照する
    public Nosemove NOSE;//スライダーを参照する
    public baranimation bar;//スライダーを参照する
    void Start()
    {
        patinkotime = false;
        if (videoPlayer != null && videoClips.Length > 0)
        {
            // 最初の動画を再生
            //videoPlayer.clip = videoClips[currentClipIndex];
            //videoPlayer.Play();
            // 動画が終了したときにOnVideoEndメソッドを呼び出すイベントを登録
            videoPlayer.loopPointReached += OnVideoEnd;
        }
        Sdc = GameObject.Find("dash_slider").GetComponent<SliderController>();
        NOSE = GameObject.Find("nose_player").GetComponent<Nosemove>();
        bar = GameObject.Find("bars").GetComponent<baranimation>();
        anima = GetComponent<Animator>();
    }

    void Update()
    {
        if (Sdc.pollenPoint >= 100)
        {
            bar.animationStart();
        }
        if (patinkotime)
        {
            StartVideos();
        }
        else
        {

        }
    }

    //動画を呼び出す
    void StartVideos()
    {
        if (Sdc.pollenPoint >= 100 && !patinkoEnd)//ポイントが100だったら
        {
            if (!patinkoEnd)//パチンコが終了していない場合
            {
                Nosemove.DoNotMove = true;
            }
            else
            {
                Camerapatinko camepati = GameObject.Find("MainCamera").GetComponent<Camerapatinko>();
                camepati.CameraPatinkoAnimeEnd();
            }
            if (patinkoawake)//一回だけ呼び出す
            {
                Timer.timerStop = true;
                NOSE.transform.eulerAngles = new Vector3(0, 0, 0);
                Camerapatinko camepati = GameObject.Find("MainCamera").GetComponent<Camerapatinko>();
                camepati.CameraPatinkoAnime(true);
                anima.SetBool("patiStart", true);//
                patinkoawake = false;
            }
            Switch_Animation();//アニメーションをを移動させるいろんな処理
        }
        else
        {
            currentClipIndex = 0;
        }
    }
    // 動画の終了時に呼ばれるイベントハンドラ
    void OnVideoEnd(VideoPlayer vp)
    {
        if (currentClipIndex < videoClips.Length)
        {
            // 自動で次の動画に遷移させるかどうかをフラグで判断
            if (autoSwitchFlags[currentClipIndex])
            {
                SwitchToNextClip();
            }
            else
            {
                Debug.Log("この動画では自動遷移しません。");
            }
        }
    }

    // 動画の遷移をコントロールする
    void Switch_Animation()
    {
        Debug.Log("現在の動画No:" + currentClipIndex);
        Debug.Log("動画の長さ:" + videoClips.Length);
        Serial serial;//呼ぶスクリプトにあだ名をつける
        GameObject objc = GameObject.Find("sencer");//Circleというゲームオブジェクトを探す
        serial = objc.GetComponent<Serial>();//スクリプトを取得
        float x, z;
        switch (currentClipIndex)
        {
            case 0: //パチンコスタート
                videoPlayer.clip = videoClips[currentClipIndex];
                videoPlayer.Play();
                break; //switch文を抜ける
            case 1: //鼻をつまめ
                if (serial.conect && float.TryParse(serial.x, out x) && float.TryParse(serial.z, out z))
                {

                    if (x < Nosemove.growlevel && z < Nosemove.growlevel)
                    {
                        SwitchToNextClip();
                    }
                    x_before = x;
                    z_before = z;
                }
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    SwitchToNextClip();
                }
                break;//switch文を抜ける
            case 2: //チャージ中
                if (serial.conect && float.TryParse(serial.x, out x) && float.TryParse(serial.z, out z))
                {

                    if (x >= Nosemove.growlevel && z >= Nosemove.growlevel)
                    {
                        Debug.Log("x" + x + "growlevel" + Nosemove.growlevel);
                        SwitchToBeforeClip();
                    }

                }
                else {
                    if (!Input.GetKey(KeyCode.Space))
                    {
                        SwitchToBeforeClip();
                    }
                }
                break;//switch文を抜ける
            case 3: //はなせ！
                if (serial.conect && float.TryParse(serial.x, out x) && float.TryParse(serial.z, out z))
                {

                    if (x >= Nosemove.growlevel && z >= Nosemove.growlevel)
                    {
                        SwitchToNextClip();
                    }
                    x_before = x;
                    z_before = z;

                }
                else
                {
                    if (!Input.GetKey(KeyCode.Space))
                    {
                        SwitchToNextClip();
                    }
                }
                break;//switch文を抜ける
            case 4: //ホワイトアウト

                break;//switch文を抜ける
            case 5://100%UP

                break;
            default: //numが「1」と「2」以外の時に実行する
                break;//switch文を抜ける
        }
    }

    //動画を次の動画へ遷移させる
    void SwitchToNextClip()
    {
        Debug.Log("次の動画へ");
        currentClipIndex++;

        // 配列内に次の動画があるか確認
        if (currentClipIndex < videoClips.Length)
        {
            // 次の動画を設定して再生
            videoPlayer.clip = videoClips[currentClipIndex];
            videoPlayer.Play();

        }
        else
        {
            StopVideo();
            Debug.Log("全ての動画が再生されました。");
            // 全ての動画が終了した場合の処理を追加（例: ゲーム終了、リスタートなど）
        }
    }

    //動画を前の動画へ遷移させる
    void SwitchToBeforeClip()
    {
        currentClipIndex--;

        // 配列内に次の動画があるか確認
        if (currentClipIndex < videoClips.Length)
        {
            // 前の動画を設定して再生
            videoPlayer.clip = videoClips[currentClipIndex];
            videoPlayer.Play();
        }
        else
        {
            
            Debug.Log("全ての動画が再生されました。");
            // 全ての動画が終了した場合の処理を追加（例: ゲーム終了、リスタートなど）
        }
    }

    //動画の再生を終了する
    void StopVideo()
    {
        
        Debug.Log("動画の再生を終了");
        if (videoPlayer.isPlaying)
        {
            videoPlayer.Stop();
            Debug.Log("動画を停止しました。");
        }
        Camerapatinko camepati = GameObject.Find("MainCamera").GetComponent<Camerapatinko>();
        camepati.CameraPatinkoAnimeEnd();
        Nosemove nose = GameObject.Find("nose_player").GetComponent<Nosemove>();
        nose.SuperDashStart();
        anima.SetBool("patiStart", false);
        StartCoroutine(moveAgain());
    }
    private IEnumerator moveAgain()
    {
        Timer.timerStop = false;
        yield return new WaitForSeconds(1);//1秒後にダッシュ終わり
        patinkoEnd = true;
        Nosemove nose = GameObject.Find("nose_player").GetComponent<Nosemove>();
        Nosemove.DoNotMove = false;
        patinkoawake = true;
    }
    

}