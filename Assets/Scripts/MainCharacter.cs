using System;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using Random = System.Random;

public class MainCharacter : MonoBehaviour
{
    [SerializeField] private GameObject fightBall;
    [SerializeField] private FightBall fightBallFb;
    [SerializeField] private Transform throne;
    [SerializeField] private Transform way;
    [SerializeField] private LineRenderer wayLine;
    [SerializeField] private float radius;
    [SerializeField] private float secsToNextResize;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private new Camera camera;
    [SerializeField] private float resizeVal;
    [SerializeField] private float fightballPostionCoef;
    [SerializeField] private float fightballVelocityCoef;
    [SerializeField] private float secsToNegative;
    [SerializeField] private float maxSizeMC;
    [SerializeField] private int deltaScore;
    private float _topTouchablePos;
    private float _botTouchablePos;

    private Canvas _canvas;
    
    public int Score { get; set; }
    private void Start()
    {
        // Vars filling
        scoreText = GameObject.Find("Score").GetComponent<TextMeshProUGUI>();
        throne = GameObject.Find("Throne").GetComponent<Transform>();
        way = transform.Find("Way");
        wayLine = way.GetComponent<LineRenderer>();
        fightBall = GameObject.Find("FightBall");
        fightBallFb = fightBall.GetComponent<FightBall>();
        radius = gameObject.GetComponent<CircleCollider2D>().radius;
        camera = Camera.main;
        Score = 2000;
        resizeVal = 0.03f;
        fightballPostionCoef = 6f;
        fightballVelocityCoef = 16f;
        secsToNegative = 0.03f;
        secsToNextResize = secsToNegative;
        maxSizeMC = 0.1f;
        deltaScore = 10;
        _canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        _topTouchablePos = 7f;
        _botTouchablePos = -4.5f;
         
        
        // Way Creating
        var lWallX = GameObject.Find("LeftWall").transform.position.x;
        var rWallX = GameObject.Find("RightWall").transform.position.x;
        throne.position = new Vector3(
                    UnityEngine.Random.Range(lWallX + 0.5f, rWallX - 0.5f),
                    throne.position.y,
                    throne.position.z
        );
        wayLine.SetPositions(new Vector3[2]{way.position, throne.position});
        wayLine.startWidth = gameObject.transform.localScale.x;
        wayLine.endWidth = wayLine.startWidth;
    }

    private void Update()
    {
#if UNITY_EDITOR
        {
            // Filling fight ball
            var lineChanger = 0f;
            if (Input.GetMouseButton(0) && fightBallFb.GetIsShooted == false)
            {
                var mousePos = camera.ScreenToWorldPoint(Input.mousePosition);
                if (mousePos.y < _botTouchablePos || mousePos.y > _topTouchablePos)
                    return;

                var tp = transform.position;
                var fightBallPos = new Vector3(
                    tp.x + (mousePos - tp).normalized.x * fightballPostionCoef * radius,
                    tp.y + (mousePos - tp).normalized.y * fightballPostionCoef * radius,
                    0f
                );
                fightBall.transform.position = fightBallPos;

                // Resize balls
                secsToNextResize -= Time.deltaTime;
                if (secsToNextResize <= 0)
                {
                    secsToNextResize = secsToNegative;
                    var resize = new Vector3(resizeVal, resizeVal);
                    fightBall.transform.localScale += resize;
                    gameObject.transform.localScale -= resize / 2f;

                    lineChanger = resize.x / 2f;
                    Score -= deltaScore;

                    if (transform.localScale.x <= maxSizeMC)
                        Destroy(gameObject);
                }
            }
            // Throw fight ball
            else if (Input.GetMouseButtonUp(0) && fightBallFb.GetIsShooted == false)
            {
                var mousePos = camera.ScreenToWorldPoint(Input.mousePosition);
                var vel = new Vector3(
                    (mousePos - transform.position).normalized.x,
                    (mousePos - transform.position).normalized.y,
                    0f) * fightballVelocityCoef;
                fightBallFb.SetVelocity(true, vel);
            }

            wayLine.SetPositions(new Vector3[2] {way.position, throne.position});
            wayLine.startWidth -= lineChanger;
            wayLine.endWidth = wayLine.startWidth;
        }
#endif
#if UNITY_ANDROID
        {
            var lineChanger = 0f;
            // Filling fight ball
            if (Input.touchCount > 0 && !fightBallFb.GetIsShooted)
            {
                var touchPos = camera.ScreenToWorldPoint(Input.touches[0].position);
                if (touchPos.y < _botTouchablePos || touchPos.y > _topTouchablePos)
                    return;

                var tp = transform.position;
                var fightBallPos = new Vector3(
                    tp.x + (touchPos).normalized.x * fightballPostionCoef * radius,
                    tp.y + (touchPos - tp).normalized.y * fightballPostionCoef * radius,
                    0f
                );
                fightBall.transform.position = fightBallPos;

                // Resize balls
                secsToNextResize -= Time.deltaTime;
                if (secsToNextResize <= 0)
                {
                    secsToNextResize = secsToNegative;
                    var resize = new Vector3(resizeVal, resizeVal);
                    fightBall.transform.localScale += resize;
                    gameObject.transform.localScale -= resize / 2f;

                    lineChanger = resize.x / 2f;
                    Score -= deltaScore;
                    
                    if (transform.localScale.x <= maxSizeMC)
                        Destroy(gameObject);
                }
            }
            // Throw fight ball
            else if (Input.GetMouseButtonUp(0) && !fightBallFb.GetIsShooted)
            {
                var mousePos = camera.ScreenToWorldPoint(Input.mousePosition);
                var vel = new Vector3(
                    (mousePos - transform.position).normalized.x,
                    (mousePos - transform.position).normalized.y,
                    0f) * fightballVelocityCoef;
                fightBallFb.SetVelocity(true, vel);
            }
            wayLine.SetPositions(new Vector3[2]{way.position, throne.position});
            wayLine.startWidth -= lineChanger;
            wayLine.endWidth = wayLine.startWidth;
        }
#endif
    }

    private void OnDestroy()
    {
        if (_canvas != null)
            _canvas.sortingOrder = 2;
        if (scoreText != null)
        {
            scoreText.text = "Your score is " + Convert.ToString(Score) + "!";
            scoreText.transform.position = new Vector3(0, 0);
        }
    }
}
