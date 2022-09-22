using System.Collections.Generic;
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
        _rigidbody.AddForce(new Vector3(0, _forse, 0), ForceMode.VelocityChange);
    }
    
    public void DestroyHairTip()
    {
        Destroy(_hairTips.gameObject);
    }
    
    public Quaternion AssighIntialRotation()
    {
        var transfomRotate = transform.rotation;
        transfomRotate.x = Random.Range(-_rotateValue, _rotateValue);
        return transfomRotate;
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

    public void SetColorMaterial(Material material)
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
}