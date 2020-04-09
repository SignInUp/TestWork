using UnityEngine;

public class FightBall : MonoBehaviour
{
    [SerializeField] private Vector3 velocity;
    public bool GetIsShooted { get; private set; }

    public void InverseVelocityX()
    {
        velocity.x = -velocity.x;
    }

    public void SetDefault()
    {
        // Random not visible point 
        transform.position = new Vector3(-100f, -100f, 0f);
        transform.localScale = new Vector3(0.1f, 0.1f);
        GetIsShooted = false;
    }
    
    private void Start()
    {
        velocity = new Vector3(0f, 0f, 90f);
        GetIsShooted = false;
    }

    private void Update()
    {
        if (GetIsShooted)
            transform.position += new Vector3(
                velocity.x * Time.deltaTime,
                velocity.y * Time.deltaTime,
                0f
            );
    }

    public void SetVelocity(bool isShooted, Vector3 velocity)
    {
        this.velocity = velocity;
        GetIsShooted = isShooted;
    }
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.name != "MainCharacter")
            SetDefault();
    }
}
