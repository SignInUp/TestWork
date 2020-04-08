using UnityEngine;

public class ObsctacleTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "Circle")
        {
            Destroy(other.gameObject.transform.parent.gameObject);
        }
    }
}
