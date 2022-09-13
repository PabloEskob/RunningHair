using UnityEngine;

public class Volos : MonoBehaviour
{
    private Transform _growthPoint;
    private Rigidbody _rigidbody;

    public Transform VolosEnd { get; private set; }
    public int SegmentNumber { get; private set; }
    public int PositionNumber { get; private set; }
    public bool IsColored { get; private set; }

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        VolosEnd = GetComponentInChildren<GrowthPosition>().transform;
    }

    private void FixedUpdate()
    {
        _rigidbody.AddForce(new Vector3(0, 0.9f, 0), ForceMode.VelocityChange);
    }

    public void SetSegmentNumber(int segmentNumber)
    {
        SegmentNumber = segmentNumber;
    }

    public void SetPositionNumber(int positionNumber)
    {
        PositionNumber = positionNumber;
    }

    public void Init(Transform growthPosition)
    {
        _growthPoint = growthPosition;
    }

    public void Colorise(Material material)
    {
        IsColored = true;
        GetComponent<SkinnedMeshRenderer>().material = material;
    }
}
