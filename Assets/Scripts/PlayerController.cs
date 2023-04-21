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

    // 总体的逻辑就是鼠标右键以后，changeColor为真，随着FixedUpdate把changeColor放进去，传入到Move函数里面
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
        changeColor = false;  // 改变完颜色以后要改回来，不能一直改颜色
    }
}
