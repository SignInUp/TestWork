using UnityEngine;

public class LevelCreator : MonoBehaviour
{
    [SerializeField] private GameObject mc;
    [SerializeField] private Transform throne;
    private float _topObstaclePos;
    private float _botObstaclePos;
    private float _rightBoard;
    
    private void Awake()
    {
        mc = GameObject.Find("MainCharacter");
        throne = GameObject.Find("Throne").GetComponent<Transform>();

        // Replace mc
        var div = (16.0f / 9.0f) / ((float) Screen.height / Screen.width);
        var mcPos = mc.transform.position;
        mc.transform.position = new Vector3(mcPos.x * div, mcPos.y, mcPos.z);

        mc.transform.localScale *= div;
        throne.localScale *= div;
        
        _topObstaclePos = 4.47f;
        _botObstaclePos = -3.5f;
        _rightBoard = 4.46f * div;

        // Making obstacles
        for (var i = 0; i < 120; ++i)
        {
            var obs = Instantiate(Resources.Load<GameObject>("Prephabs/obstacle"));
            obs.transform.position = new Vector3(
                Random.Range(-_rightBoard, _rightBoard),
                Random.Range(_botObstaclePos, _topObstaclePos),
                0f);
            obs.transform.localScale *= div;
        }
    }
}
