using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtenPressSound : MonoBehaviour
{
    [SerializeField] private AudioClip buttenPressSound;
    // Start is called before the first frame update
    public void ButtenSound()
    {
        AudioController.instance.PlayClip(AudioController.instance.button);
    }
}
