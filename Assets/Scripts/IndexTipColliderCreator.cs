using UnityEngine;

public class IndexTipColliderCreator : MonoBehaviour
{
    [SerializeField] private string m_nameToTrigger;

    [SerializeField] private GameObject m_colliderPrefab;
    [SerializeField] private Transform m_parent;

    private void Start()
    {
        if (name != m_nameToTrigger)
        {
            return;
        }

        GameObject collider = Instantiate(m_colliderPrefab);
        collider.transform.SetParent(m_parent);
    }
}
