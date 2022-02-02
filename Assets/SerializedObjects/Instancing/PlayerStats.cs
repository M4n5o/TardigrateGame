using UnityEngine;


[CreateAssetMenu(fileName = "PlayerStats", menuName = "ScriptableObjects/PlayerStats", order = 1)]
public class PlayerStats : ScriptableObject
{
    
    public float speed { get { return _speed; } set { _speed = value; } }
    [SerializeField]
    private float _speed;

    public float jumpHeight { get { return _jumpHeight; } set { _jumpHeight = value; } }
    [SerializeField]
    private float _jumpHeight;

    public float shotSpeed { get { return _shotSpeed; } set { _shotSpeed = value; } }
    [SerializeField]
    private float _shotSpeed;

    public float damage { get { return _damage; } set { _damage = value; } }
    [SerializeField]
    private float _damage;

}
