using UnityEngine;

public class UfoBehaviour : BaseEnemy<UfoPool>
{
    [SerializeField] private float speed = default;
    [field:SerializeField] public override int CountScoreByDefeat { get; set; } = default;

    protected override void OnUpdate()
    {
        Vector2 posPlayer = ChController.gameObject.transform.position;
        var targetDir = posPlayer - (Vector2)transform.position;
        targetDir.Normalize();
        transform.Translate(targetDir * speed * Time.deltaTime, Space.World);
    }
}
