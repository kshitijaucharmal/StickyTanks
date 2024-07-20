using UnityEngine;

public class Triangle_Powerup : MonoBehaviour {

    [Header("Explosion Settings")]
    [SerializeField] private float range = 5f;
    [SerializeField] private Vector2 heightRange = new Vector2(4, 8);
    [SerializeField] private Vector2 wiggleRange = new Vector2(-1, 1);

    // Start is called before the first frame update
    void Start() {
        Collider[] colliders = Physics.OverlapSphere(transform.position, range);
        foreach(Collider nearObj in colliders){
            if(nearObj.CompareTag("GroundTile")){
                ReplaceWithWall(nearObj.transform);
            }
        }
    }

    void ReplaceWithWall(Transform nearObj){
        var pos = nearObj.position;
        pos.x += Random.Range(wiggleRange.x, wiggleRange.y);
        pos.z += Random.Range(wiggleRange.x, wiggleRange.y);
        var scale = transform.localScale;
        scale.y = Random.Range(heightRange.x, heightRange.y);
        transform.localScale = scale;
        pos.y = transform.localScale.y / 2 - 0.5f;
        transform.position = pos;
    }
}