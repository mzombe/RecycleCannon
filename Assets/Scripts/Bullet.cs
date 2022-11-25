using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
	//public GameObject impactEffect;
	
    public Type typeBullet;
    public int damage;

    public enum Type{
        Plastic,
        Organic,
        Metal
    }

    private void Start() {
        StartCoroutine(Destroy());
    }
	
	// GameObject effectIns = (GameObject)Instantiate(impactEffect, transform.position, transform.rotation);
	// Destroy(effectIns, 5f);

    IEnumerator Destroy()
	{
		yield return new WaitForSeconds(2f);
        Destroy(this.gameObject);
    }

    void OnCollisionEnter(Collision col)
    {   
        if(col.gameObject.tag == "Monster"){
            Enemy hitEnemy = col.gameObject.GetComponent<Enemy>();
            switch(typeBullet)
            {
            case Type.Plastic:
                if((int)hitEnemy.typeEnemy == 1){
                    hitEnemy.TakeDamage(damage);
                }
            break;

            case Type.Organic:
                if((int)hitEnemy.typeEnemy == 0 || (int)hitEnemy.typeEnemy == 2){
                    hitEnemy.TakeDamage(damage);
                }
            break;

            case Type.Metal:
                if((int)hitEnemy.typeEnemy == 1){
                    hitEnemy.TakeDamage(damage);
                }
            break;
            }
            
            if((int)hitEnemy.typeEnemy == 3){
                hitEnemy.TakeDamage(damage);
            }
            
        }    
        
        if(col.gameObject.tag != "Player") Destroy(this.gameObject);
    }           
        
}
