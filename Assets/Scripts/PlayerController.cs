using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    bool jump;
    PlayerCharacter cha;
    bool changeColor;

    private void Start()
    {
        cha = GetComponent<PlayerCharacter>();
    }

    // ������߼���������Ҽ��Ժ�changeColorΪ�棬����FixedUpdate��changeColor�Ž�ȥ�����뵽Move��������
    void Update()
    {
        if(Input.GetButtonDown("Jump"))
        {
            jump = true;
        }
        if (Input.GetButtonDown("Fire2"))
        {
            changeColor = true;
        }

    }

    private void FixedUpdate()
    {
        cha.Move(jump,changeColor);
        jump = false;
        changeColor = false;  // �ı�����ɫ�Ժ�Ҫ�Ļ���������һֱ����ɫ
    }
}
