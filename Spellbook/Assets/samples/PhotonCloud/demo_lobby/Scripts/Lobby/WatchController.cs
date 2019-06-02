using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WatchController : MonoBehaviour
{
    public void PlayTickSound()
    {
        SoundManager.instance.PlaySingle(SoundManager.tick);
    }
}
