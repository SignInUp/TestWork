﻿using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void OnClickStart()
    {
        SceneManager.LoadScene("Main");
    }
}
