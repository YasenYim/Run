using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMode : MonoBehaviour
{
    Transform gameOverPanel;

    // GameMode���ɵ���ģʽ
    public static GameMode Instance { get; private set; }

    // Awake���������õ�ʱ��InstanceӦ�ñ����ã�����˵��Awake��������ʱ��GameMode�Ѿ���������
    private void Awake()
    {
        Instance = this;
    }

    public void Start()
    {
        // �ҵ�canvas���������GameObject.Find�ķ�ʽ
        GameObject canvas = GameObject.Find("Canvas");

        // canvas�������������������.transform.Find�ķ�ʽ�ҵ�
        gameOverPanel = canvas.transform.Find("GameOverPanel");

        // һ������Ϸ��ʱ��canvas����ʧ��
        gameOverPanel.gameObject.SetActive(false);
    }

    // ������󣬰�gameOverPanel����Ϊ��
    public void OnPlayerDie()
    {
        gameOverPanel.gameObject.SetActive(true);
    }

    // ���¿�ʼ����
    public void OnReStart()
    {
        SceneManager.LoadScene("Game");
    }
}
