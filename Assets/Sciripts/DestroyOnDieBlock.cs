using UnityEngine;

public class DestroyOnDieBlock : MonoBehaviour
{
    void OnTriggerEnter(Collider other){
        if(other.CompareTag("Player1") || other.CompareTag("Player2"))
        {
            return;
        }
        Destroy(other.gameObject);
    }
}
