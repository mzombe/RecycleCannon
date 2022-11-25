using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cannon : MonoBehaviour
{
    public FixedJoystick fixedJoystick;

	public GameObject bulletPrefabPlastic, bulletPrefabOrganic, bulletPrefabMetal;

	public Transform firePoint;
	public Transform targetPoint;
	public Transform tubePoint;

    public int munitionOrganic = 0;
    public int munitionPlastic = 0;
    public int munitionMetal = 0;

    public Text munitionOrganicText;
    public Text munitionPlasticText;
    public Text munitionMetalText;

    private Transform target;
    private GameObject bulletPlastic, bulletOrganic, bulletMetal;
    private float down = 0;

    public Type typeMunition;

    public enum Type{
        Organic,
        Plastic,
        Metal
    }

    public void ChangeToOrganic(){typeMunition = Type.Organic;}
    public void ChangeToPlastic(){typeMunition = Type.Plastic;}
    public void ChangeToMetal(){typeMunition = Type.Metal;}

    void FixedUpdate () {

        munitionOrganicText.text = munitionOrganic.ToString(); 
        munitionPlasticText.text = munitionPlastic.ToString(); 
        munitionMetalText.text = munitionMetal.ToString(); 
            
        if((int)typeMunition == 0 ){
            munitionOrganicText.color = Color.green;
        }else{
            munitionOrganicText.color = Color.white;
        }
        if((int)typeMunition == 1){
            munitionPlasticText.color = Color.green;
        }else{
            munitionPlasticText.color = Color.white;
        }
        if((int)typeMunition == 2 ){
            munitionMetalText.color = Color.green;
        }else{
            munitionMetalText.color = Color.white;
        }
                    

        if(munitionPlastic > 0 || munitionOrganic > 0 || munitionMetal > 0 ){
            LockOnTarget();
            fixedJoystick.enabled = true;

            if(fixedJoystick.Horizontal != 0f ) {
                transform.localEulerAngles = new Vector3(down,Mathf.PingPong(-55, 55) * fixedJoystick.Horizontal,transform.rotation.z);
            }

            if(fixedJoystick.press){
                if(bulletPlastic == null && bulletOrganic == null && bulletMetal == null){
                    if((int)typeMunition == 2 && munitionMetal != 0){
                        FindObjectOfType<AudioManager>().PlaySound("Cannon");
                        ShootMetal();
                    }
                    
                    if((int)typeMunition == 0 && munitionOrganic != 0){
                        FindObjectOfType<AudioManager>().PlaySound("Cannon");
                        ShootOrganic();
                    }
                    
                    if((int)typeMunition == 1 && munitionPlastic != 0){
                        FindObjectOfType<AudioManager>().PlaySound("Cannon");
                        ShootPlastic();
                    }
                }
            }
        }else{
            fixedJoystick.enabled = false;
        }

        UpdateTarget();
	}

	void ShootPlastic ()
	{
		bulletPlastic = (GameObject)Instantiate(bulletPrefabPlastic, firePoint.position, Quaternion.identity);
        bulletPlastic.GetComponent<Rigidbody>().velocity = targetPoint.forward * 2500f * Time.deltaTime;
        munitionPlastic--;
	}
	void ShootMetal ()
	{
		bulletMetal = (GameObject)Instantiate(bulletPrefabMetal, firePoint.position, Quaternion.identity);
        bulletMetal.GetComponent<Rigidbody>().velocity = targetPoint.forward * 2500f * Time.deltaTime;
        munitionMetal--;
	}
	void ShootOrganic ()
	{
		bulletOrganic = (GameObject)Instantiate(bulletPrefabOrganic, firePoint.position, Quaternion.identity);
        bulletOrganic.GetComponent<Rigidbody>().velocity = targetPoint.forward * 2500f * Time.deltaTime;
        munitionOrganic--;
	}

    void LockOnTarget ()
	{
		if(target != null){
            Vector3 dir = target.position - transform.position;
            tubePoint.rotation = new Quaternion(Quaternion.LookRotation(dir).x,transform.rotation.y,0,1);
        }else{
            tubePoint.rotation = new Quaternion(0,transform.rotation.y,0,1);
        }
	}

    void UpdateTarget ()
	{
		GameObject[] enemies = GameObject.FindGameObjectsWithTag("Monster");
		float shortestDistance = Mathf.Infinity;
		GameObject nearestEnemy = null;
		foreach (GameObject enemy in enemies)
		{
			float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
			if (distanceToEnemy < shortestDistance)
			{
				shortestDistance = distanceToEnemy;
				nearestEnemy = enemy;
			}
		}

		if (nearestEnemy != null && shortestDistance <= 30f)
		{
			target = nearestEnemy.transform;
		} else
		{
			target = null;
		}

	}

}
