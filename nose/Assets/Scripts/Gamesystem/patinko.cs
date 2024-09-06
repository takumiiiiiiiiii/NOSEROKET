using System;
using UnityEngine;
using UnityEngine.Video;

public class patinko : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public VideoClip[] videoClips;        // 再生する動画クリップを格納する配列
    public bool[] autoSwitchFlags ;// 各動画に対する自動遷移フラグ
    private int currentClipIndex = 0;
    private float x_before = 10;
    private float z_before = 10;
    void Start()
    {
        if (videoPlayer != null && videoClips.Length > 0)
        {
            // 最初の動画を再生
            videoPlayer.clip = videoClips[currentClipIndex];
            videoPlayer.Play();

            // 動画が終了したときにOnVideoEndメソッドを呼び出すイベントを登録
            videoPlayer.loopPointReached += OnVideoEnd;
        }
    }

    void Update()
    {
        Serial serial;//呼ぶスクリプトにあだ名をつける
        GameObject objc = GameObject.Find("sencer");//Circleというゲームオブジェクトを探す
        serial = objc.GetComponent<Serial>();//スクリプトを取得
        float x, z;
        switch (currentClipIndex)
        {
            case 0: //パチンコスタート
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
                        SwitchToBeforeClip();
                    }

                }
                if (!Input.GetKey(KeyCode.Space))
                {
                    SwitchToBeforeClip();
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
                if (!Input.GetKey(KeyCode.Space))
                {
                    SwitchToNextClip();
                }
                break;//switch文を抜ける
            case 4: //ホワイトアウト

                break;//switch文を抜ける
            case 5://100%UP

                break;
            default: //numが「1」と「2」以外の時に実行する
                break;//switch文を抜ける
        }
        if (currentClipIndex == 0)
            // スペースキーが押されたときに次の動画に遷移
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SwitchToNextClip();
            }
    }

    // 動画の終了時に呼ばれるイベントハンドラ
    void OnVideoEnd(VideoPlayer vp)
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

    void SwitchToNextClip()
    {
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
    void SwitchToBeforeClip()
    {
        currentClipIndex--;

        // 配列内に次の動画があるか確認
        if (currentClipIndex < videoClips.Length)
        {
            // 次の動画を設定して再生
            videoPlayer.clip = videoClips[currentClipIndex];
            videoPlayer.Play();
            videoPlayer.loopPointReached += OnVideoEnd;
        }
        else
        {
            
            Debug.Log("全ての動画が再生されました。");
            // 全ての動画が終了した場合の処理を追加（例: ゲーム終了、リスタートなど）
        }
    }
    void StopVideo()
    {
        if (videoPlayer.isPlaying)
        {
            videoPlayer.Stop();
            Debug.Log("動画を停止しました。");
        }
    }
}