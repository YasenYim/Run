using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScene : MonoBehaviour
{
    // Canvas组件中，点击了Button，Button上面挂在了On Click（）一旦点击进入了Canvas，立即切换进入Scene1，即游戏场景
    public void OnbtnStart()
    {
        SceneManager.LoadScene(1);
    }
}
