using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMode : MonoBehaviour
{
    Transform gameOverPanel;

    // GameMode做成单例模式
    public static GameMode Instance { get; private set; }

    // Awake函数被调用的时候，Instance应该被调用，或者说当Awake被调动的时候，GameMode已经创建好了
    private void Awake()
    {
        Instance = this;
    }

    public void Start()
    {
        // 找到canvas组件，利用GameObject.Find的方式
        GameObject canvas = GameObject.Find("Canvas");

        // canvas下面的子物体可以用组件.transform.Find的方式找到
        gameOverPanel = canvas.transform.Find("GameOverPanel");

        // 一运行游戏的时候canvas就消失了
        gameOverPanel.gameObject.SetActive(false);
    }

    // 玩家死后，把gameOverPanel设置为真
    public void OnPlayerDie()
    {
        gameOverPanel.gameObject.SetActive(true);
    }

    // 重新开始函数
    public void OnReStart()
    {
        SceneManager.LoadScene("Game");
    }
}
