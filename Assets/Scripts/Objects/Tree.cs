using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour
{
	public int HP = 30;
	Animation reaction_anim;
    // Start is called before the first frame update
    void Awake()
    {
        reaction_anim = GetComponent<Animation>();
    }

    public void Hit()
	{
		HP--;
		reaction_anim.GetComponent<Animation>().Play();
		//
		if(HP <= 0)
		{
			Destroy(this.gameObject);
		}
	}
}
