using UnityEngine;
public class Health 
{
    int maxHealth;
    int currentHealth;
    private HealthBar healthbar;
    public Health(int maxHealth, HealthBar healthbar){
        this.maxHealth = maxHealth;
        this.healthbar = healthbar;
        currentHealth = maxHealth;
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
    void Die(){
        Debug.Log("Dead");
    } 
}
