using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ��ҽ�ɫ����ɫ����
public enum PlayerColor
{
    Red,
    Green,
}

public class PlayerCharacter : MonoBehaviour
{
    // �������
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

        //ÿһ���ƶ����Ժ�isGround�����ó�false��Ȼ�󵱽Ӵ�����ײ����߳�������ײ������Ϊtrue
        anim.SetBool("IsGround", isGround);
        // ÿһ֡�ƶ����Ժ�isGround������Ϊfalse
        isGround = false;

        // ������������Ժ���ô�ͽ���ChangeColor������
        if(changeColor)
        {
            ChangeColor();
        }
    }

    // �ı���ɫ����
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

        // ����ɫ��ʱ��ҲҪ�������أ����Ŷ�Ӧ�Ķ���
        anim.SetTrigger("Change");
    }

    // ������ײ���ʱ��isGround����Ϊtrue
    private void OnCollisionEnter(Collision collision)
    {
        string tag = collision.gameObject.tag;
        if(tag == "Green" || tag == "Red")
        {
            jumpCount = 0;
            isGround = true;
        }

        
    }

    // ����������ײ��ϵ��ʱ���isGround����Ϊtrue
    private void OnCollisionStay(Collision collision)
    {
        string tag = collision.gameObject.tag;
        if (tag == "Green" || tag == "Red")
        {
            jumpCount = 0;
            isGround = true;
        }

        // ��������ɫΪ��ɫ������ײ��������Ϊ��ɫ������
        // ��������ɫΪ��ɫ������ײ��������Ϊ��ɫ������
        if (color == PlayerColor.Green && tag == "Red")
        {
            PlayerDie();
        }
        else if (color == PlayerColor.Red && tag == "Green")
        {
            PlayerDie();
        }
    }

    // �����������
    void PlayerDie()
    {
        gameObject.SetActive(false);

        // Ĭ������Ǻ��
        Transform prefab = prefabDieParticleRed;

        // ��������ɫ�������ɫ����ô���ŵ���������ɫ��
        if (color == PlayerColor.Green)
        {
            prefab = prefabDieParticleGreen;
        }

        // ���ű�ը���ӣ�ָ�������Ԥ���壬λ�ã�����
        Transform p = Instantiate(prefab, transform.position, Quaternion.identity);

        // �ӳٺ����������ӳ������������ӳٵ�ʱ��Ϊ1s

        Invoke("DelayPlayerDie", 1);
    }

    // �ӳ���������
    void DelayPlayerDie()
    {
        // ������һ��״̬Ӧ�÷���һ�������Ľű�������������ҵ�ǰ��״̬��������Ϸ�еĳԽ��ϵͳ���������GameMode��
        // ����GameMode��OnPlayerDie����,GameMode�Ѿ������˵�����ģʽ
        GameMode.Instance.OnPlayerDie();
    }
}
