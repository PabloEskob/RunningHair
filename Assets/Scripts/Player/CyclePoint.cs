using UnityEngine;

[RequireComponent(typeof(CharacterJoint))]
public class CyclePoint : MonoBehaviour
{
    public void Join(Rigidbody hair)
    {
        var configurableJoint = GetComponent<CharacterJoint>();
        configurableJoint.connectedBody = hair;
    }

    public void IncreaseRadius(Vector3 position, float speed, float positionY)
    {
        float counter = 0;

        while (counter < speed)
        {
            counter += Time.fixedDeltaTime;
            Vector3 currentPos = transform.position;
            float time = Vector3.Distance(currentPos,
                new Vector3(position.x, positionY, position.z) / (speed - counter) * Time.deltaTime);
            transform.position = Vector3.MoveTowards(currentPos, new Vector3(position.x, positionY, position.z), time);
        }
    }

    public void SetWirlPool(Vector3 centr, float speed)
    {
        transform.RotateAround(centr, Vector3.up, speed * Time.deltaTime);
    }
}