using UnityEngine;

public class DestroyOnDieBlock : MonoBehaviour
{
    void OnTriggerEnter(Collider other){
        Destroy(other.gameObject);
    }
}
