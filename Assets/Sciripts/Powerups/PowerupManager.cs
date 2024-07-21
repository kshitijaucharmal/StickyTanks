using System.Collections;
using UnityEngine;

public enum PowerupType
{
    CIRCLE,
    SQUARE,
    TRIANGLE,
    NONE,
}

public class PowerupManager : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private int playerNo = 1;
    [SerializeField] private string usePowerupKey;
    [Range(0f, 1f)][SerializeField] private float spawnDelay = 0.1f;

    private PowerupType currentPowerup = PowerupType.NONE;

    [Header("Powerup Objects")]
    [SerializeField] private Circle_Powerup circleExplosion;
    [SerializeField] private GameObject triangleWall;
    [SerializeField] private GameObject squareHole;

    private PlayerMovement playerMovement;
    public AudioManager audioManager;
    
    
    // Start is called before the first frame update
    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown(usePowerupKey) && playerMovement.canUsePowerup)
        {
            StartCoroutine(UsePowerup());
        }
    }

    public void SetPowerupType(PowerupType type)
    {
        currentPowerup = type;
    }

    IEnumerator UsePowerup()
    {
        playerMovement.canUsePowerup = false;
        bool audioPlayed = false;
        // Get Points from trail script
        var points = playerMovement.positions.ToArray();
        foreach (Vector3 pointPos in points)
        {
            if (currentPowerup == PowerupType.CIRCLE)
            {
                var powerupPrefab = circleExplosion;
                var powerup = Instantiate(powerupPrefab, pointPos, Quaternion.identity);
                powerup.SetPlayer(playerNo);
            }
            else if (currentPowerup == PowerupType.SQUARE)
            {
                var powerupPrefab = squareHole;
                if (!audioPlayed && audioManager != null)
                    {
                        audioManager.Play("square");
                        audioPlayed = true;
                    }
                var powerup = Instantiate(powerupPrefab, pointPos, Quaternion.identity);

            }
            else if (currentPowerup == PowerupType.TRIANGLE)
            {
                var powerupPrefab = triangleWall;
                var powerup = Instantiate(powerupPrefab, pointPos, Quaternion.identity);
            }
            else if (currentPowerup == PowerupType.NONE)
            {
                playerMovement.health.TakeDamage(10);
                break;
            }

            yield return new WaitForSeconds(spawnDelay);
        }
        
        playerMovement.canUsePowerup = true;
        playerMovement.ResetPositions();
    }
}
