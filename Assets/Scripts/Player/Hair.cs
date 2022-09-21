using UnityEngine;

[RequireComponent(typeof(CharacterJoint),typeof(Rigidbody))]
public class Hair : MonoBehaviour
{
    [SerializeField] private GameObject _hairUp;
    [SerializeField] private HairTip _hairTip;
    [SerializeField] private float _minScale;
    [SerializeField] private float _maxScale;
    [SerializeField] private float _forse;
    [SerializeField] private float _rotateValue;

    private Rigidbody _rigidbody;
    private HairTip _hairTips;

    public GameObject hairUp => _hairUp;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        AssighIntialRotation();
        AssighIntialScale();
        CreateHairTip();
    }

    private void FixedUpdate()
    {
        _rigidbody.AddForce(new Vector3(0, _forse, 0), ForceMode.VelocityChange);
    }
    
    public void DestroyHairTip()
    {
        Destroy(_hairTips.gameObject);
    }

    public void Join(Rigidbody hair)
    {
        var configurableJoint = GetComponent<CharacterJoint>();
        configurableJoint.connectedBody = hair;
    }
    
    public void ReduceWeightRigidbody(int maxMass)
    {
        _rigidbody.mass = maxMass;
    }

    private void CreateHairTip()
    {
        _hairTips = Instantiate(_hairTip, _hairUp.transform.position, Quaternion.identity);
        _hairTips.transform.parent = _hairUp.transform;
    }
    

    private void AssighIntialScale()
    {
        var transformLocalScale = transform.localScale;
        transformLocalScale.y = Random.Range(_minScale, _maxScale);
        transform.localScale = transformLocalScale;
    }

    private void AssighIntialRotation()
    {
        var transfomRotate = transform.rotation;
        transfomRotate.x = Random.Range(-_rotateValue, _rotateValue);
        transform.rotation = transfomRotate;
    }
}