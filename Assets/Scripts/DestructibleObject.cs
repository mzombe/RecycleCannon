using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructibleObject : MonoBehaviour {

    public Transform pfObjectBroken;
    //public Transform vfxSmoke;
    //public int vfxAmount = 5;
    public float explosionForce = 200f;

    private Vector3 lastDamagePosition;

    private void HealthSystem_OnDead(object sender, System.EventArgs e) {

        // Instantiate(vfxSmoke, lastDamagePosition, Quaternion.identity);
        // for (int i = 1; i < vfxAmount; i++) {
        //     Instantiate(vfxSmoke, lastDamagePosition + UtilsClass.GetRandomDir() * 2f, Quaternion.identity);
        // }

        Transform wallBrokenTransform = Instantiate(pfObjectBroken, transform.position, transform.rotation);
        foreach (Transform child in wallBrokenTransform) {
            if (child.TryGetComponent<Rigidbody>(out Rigidbody childRigidbody)) {
                childRigidbody.AddExplosionForce(200f, lastDamagePosition, 10f);
            }
        }

        Destroy(gameObject);
    }

}