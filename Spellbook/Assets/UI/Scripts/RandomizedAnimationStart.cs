using UnityEngine;

[RequireComponent(typeof(Animator))]
public class RandomizedAnimationStart : MonoBehaviour {

    // Public Fields
    public string animationName;

    void Start() {
        GetComponent<Animator>().Play(animationName, -1, Random.Range(0.0F, 1.0F));
    }
}
