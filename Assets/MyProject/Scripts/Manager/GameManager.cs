using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager
{
    private static readonly Lazy<GameManager> instance = new Lazy<GameManager>(() => new GameManager());

    public static GameManager Instance
    {
        get { return instance.Value; }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Retry()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void NextStage()
    {
        SceneManager.LoadScene("GameScene2");
    }
}
