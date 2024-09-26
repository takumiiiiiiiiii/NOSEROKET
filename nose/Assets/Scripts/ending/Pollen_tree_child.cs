using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class Pollen_tree_child : MonoBehaviour
{
    [SerializeField] private CircleCollider2D CC;
    [SerializeField] private Rigidbody2D RB;
    [SerializeField] private GameObject audio_tree_falldown;
    public GameObject tree;
    public GameObject NO1;//
    public GameObject NO2;//
    public GameObject NO3;//
    public int angle = 30;
    public float NO_height=3.0f;
    private int ID;
    private bool taoreru=false;
    

    // Start is called before the first frame update
    void Start()
    {

        ID = Pollen_tree_master.tree_cnt;
        Pollen_tree_master.tree_cnt++;
        RB = GetComponent<Rigidbody2D>();
        CC = GetComponent<CircleCollider2D>();
        if (Pollen_tree_master.tree_cnt < Pollen_tree_master.tree_cnt_max)
        {
            Instantiate(tree, this.transform.position + new Vector3(Pollen_tree_master.distance, 0, 0), Quaternion.identity);
        }
        if (ID > Pollen_tree_master.tree_cnt_end)
        {
           
            RB.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY;
            RB.freezeRotation = true;
            if (Pollen_tree_master.end == true)
            {
                
                Pollen_tree_master.end = false;
            }
        }
        if (Ranking.rankingshare[0] == ID && NO1 != null)
        {
            Instantiate(NO1, this.transform.position + new Vector3(0, NO_height, 0), Quaternion.identity);
        }
        if (Ranking.rankingshare[1] == ID && NO2 != null)
        {
            Instantiate(NO2, this.transform.position + new Vector3(0, NO_height, 0), Quaternion.identity);
        }
        if (Ranking.rankingshare[3] == ID && NO3 != null)
        {
            Instantiate(NO3, this.transform.position + new Vector3(0, NO_height, 0), Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {
        float currentAngle = transform.eulerAngles.z;
        //Debug.Log(currentAngle);
        // 角度を-180から180の範囲に正規化します
        if (currentAngle > 180)
        {
            currentAngle -= 360;
        }

        //Debug.Log(Mathf.Abs(currentAngle));
        // 現在の角度がトリガー角度を超えているかどうかをチェックします
        if (Mathf.Abs(currentAngle) > 10&&taoreru==false)
        {
            UIchake.schake = true;//UIをゆらす
            Domino_camera.target = this.transform;
            Instantiate(audio_tree_falldown, this.transform.position, Quaternion.identity);
            Debug.Log("fwsefwe");
            Ending_Count.tree++;
            taoreru = true;
            if (ID == Pollen_tree_master.tree_cnt_end)
            {
                Domino_camera.finaldomino = true;
            }
        }
    }
}
