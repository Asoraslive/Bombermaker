using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class looseScreen : MonoBehaviour
{
   
    public Animator anim;
 
   public void looseGame()
    {
        anim.SetBool("Loose",true);
    }
   
}
