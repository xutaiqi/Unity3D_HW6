using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CCPhysicmove : SSAction
{
    // Use this for initialization
    public float xspeed;

    public override void Start()
    {
        if (!this.gameObject.GetComponent<Rigidbody>())
        {
            this.gameObject.AddComponent<Rigidbody>();
        }
        this.gameObject.GetComponent<Rigidbody>().AddForce(Vector3.up*8f, ForceMode.Acceleration);
        this.gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(xspeed, 0, 0), ForceMode.VelocityChange);
    }
    private CCPhysicmove()
    {

    }
    public static CCPhysicmove getAction(float speed){
        CCPhysicmove action = CreateInstance<CCPhysicmove>();
        action.xspeed = speed;
        return action;
    }

	// Update is called once per frame

	override public void Update()
	{
        if(transform.position.y<=3){
            Destroy(this.gameObject.GetComponent<Rigidbody>());
            destroy = true;
            CallBack.SSActionCallback(this);
        }
	}
}
