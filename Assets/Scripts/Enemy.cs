using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public GameObject dropInst;

	public int startHealth = 3;
	public int damageInWall;
	private int health;

	public GameObject deathEffect;

	public Image healthBar;

	private bool isDead = false;
	private bool colForward = false;
	private RaycastHit hitinfo;

	public Type typeEnemy;

	public LayerMask layermask;

	public bool ingorePlayer = false;

	public Renderer model;

    public enum Type{
        Plastic,
        Organic,
        Metal,
		Boss
    }

    private void Start() {
		health = startHealth;
        healthBar.enabled = false;
	}

	private void Update() {
		Ray rayForward = new Ray (transform.position, transform.TransformDirection (Vector3.forward));
		if (Physics.Raycast (rayForward, out RaycastHit hitinfo, 3f, layermask)){
			Debug.DrawRay (transform.position, transform.TransformDirection (Vector3.forward) * hitinfo.distance, Color.red);
			colForward = true;
		}
		else{
			Debug.DrawRay (transform.position, transform.TransformDirection (Vector3.forward) * 3f, Color.green);
			colForward = false;
		}
		
	}

	public void TakeDamage (int amount)
	{
		FindObjectOfType<AudioManager>().PlaySound("EnemyDamage");
		if(model != null) StartCoroutine(Blink());
        healthBar.enabled = true;

		health -= amount;

		healthBar.fillAmount = (float)health / (float)startHealth;

		if (health <= 0 && !isDead)
		{
			Die();
		}

        StartCoroutine(TurnInvisible());
	}

    IEnumerator TurnInvisible(){
        yield return new WaitForSeconds(2f);
        healthBar.enabled = false;
    }

	void Die (bool dieShoot = true)
	{
		FindObjectOfType<AudioManager>().StopSound("BossSound"); 
		FindObjectOfType<AudioManager>().PlaySound("EnemyDeath");
		isDead = true;

        if(dieShoot && dropInst != null) Instantiate(dropInst,new Vector3(transform.position.x, 0.3f,transform.position.z), Quaternion.identity);

		if(deathEffect != null){
			 GameObject effect = (GameObject)Instantiate(deathEffect, transform.position, Quaternion.identity);
			Destroy(effect, 5f);
		}

		WaveSpawner.EnemiesAlive--;

		Destroy(this.gameObject);
    }
	
    void OnTriggerEnter(Collider col)
    {
        if(col.tag == "LastPath" ){
            Die(false);
        }
    }

	private void OnCollisionEnter(Collision col) {
		if(col.gameObject.tag == "Player" && colForward && !ingorePlayer){
			col.gameObject.transform.localScale = Vector3.zero;
			Collector.stun = true;
			FindObjectOfType<AudioManager>().PlaySound("Swallon");
			StartCoroutine(KnockBack(col.gameObject));
		}
	}

	IEnumerator KnockBack(GameObject obj){
        yield return new WaitForSeconds(1f);
		FindObjectOfType<AudioManager>().PlaySound("CollectorDamage");
		obj.transform.localScale = Vector3.one;
		obj.gameObject.GetComponent<Rigidbody>().AddForce(transform.forward * 10500);
		obj.GetComponent<Collector>().StartCoroutine("Blink");
		Collector.health -= 1;
		Collector.stun = false;
	}

	public IEnumerator Blink()
    {
      model.enabled = false;
      yield return new WaitForSeconds (0.12f);
      model.enabled = true;
      yield return new WaitForSeconds (0.24f);
      model.enabled = false;
      yield return new WaitForSeconds (0.36f);
      model.enabled = true;
      yield return new WaitForSeconds (0.24f);
      model.enabled = false;
      yield return new WaitForSeconds (0.36f);
      model.enabled = true;
   }
}
