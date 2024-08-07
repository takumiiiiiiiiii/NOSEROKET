using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class Pollen_tree_child : MonoBehaviour
{
    [SerializeField] private CircleCollider2D CC;
    [SerializeField] private Rigidbody2D RB;
    public GameObject tree;//花粉
    public int angle = 30;
    private int ID;
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
            Domino_camera.target = this.transform;
            RB.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY;
            RB.freezeRotation = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        float currentAngle = transform.eulerAngles.z;
        // 角度を-180から180の範囲に正規化します
        if (currentAngle > 180)
        {
            currentAngle -= 360;
        }

        // 現在の角度がトリガー角度を超えているかどうかをチェックします
        if (Mathf.Abs(currentAngle) > 30)
        {

        }
    }
}
