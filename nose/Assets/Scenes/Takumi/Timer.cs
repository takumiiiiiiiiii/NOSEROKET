using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement; // UnityEngine.SceneManagemntの機能を使用


public class Timer1 : MonoBehaviour
{

    //　トータル制限時間
    private float totalTime;
    //　制限時間（分）
    [SerializeField]
    private int minute;
    //　制限時間（秒）
    [SerializeField]
    private float seconds;
    //　前回Update時の秒数
    private float oldSeconds;
    public Text timerText;
    public float waitTime = 1000;

    //public GameObject End;
    AudioSource audiosource;
    void Start()
    {
        //minute = 0;
        //seconds = 0;
        totalTime = minute * 60 + seconds;
        oldSeconds = 0f;
        timerText = GetComponentInChildren<Text>();
        audiosource = GetComponent<AudioSource>();
    }

    float countdown = 3f;
    int count;

    void Update()
    {
        if (countdown >= 0)
        {
            countdown -= Time.deltaTime;
            count = (int)countdown;
        }
        if (countdown <= 0)
        {
            //　制限時間が0秒以下なら何もしない
            if (totalTime <= 0f)
            {
                return;
            }
            //　一旦トータルの制限時間を計測；
            totalTime = minute * 60 + seconds;
            totalTime -= Time.deltaTime;

            //　再設定
            minute = (int)totalTime / 60;
            seconds = totalTime - minute * 60;

            //　タイマー表示用UIテキストに時間を表示する
            if ((int)seconds != (int)oldSeconds)
            {
                timerText.text = minute.ToString("00") + ":" + ((int)seconds).ToString("00");
            }
            oldSeconds = seconds;
            //　制限時間以下になったらコンソールに『制限時間終了』という文字列を表示する
            if (totalTime <= 0f)
            {
                Debug.Log("制限時間終了");
                //Instantiate(End);
                StartCoroutine(nameof(LoadScene));
                // ResultSceneに切り替える
            }
        }
    }
    IEnumerator LoadScene()
    {
        //Instantiate(End);
        yield return new WaitForSeconds(waitTime);
        SceneManager.LoadScene("ResultScene");
        //SceneManager.LoadScene(targetSceneName);
    }
}