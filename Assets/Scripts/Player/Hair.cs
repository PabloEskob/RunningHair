using UnityEngine;

[RequireComponent(typeof(CharacterJoint), typeof(Rigidbody))]
public class Hair : MonoBehaviour
{
    [SerializeField] private GameObject _hairUp;
    [SerializeField] private HairTip _hairTip;
    [SerializeField] private float _minScale;
    [SerializeField] private float _maxScale;
    [SerializeField] private float _forse;
    [SerializeField] private float _rotateValue;

    private int _number;
    private int _squadNumber;
    private Rigidbody _rigidbody;
    private HairTip _hairTips;

    public GameObject HairUp => _hairUp;
    public HairTip HairTip => _hairTips;


    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        AssighIntialScale();
        CreateHairTip();
    }

    private void FixedUpdate()
    {
        if (AllowUseForce(true))
        {
            _rigidbody.AddForce(new Vector3(0, _forse, 0), ForceMode.VelocityChange);
        }
    }

    public void DestroyHairTip()
    {
        Destroy(_hairTips.gameObject);
    }

    public Quaternion AssighIntialRotation()
    {
        var transformRotate = transform.rotation;
        transformRotate.x = Random.Range(-_rotateValue, _rotateValue);
        return transformRotate;
    }

    public void Uint(int squadNumber, int number)
    {
        _squadNumber = squadNumber;
        _number = number;
    }

    public int SetSquadNumber()
    {
        return _squadNumber;
    }

    public int SetNumber()
    {
        return _number;
    }

    public void Join(Rigidbody hair)
    {
        var configurableJoint = GetComponent<CharacterJoint>();
        configurableJoint.connectedBody = hair;
    }

    public bool AllowUseForce(bool allow)
    {
        return allow;
    }

    public void ReduceWeightRigidbody(int maxMass)
    {
        _rigidbody.mass = maxMass;
    }
    
    public void ChangeMaterial(Material material)
    {
        GetComponent<Renderer>().material = material;
        _hairTips.SetColorMaterial(material);
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

    public Hair FindHair(int squareNumber, int number)
    {
        if (_squadNumber == squareNumber && _number == number)
        {
            return this;
        }
        return null;
    }
}