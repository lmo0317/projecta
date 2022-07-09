using UnityEngine;

public class MonsterGizmoComponent : MonoBehaviour
{
    private Monster _monster;

    private void Start()
    {
        _monster = GetComponent<Monster>();
    }

    void OnDrawGizmos()
    {
        if (_monster)
        {

            Gizmos.color = Color.blue;
            var traceDist = _monster.traceDist;
            Gizmos.DrawWireSphere(transform.position, traceDist);

            Gizmos.color = Color.red;
            var attackDist = _monster.attackDist;
            Gizmos.DrawWireSphere(transform.position, attackDist);
        }
    }
}
