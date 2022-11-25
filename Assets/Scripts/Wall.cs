using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Wall : MonoBehaviour
{
    public int startHealth = 20;
	public Image healthBar;
    public Text healthText;
    public bool isDestroy;
    public List<GameObject> objEnemy = new List<GameObject>();

	public static int health;

    private int valueDamage;

    void Start()
    {
        health = startHealth;
    }

    private void Update() {
        healthText.text = health+"/"+startHealth;
        objEnemy.RemoveAll(s => s == null);
        if(objEnemy.Count == 0){
            CancelInvoke();
        }
        healthBar.fillAmount = (float)health / (float)startHealth;
    }

    void Die(){
        isDestroy = true;
        Destroy(this.gameObject);
    }

    public void TakeDamage(){
        health -= valueDamage;

        if (health <= 0 && !isDestroy)
		{
			Die();
		}
    }

    private void OnCollisionEnter(Collision col) {
        if(col.gameObject.tag == "Monster"){
            objEnemy.Add(col.gameObject);
            valueDamage = col.gameObject.GetComponent<Enemy>().damageInWall;
            InvokeRepeating("TakeDamage", 0.1f, 1f);
        }
    }
    private void OnCollisionExit(Collision col) {
        if(col.gameObject.tag == "Monster"){
            CancelInvoke();
        }
    }

}
