
using Cinemachine;
using UnityEngine;

public class SwitchConfinderBoundingShape : MonoBehaviour
{
    public
    // Start is called before the first frame update
    void Start()
    {
        SwitchBoundShape();
    }

    // Update is called once per frame


    private void SwitchBoundShape()
    {
        PolygonCollider2D polygonCollider2D = GameObject.FindWithTag(Tags.BoundsConfinder).GetComponent<PolygonCollider2D>();
        CinemachineConfiner cinemachineConfiner = GetComponent<CinemachineConfiner>();
        cinemachineConfiner.m_BoundingShape2D = polygonCollider2D;
        cinemachineConfiner.InvalidatePathCache();
    }
}
