using UnityEngine;

public class Throne : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.name != "MainCharacter") return;
        Destroy(other.gameObject);
    }
}
