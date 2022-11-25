using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Collector : MonoBehaviour
{
    public float speed;
    public FixedJoystick fixedJoystick;

    public int munitionPlastic = 0;
    public int munitionMetal = 0;
    public int munitionOrganic = 0;

    public Text allMunitionText;
    public int allMunition;

    public Animator anim;

    public static bool stun = false;

    public int startHealth = 3;
	public Image healthBar;
    public Text healthText;

    public static int health;

    public Renderer modelPlayer;

    private Rigidbody rig;

    private void Start() {
        health = startHealth;
        rig = GetComponent<Rigidbody>();
    }

    public void FixedUpdate()
    {
        allMunitionText.text = allMunition.ToString();

        Vector3 dir = Vector3.forward * fixedJoystick.Vertical + Vector3.right * fixedJoystick.Horizontal;
        if(dir != Vector3.zero && !stun){
            anim.SetBool("Walk", true);
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), 0.15f);
            anim.speed = dir.magnitude;
            rig.velocity = dir * speed;            
        }else{
            anim.SetBool("Walk", false);
            rig.velocity = Vector3.zero;
        }       

        healthBar.fillAmount = (float)health / (float)startHealth; 
        healthText.text = health+"/"+startHealth;

        if (health <= 0)
		{
			Destroy(this.gameObject);
		}
    } 

    private void OnCollisionExit(Collision col) {
       if(col.gameObject.tag == "Monster"){
            stun = false;
       } 
    }

    public IEnumerator Blink()
    {
      modelPlayer.enabled = false;
      yield return new WaitForSeconds (0.12f);
      modelPlayer.enabled = true;
      yield return new WaitForSeconds (0.24f);
      modelPlayer.enabled = false;
      yield return new WaitForSeconds (0.36f);
      modelPlayer.enabled = true;
      yield return new WaitForSeconds (0.24f);
      modelPlayer.enabled = false;
      yield return new WaitForSeconds (0.36f);
      modelPlayer.enabled = true;
   }

}
