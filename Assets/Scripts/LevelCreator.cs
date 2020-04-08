using UnityEngine;

public class LevelCreator : MonoBehaviour
{
    private GameObject _mc;
    private Transform _throne;

    private void Awake()
    {
        _mc = GameObject.Find("MC");
        _throne = GameObject.Find("Throne").GetComponent<Transform>();

        // Replace MC
        var div = (16.0f / 9.0f) / ((float) Screen.height / Screen.width);
        var mcPos = _mc.transform.position;
        _mc.transform.position = new Vector3(mcPos.x * div, mcPos.y, mcPos.z);

        _mc.transform.localScale *= div;
        _throne.localScale *= div;
        
        // Making obstacles
        var rightBoard = System.Convert.ToInt32(446f * div * 10f);
        var rnd = new System.Random();
        var topPos = 447;
        var downPos = -350;
        for (var i = 0; i < 120; ++i)
        {
            var obs = GameObject.Instantiate(Resources.Load<GameObject>("Prephabs/obstacle"));
            obs.transform.position = new Vector3(
                rnd.Next(-rightBoard, rightBoard) / 1000f,
                rnd.Next(downPos, topPos) / 100f,
                0f);
            obs.transform.localScale *= div;
        }
    }
}
