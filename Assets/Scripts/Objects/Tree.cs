using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour
{
    public int HP = 30;
    Animation reaction_anim;
    // Start is called before the first frame update
    void Start()
    {
        reaction_anim = this.GetComponent<Animation>();
    }

    public void Hit()
    {
        HP--;
        reaction_anim.GetComponent<Animation>().Play();
        //
        if (HP <= 0)
        {
            Destroy(this.gameObject);
            GameObject prefab = Resources.Load<GameObject>("Wood");
            if (prefab != null)
            {
                GameObject newItem = Instantiate(prefab, this.transform.position + new Vector3(0.0f,2.0f,0.0f), Quaternion.identity);
            }
        }
    }
}
