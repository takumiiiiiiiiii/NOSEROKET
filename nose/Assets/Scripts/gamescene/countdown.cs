using UnityEngine;
using UnityEngine.UI;
public class countdown : MonoBehaviour
{
    public Text textMesh;  // TextMesh コンポーネントの参照
    public float countdownTime = 3f;  // カウントダウンの秒数

    private float currentTime;

    void Start()
    {
        currentTime = countdownTime;
        textMesh.text = currentTime.ToString("F0");  // 初期値を表示
    }

    void Update()
    {
        if (currentTime > 0)
        {
            currentTime -= Time.deltaTime;  // カウントダウンの進行
            textMesh.text = Mathf.Ceil(currentTime).ToString();  // 小数点以下を切り捨てて表示
        }
        else
        {

        }
    }
}
