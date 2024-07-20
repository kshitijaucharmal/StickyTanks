using Unity.VisualScripting;
using UnityEngine;

public class Square_Powerup : MonoBehaviour {

    [Header("Explosion Settings")]
    [SerializeField] private float range = 5f;

    // Start is called before the first frame update
    void Start() {
        Collider[] colliders = Physics.OverlapSphere(transform.position, range);
        foreach(Collider nearObj in colliders){
            if(nearObj.CompareTag("GroundTile")){
                if(nearObj.GetComponent<Rigidbody>() == null) nearObj.AddComponent<Rigidbody>();
            }
        }
        Destroy(gameObject);
    }

    void OnDrawGizmos(){
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}