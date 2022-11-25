using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    public Type typeCollectable;
    public int value;

    public enum Type{
        Plastic,
        Organic,
        Metal
    }

    void OnTriggerEnter(Collider col)
    {
        if(col.tag == "Player"){
            Collector collector = col.GetComponent<Collector>();

            switch(typeCollectable)
            {
            case Type.Plastic:
                FindObjectOfType<AudioManager>().PlaySound("Collect");
                collector.munitionPlastic += value;
                collector.allMunition += value;
            break;

            case Type.Organic:
                FindObjectOfType<AudioManager>().PlaySound("Collect");
                collector.munitionOrganic += value;
                collector.allMunition += value;
            break;

            case Type.Metal:
                FindObjectOfType<AudioManager>().PlaySound("Collect");
                collector.munitionMetal += value;
                collector.allMunition += value;
            break;
            }

            Destroy(this.gameObject);
        }
    }

}
