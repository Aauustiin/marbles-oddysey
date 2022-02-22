//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//
//public class BonkPlayer : MonoBehaviour
//{
//    [SerializeField] float bonkMagnitude;
//
//    void OnCollisionEnter(Collision collisionInfo)
//    {
//        if (collisionInfo.collider.gameObject.layer == LayerMask.NameToLayer("Player"))
//        {
//            Vector3 bonkDirection = collisionInfo.transform.position - transform.position;
//            collisionInfo.rigidbody.AddForce((bonkDirection.normalized + transform.up * 0.5f) * bonkMagnitude, ForceMode.Impulse);
//        }
//    }
//}
