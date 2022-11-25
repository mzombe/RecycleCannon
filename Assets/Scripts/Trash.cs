using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trash : MonoBehaviour
{

    public Type typeTrash;

    private Collector collector;
    private Cannon cannon;

    public enum Type{
        Plastic,
        Organic,
        Metal
    }

    private void Start() {
        cannon = GameObject.FindGameObjectWithTag("Cannon").GetComponent<Cannon>();;
    }

    void Update()
    {
        switch(typeTrash)
            {
            case Type.Plastic:
                if(collector != null && collector.fixedJoystick.press){
                    if(collector.munitionPlastic > 0){
                        FindObjectOfType<AudioManager>().PlaySound("Trash");
                        int i = collector.munitionPlastic;
                        collector.allMunition -= i;
                        cannon.munitionPlastic += i;
                        collector.munitionPlastic = 0;
                    }
                }
            break;

            case Type.Organic:
                if(collector != null && collector.fixedJoystick.press){
                    if(collector.munitionOrganic > 0){
                        FindObjectOfType<AudioManager>().PlaySound("Trash");
                        int i = collector.munitionOrganic;
                        collector.allMunition -= i;
                        cannon.munitionOrganic += i;
                        collector.munitionOrganic = 0;
                    }
                }
            break;

            case Type.Metal:
                if(collector != null && collector.fixedJoystick.press){
                    if(collector.munitionMetal > 0){
                        FindObjectOfType<AudioManager>().PlaySound("Trash");
                        int i = collector.munitionMetal;
                        collector.allMunition -= i;
                        cannon.munitionMetal += i;
                        collector.munitionMetal = 0;
                    }
                }
            break;
            }
    }

    private void OnTriggerEnter(Collider col) {
        if(col.tag == "Player"){
            collector = col.GetComponent<Collector>();
        }
    }
    private void OnTriggerExit(Collider col) {
        if(col.tag == "Player"){
            collector = null;
        }
    }
}
