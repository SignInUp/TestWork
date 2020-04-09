using System.Collections;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField] private Transform fightBallCol;
    [SerializeField] private CircleCollider2D ownTrigger;
    [SerializeField] private MainCharacter mainCharacter;
    private void Start()
    {
        fightBallCol = GameObject.Find("FightBall").GetComponent<Transform>();
        ownTrigger = GetComponentInChildren<CircleCollider2D>();
        mainCharacter = GameObject.Find("MainCharacter").GetComponent<MainCharacter>();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        switch (other.gameObject.name)
        {
            case "MainCharacter":
                mainCharacter.Score = 0;
                Destroy(other.gameObject);
                break;
            case "FightBall":
                ownTrigger.radius += fightBallCol.transform.localScale.x;
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
