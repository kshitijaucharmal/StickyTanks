using Unity.VisualScripting;
using UnityEngine;

public class Circle_Powerup : MonoBehaviour {

    [SerializeField] private float explosionTime = 0.5f;
    [SerializeField] private float force = 200f;
    [SerializeField] private float playerFlyForce = 10f;
    [SerializeField] private ParticleSystemRenderer colorParticles;

    [SerializeField] private Material player1Mat;
    [SerializeField] private Material player2Mat;

    [Header("Explosion Settings")]
    [SerializeField] private float range = 5f;

    private int playerN;

    public void SetPlayer(int playerNo){
        playerN = playerNo;
        colorParticles.material = playerNo == 1 ? player1Mat : player2Mat;
    }

    // Start is called before the first frame update
    void Start() {
        Destroy(gameObject, explosionTime);

        Collider[] colliders = Physics.OverlapSphere(transform.position, range);
        foreach(Collider nearObj in colliders){
            Rigidbody rb = nearObj.GetComponent<Rigidbody>();
            if(nearObj.CompareTag("Player" + playerN))
            {
                
                continue;
            }
            else if(nearObj.CompareTag("Player" + (playerN == 1 ? 2 : 1)))
            {
                rb.AddForce(transform.up * playerFlyForce, ForceMode.Impulse);
                PlayerMovement player = nearObj.GetComponent<PlayerMovement>();
                player.health.TakeDamage(10);
                
                continue;
            }
            else if (nearObj.CompareTag("Wall"))
            {
                rb = nearObj.AddComponent<Rigidbody>();
            }

            if(rb != null){
                rb.AddExplosionForce(force, transform.position, range);
            }
        }
    }

    void OnDrawGizmos(){
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }

    void FixedUpdate(){
        
    }
}
