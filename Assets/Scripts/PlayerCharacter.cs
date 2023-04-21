using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 玩家角色的颜色种类
public enum PlayerColor
{
    Red,
    Green,
}

public class PlayerCharacter : MonoBehaviour
{
    // 各种组件
    Rigidbody rigid;
    Animator anim;
    Renderer render;

    public float speed = 10;
    public float jumpSpeed = 4.6f;
    public int jumpCount = 0;

    bool isGround;

    PlayerColor color = PlayerColor.Red;

    public Transform prefabDieParticleRed;
    public Transform prefabDieParticleGreen;



    void Start()
    {
        rigid = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        render = GetComponentInChildren<Renderer>();
        Debug.Log("render" + render);
    }

  
    public void Move(bool jump,bool changeColor)
    {
        Vector3 velTemp = rigid.velocity;
        velTemp.z = speed;
        if(jump && jumpCount < 2)
        {
            velTemp.y = jumpSpeed;
            jumpCount++;
        }
        rigid.velocity = velTemp;

        //每一次移动完以后isGround都设置成false，然后当接触到碰撞体或者持续在碰撞体上则为true
        anim.SetBool("IsGround", isGround);
        // 每一帧移动完以后isGround都设置为false
        isGround = false;

        // 如果鼠标左击了以后那么就进入ChangeColor函数中
        if(changeColor)
        {
            ChangeColor();
        }
    }

    // 改变颜色函数
    void ChangeColor()
    {
        if (color == PlayerColor.Red) { color = PlayerColor.Green; }
        else { color = PlayerColor.Red; }

        if(color == PlayerColor.Red)
        {
            render.material.color = new Color(0.95f, 0.05f, 0.03f);
        }
        else
        {
            render.material.color = Color.green;
        }

        // 换颜色的时候也要触发开关，播放对应的动画
        anim.SetTrigger("Change");
    }

    // 进入碰撞体的时候isGround设置为true
    private void OnCollisionEnter(Collision collision)
    {
        string tag = collision.gameObject.tag;
        if(tag == "Green" || tag == "Red")
        {
            jumpCount = 0;
            isGround = true;
        }

        
    }

    // 持续处于碰撞关系的时候把isGround设置为true
    private void OnCollisionStay(Collision collision)
    {
        string tag = collision.gameObject.tag;
        if (tag == "Green" || tag == "Red")
        {
            jumpCount = 0;
            isGround = true;
        }

        // 如果玩家颜色为绿色，且碰撞到的物体为红色则死亡
        // 如果玩家颜色为红色，且碰撞到的物体为绿色则死亡
        if (color == PlayerColor.Green && tag == "Red")
        {
            PlayerDie();
        }
        else if (color == PlayerColor.Red && tag == "Green")
        {
            PlayerDie();
        }
    }

    // 玩家死亡函数
    void PlayerDie()
    {
        gameObject.SetActive(false);

        // 默认情况是红的
        Transform prefab = prefabDieParticleRed;

        // 如果玩家颜色变成了绿色，那么播放的粒子是绿色的
        if (color == PlayerColor.Green)
        {
            prefab = prefabDieParticleGreen;
        }

        // 播放爆炸粒子（指定物体的预制体，位置，朝向）
        Transform p = Instantiate(prefab, transform.position, Quaternion.identity);

        // 延迟函数，调用延迟死亡函数，延迟的时间为1s

        Invoke("DelayPlayerDie", 1);
    }

    // 延迟死亡函数
    void DelayPlayerDie()
    {
        // 死亡是一个状态应该放在一个单独的脚本中用来调用玩家当前的状态，例如游戏中的吃金币系统，这里放在GameMode中
        // 调用GameMode的OnPlayerDie函数,GameMode已经做成了单例的模式
        GameMode.Instance.OnPlayerDie();
    }
}
