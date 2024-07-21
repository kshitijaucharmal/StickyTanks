using Unity.VisualScripting;
using UnityEngine;
public class Health 
{
    int maxHealth;
    int currentHealth;
    GameObject player;
    private HealthBar healthbar;
    public bool isDead = false;
    public Health(int maxHealth, HealthBar healthbar, GameObject player)
    {
        this.maxHealth = maxHealth;
        this.healthbar = healthbar;
        this.player = player;   
        currentHealth = maxHealth;
    }

    void Start()
    {
        PlayerMovement movement = player.GetComponent<PlayerMovement>();
    }
    public int GetHealth(){
        return currentHealth;
    }
    public void TakeDamage(int damage){
        currentHealth -= damage;
        healthbar.SetHealth(currentHealth);
        Debug.Log("currentHealth = "+currentHealth);
        if (currentHealth <= 0){
            Die();
        }
        
    }
    public void Heal(int heal){
        currentHealth += heal;
        Debug.Log("currentHealth = "+currentHealth);
        if (currentHealth > maxHealth){
            currentHealth = maxHealth;
        }
        healthbar.SetHealth(currentHealth);
    }
    public void Die(){
        Debug.Log(player.name+ " is Dead");
        Object.Destroy(player);
        isDead = true;
        GameManager.Instance.CheckWinner();
    }
}
