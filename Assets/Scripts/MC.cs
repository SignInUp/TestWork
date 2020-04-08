using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MC : MonoBehaviour
{
    private GameObject _fightBall;
    private FightBall _fightBallFb;
    private Transform _throne;
    private Transform _way;
    private LineRenderer _wayLine;
    private float _radius;
    private float _secsToNextResize;
    private TextMeshProUGUI _scoreText;
    private Canvas _canvas;
    
    public int Score { get; set; }
    private void Start()
    {
        // Vars filling
        _scoreText = GameObject.Find("Score").GetComponent<TextMeshProUGUI>();
        _throne = GameObject.Find("Throne").GetComponent<Transform>();
        _way = transform.Find("Way");
        _wayLine = _way.GetComponent<LineRenderer>();
        _fightBall = GameObject.Find("FightBall");
        _fightBallFb = _fightBall.GetComponent<FightBall>();
        _radius = gameObject.GetComponent<CircleCollider2D>().radius;
        _secsToNextResize = 0f;
        Score = 2000;
        _canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        
        // Way Creating
        var rnd = new System.Random();
        var lWall = GameObject.Find("LeftWall").transform.position;
        var rWall = GameObject.Find("RightWall").transform.position;
        _throne.position = new Vector3(
                rnd.Next(Convert.ToInt32(lWall.x * 100f + 50f), 
                            Convert.ToInt32(rWall.x * 100f - 50f)) / 100f,
                    _throne.position.y,
                    _throne.position.z
        );
        _wayLine.SetPositions(new Vector3[2]{_way.position, _throne.position});
        _wayLine.startWidth = gameObject.transform.localScale.x;
        _wayLine.endWidth = _wayLine.startWidth;
    }

    private void Update()
    {
#if UNITY_EDITOR
        {
            // Filling fight ball
            var lineChanger = 0f;
            if (Input.GetMouseButton(0) && _fightBallFb.GetIsShooted == false)
            {
                var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                if (mousePos.y < -4.5f || mousePos.y > 7f)
                    return;

                var tp = transform.position;
                var fightBallPos = new Vector3(
                    tp.x + (mousePos - tp).normalized.x * 6f * _radius,
                    tp.y + (mousePos - tp).normalized.y * 6f * _radius,
                    0f
                );
                _fightBall.transform.position = fightBallPos;

                // Resize balls
                _secsToNextResize -= Time.deltaTime;
                if (_secsToNextResize <= 0)
                {
                    _secsToNextResize = 0.03f;
                    var resize = new Vector3(0.02f, 0.02f);
                    _fightBall.transform.localScale += resize;
                    gameObject.transform.localScale -= resize / 2f;

                    lineChanger = resize.x / 2f;
                    Score -= 10;

                    if (transform.localScale.x <= 0.1f)
                        Destroy(gameObject);
                }
            }
            // Throw fight ball
            else if (Input.GetMouseButtonUp(0) && _fightBallFb.GetIsShooted == false)
            {
                var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                var vel = new Vector3((mousePos - transform.position).normalized.x,
                    (mousePos - transform.position).normalized.y,
                    0f) * 16f;
                _fightBallFb.SetVelocity(true, vel);
            }

            _wayLine.SetPositions(new Vector3[2] {_way.position, _throne.position});
            _wayLine.startWidth -= lineChanger;
            _wayLine.endWidth = _wayLine.startWidth;
        }
#endif
#if UNITY_ANDROID
        {
            var lineChanger = 0f;
            // Filling fight ball
            if (Input.touchCount > 0 && _fightBallFb.GetIsShooted == false)
            {
                var touchPos = Camera.main.ScreenToWorldPoint(Input.touches[0].position);
                if (touchPos.y < -4.5f || touchPos.y > 7f)
                    return;

                var tp = transform.position;
                var fightBallPos = new Vector3(
                    tp.x + (touchPos).normalized.x * 6f * _radius,
                    tp.y + (touchPos - tp).normalized.y * 6f * _radius,
                    0f
                );
                _fightBall.transform.position = fightBallPos;

                // Resize balls
                _secsToNextResize -= Time.deltaTime;
                if (_secsToNextResize <= 0)
                {
                    _secsToNextResize = 0.03f;
                    var resize = new Vector3(0.02f, 0.02f);
                    _fightBall.transform.localScale += resize;
                    gameObject.transform.localScale -= resize / 2f;

                    lineChanger = resize.x / 2f;
                    Score -= 10;
                    
                    if (transform.localScale.x <= 0.1f)
                        Destroy(gameObject);
                }
            }
            // Throw fight ball
            else if (Input.GetMouseButtonUp(0) && _fightBallFb.GetIsShooted == false)
            {
                var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                var vel = new Vector3((mousePos - transform.position).normalized.x,
                    (mousePos - transform.position).normalized.y,
                    0f) * 16f;
                _fightBallFb.SetVelocity(true, vel);
            }
            _wayLine.SetPositions(new Vector3[2]{_way.position, _throne.position});
            _wayLine.startWidth -= lineChanger;
            _wayLine.endWidth = _wayLine.startWidth;
        }
#endif
    }

    private void OnDestroy()
    {
        if (_canvas != null)
            _canvas.sortingOrder = 2;
        if (_scoreText != null)
        {
            _scoreText.text = "Your score is " + Convert.ToString(Score) + "!";
            _scoreText.transform.position = new Vector3(0, 0);
        }
    }
}
