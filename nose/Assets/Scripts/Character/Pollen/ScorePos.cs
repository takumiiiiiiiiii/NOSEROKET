using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

public class ScorePos : MonoBehaviour
{
    public RectTransform uiElement; // Canvas上のUI要素
    public static Vector3 worldPos;
    public Vector2 screenpos;
    // Start is called before the first frame update
    void Start()
    {
        screenpos = this.transform.position;
        worldPos = Camera.main.ScreenToWorldPoint(screenpos);
    }

    // Update is called once per frame
    void Update()
    {

        //RectTransformUtility.ScreenPointToWorldPointInRectangle(uiElement, uiElement.position, null, out worldPos);
        // スコアの座標を取得
    }
}
