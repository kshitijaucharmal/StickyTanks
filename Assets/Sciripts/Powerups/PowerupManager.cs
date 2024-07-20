using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public enum powerupType{
    CIRCLE,
    SQAURE,
    TRIANGLE,
    NONE,
}

public class PowerupManager : MonoBehaviour {

    
    [Header("Settings")]
    [SerializeField] private int playerNo = 1;
    [SerializeField] private string usePowerupKey;
    [Range(0f, 1f)] [SerializeField] private float spawnDelay = 0.1f;

    // TEMP;
    [Header("Temp")]
    [SerializeField] private powerupType currentPowerup = powerupType.NONE;
    [SerializeField] private Transform pointsParent;
    private Queue<GameObject> points = new Queue<GameObject>();

    [Header("Powerup Objects")]
    [SerializeField] private Circle_Powerup circleExplosion;
    [SerializeField] private GameObject traingleWall;
    [SerializeField] private GameObject squareHole;

    private bool canUsePoweup = true;

    // Start is called before the first frame update
    void Start() {
        foreach(Transform point in pointsParent){
            points.Enqueue(point.gameObject);
        }

    }

    // Update is called once per frame
    void Update() {
        if(Input.GetButtonDown(usePowerupKey) && canUsePoweup){
            StartCoroutine(UsePowerup());
        }
    }

    IEnumerator UsePowerup(){
        canUsePoweup = false;
        // Get Points from trail script
        foreach(GameObject point in points){
            var pointPos = point.transform.position;
            if(currentPowerup == powerupType.CIRCLE){
                var powerupPrefab = circleExplosion;
                var powerup = Instantiate(powerupPrefab, pointPos, Quaternion.identity);
                powerup.SetPlayer(playerNo);
            }
            else if(currentPowerup == powerupType.SQAURE){
                var powerupPrefab = squareHole;
                var powerup = Instantiate(powerupPrefab, pointPos, Quaternion.identity);
            }
            else if(currentPowerup == powerupType.TRIANGLE){
                var powerupPrefab = traingleWall;
                var powerup = Instantiate(powerupPrefab, pointPos, Quaternion.identity);
            }
            else{
                Debug.Log("You Dont have any powerup");
            }

            Debug.Log("Powerup Spawned: " + currentPowerup);
            yield return new WaitForSeconds(spawnDelay);
        }
        canUsePoweup = true;
    }
}
