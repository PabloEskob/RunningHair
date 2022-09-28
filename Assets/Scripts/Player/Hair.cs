using UnityEngine;

[RequireComponent(typeof(CharacterJoint), typeof(Rigidbody))]
public class Hair : MonoBehaviour
{
    [SerializeField] private HairPoint _hairUp;
    [SerializeField] private HairTip _hairTip;
    [SerializeField] private float _minScale;
    [SerializeField] private float _maxScale;
    [SerializeField] private float _forse;
    [SerializeField] private float _rotateValue;
    [SerializeField] private HairColor[] _hairColors;

    private int _number;
    private int _squadNumber;
    private Rigidbody _rigidbody;
    private HairTip _targetHairTip;
    private Renderer _renderer;

    public Renderer Renderer => _renderer;
    public HairPoint HairUp => _hairUp;
    public int SquadNumber => _squadNumber;
    public int Number => _number;
    public HairTip TargetHairTip => _targetHairTip;


    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _renderer = GetComponent<Renderer>();
    }

    private void Start()
    {
        AssighIntialScale();
        CreateHairTip();
    }

    private void FixedUpdate()
    {
        _rigidbody.AddForce(new Vector3(0, _forse, 0), ForceMode.VelocityChange);
    }

    public void DestroyHairTip()
    {
        Destroy(_targetHairTip.gameObject);
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

    public void Join(Rigidbody hair)
    {
        var configurableJoint = GetComponent<CharacterJoint>();
        configurableJoint.connectedBody = hair;
    }
    
    public void ReduceWeightRigidbody(int maxMass)
    {
        _rigidbody.mass = maxMass;
    }

    public void ChangeMaterial(Material material)
    {
        _renderer.material = material;
        _targetHairTip.SetNewMaterial(material);
    }

    public void OnEnableMaterial(int numberColor)
    {
        foreach (var hairColor in _hairColors)
        {
            _hairColors[numberColor].OnEnableColor();
        }
    }

    private void CreateHairTip()
    {
        _targetHairTip = Instantiate(_hairTip, _hairUp.transform.position, Quaternion.identity);
        _targetHairTip.transform.parent = _hairUp.transform;
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