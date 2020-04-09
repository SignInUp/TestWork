using System.Collections;
using UnityEngine;

public class MainCharacterMove : MonoBehaviour
{
    [SerializeField] private Transform mc;
    [SerializeField] private Vector3 velocity;
    [SerializeField] private float velocityVal;
    private void Start()
    {
        mc = gameObject.GetComponent<Transform>();
        velocityVal = 5f;
        var throne = GameObject.Find("Throne").transform.position;
        velocity = (throne - mc.position).normalized * velocityVal;
    }
    IEnumerator Move()
    {
        mc.position += velocity * Time.deltaTime;
        yield return new WaitForFixedUpdate();
    }

    private void FixedUpdate()
    {
        StartCoroutine(Move());
    }
}
