using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Interfaces;

public class FirstSceneController : MonoBehaviour, ISceneController, UserAction
{
    public bool physicManager = false;
    bool nowmanager = false;
    int score = 0;
    int round = 1;//回合数
    int tral = 0;//飞碟计数
    bool start = false;
    IActionManager Manager;
    DiskFactory DF;

    void Awake()
    {
        SSDirector director = SSDirector.getInstance();
        director.currentScenceController = this;
        DF = DiskFactory.DF;//在场景中实例化一个飞碟工厂
                            //Manager = GetComponent<CCActionManager>();//管理飞碟的动作
                            // Manager = GetComponent<CCPhysicmoveManager>();
        Manager = this.gameObject.AddComponent<CCPhysicmoveManager>() as IActionManager;
        Debug.Log("shilihua");

    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    int count = 0;
    void Update()
    {
        if (start == true)//开始游戏
        {
            count++;
            if (count >= 80)//每80帧出现一个飞碟
            {
                count = 0;

                if (DF == null)
                {
                    Debug.LogWarning("DF is NUll!");
                    return;
                }
                tral++;
                Disk d = DF.GetDisk(round);//得到飞碟实例
                if(Manager==null){
                    Debug.LogWarning("Manager is  null");
                    return;
                }
                Manager.MoveDisk(d);//飞碟开始移动
                if (tral == 10)//毎十个飞碟算一轮
                {
                    round++;
                    tral = 0;
                }
            }
        }
    }

    public void LoadResources()
    {
    }

    public void Hit(Vector3 pos)
    {
        Ray ray = Camera.main.ScreenPointToRay(pos);//返回到一条从摄像机到屏幕指定点的射线

        RaycastHit[] hits;//存储被射线碰撞到的所有体
        hits = Physics.RaycastAll(ray);
        for (int i = 0; i < hits.Length; i++)
        {
            RaycastHit hit = hits[i];

            if (hit.collider.gameObject.GetComponent<Disk>() != null)//根据被碰撞到的物体的颜色确认得分
            {
                Color c = hit.collider.gameObject.GetComponent<Renderer>().material.color;
                if (c == Color.yellow) score += 1;
                if (c == Color.red) score += 2;
                if (c == Color.black) score += 3;

                hit.collider.gameObject.transform.position = new Vector3(0, -5, 0);
            }

        }
    }

    public int GetScore()
    {
        return score;
    }

    public void Restart()
    {
        score = 0;
        round = 1;
        start = true;
    }
    public bool RoundStop()
    {
        if (round > 3)
        {
            start = false;
            return Manager.IsAllFinished();
        }
        else return false;
    }
    public int GetRound()
    {
        return round;
    }
}