using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScene : MonoBehaviour
{
    // Canvas����У������Button��Button���������On Click����һ�����������Canvas�������л�����Scene1������Ϸ����
    public void OnbtnStart()
    {
        SceneManager.LoadScene(1);
    }
}
