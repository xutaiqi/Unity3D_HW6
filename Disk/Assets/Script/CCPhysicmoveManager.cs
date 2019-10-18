using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Interfaces;

public class CCPhysicmoveManager : SSActionManager,SSActionCallback ,IActionManager
{
    // Use this for initialization
    int count = 0;
    public SSActionEventType Complete = SSActionEventType.Completed;

    public void MoveDisk(Disk disk){
        count++;
        Complete = SSActionEventType.Started;
        CCPhysicmove action = CCPhysicmove.getAction(disk.speed);
        addAction(disk.gameObject, action, this);
    }

    public void SSActionCallback(SSAction source){
        count--;
        Complete = SSActionEventType.Completed;
        source.gameObject.SetActive(false);
    }
    public bool IsAllFinished(){
        if (count == 0) return true;
        return false;
    }
}
