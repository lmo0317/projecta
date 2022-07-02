using UnityEngine;

public partial class MonsterCtrl : MonoBehaviour
{
    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, traceDist);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackDist);
    }
}
