using UnityEngine;
using System.Collections;
namespace Shooter{
public class DestroyDelay : MonoBehaviour {
    
    public float time = 15.0f;

    void Start() {
        Destroy(this.gameObject, time);
    }
}
}