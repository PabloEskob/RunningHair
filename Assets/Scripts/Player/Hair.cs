using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(CharacterJoint))]
public class Hair : MonoBehaviour
{
    [SerializeField] private GameObject _hairUp;
    [SerializeField] private HairTip _hairTip;
    [SerializeField] private Vector3 _offset;
    
    private Rigidbody _rigidbody;
    private HairTip _hairTips;

    public GameObject hairUp => _hairUp;

    private void Start()
    {
        CreateHairTip();
    }

    private void CreateHairTip()
    {
        _hairTips = Instantiate(_hairTip, _hairUp.transform.position + _offset, Quaternion.identity);
        _hairTips.transform.parent = _hairUp.transform;
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

    public void PutExactly()
    {
        transform.DORotate(new Vector3(0, 0, 0), 5);
    }
}