using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour
{
    private GameObject _mc;
    private GameObject _startMoveButton;
    private void Start()
    {
        _mc = GameObject.Find("MC");
        _startMoveButton = GameObject.Find("StartMove");
    }

    public void OnClickMenu() { SceneManager.LoadScene("Menu");}

    public void OnClickStartMove()
    {
        _mc.AddComponent<MCMove>();
        _startMoveButton.SetActive(false);
    }
    
}
