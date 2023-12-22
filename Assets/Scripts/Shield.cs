using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        EnnemyAI _e = other.GetComponent<EnnemyAI>();
        if (_e)
        {
            float original_speed = _e.get_speed();
            _e.set_pos(new UnityEngine.Vector3(_e.x() - Random.Range(-1.0f, 1.0f), _e.y(), _e.z() - 1.0f));
            _e.set_speed(original_speed);
        }
    }
}
