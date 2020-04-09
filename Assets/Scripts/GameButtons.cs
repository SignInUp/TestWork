using UnityEngine;
using UnityEngine.SceneManagement;

public class GameButtons : MonoBehaviour
{
    [SerializeField] private GameObject mc;
    [SerializeField] private GameObject startMoveButton;
    private void Start()
    {
        mc = GameObject.Find("MainCharacter");
        startMoveButton = GameObject.Find("StartMove");
    }

    public void OnClickMenu() { SceneManager.LoadScene("Menu");}

    public void OnClickStartMove()
    {
        mc.AddComponent<MainCharacterMove>();
        startMoveButton.SetActive(false);
    }
}
