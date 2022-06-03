using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using NavMeshBuilder = UnityEngine.AI.NavMeshBuilder;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //NavMeshBuilder.UpdateNavMeshData()
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Retry()
    {
        SceneManager.LoadScene("GameScene");
    }
}
