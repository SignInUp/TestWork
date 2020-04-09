using UnityEngine;
using UnityEngine.SceneManagement;

public class ButonsMenu : MonoBehaviour
{
    public void OnClickStart()
    {
        SceneManager.LoadScene("Main");
    }
}
