using System.Collections;
using System.Security.Cryptography;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private Transform _fightBallCol;
    private CircleCollider2D _ownTrigger;
    private MC _mc;
    private void Start()
    {
        _fightBallCol = GameObject.Find("FightBall").GetComponent<Transform>();
        _ownTrigger = GetComponentInChildren<CircleCollider2D>();
        _mc = GameObject.Find("MC").GetComponent<MC>();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        switch (other.gameObject.name)
        {
            case "MC":
                _mc.Score = 0;
                Destroy(other.gameObject);
                break;
            case "FightBall":
                _ownTrigger.radius += _fightBallCol.transform.localScale.x;
                StartCoroutine(DefferedDestroy());
                break;
        }
    }

    private IEnumerator DefferedDestroy()
    {
        yield return new WaitForSeconds(0.1f);
        Destroy(gameObject);
        yield return null;
    }
}
