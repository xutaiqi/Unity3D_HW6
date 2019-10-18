using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Interfaces
{
    public interface ISceneController
    {
        void LoadResources();
    }

    public interface UserAction
    {
        void Hit(Vector3 pos);//点击飞碟后，飞碟消失
        void Restart();//重新开始
        int GetScore();//返回得到的分数
        bool RoundStop();//是否停止
        int GetRound();//得到当前回合数
    }

    public enum SSActionEventType : int { Started, Completed }

    public interface SSActionCallback
    {
        void SSActionCallback(SSAction source);//动作回调
    }
    public interface IActionManager{
        void MoveDisk(Disk disk);
        bool IsAllFinished();
    }

}