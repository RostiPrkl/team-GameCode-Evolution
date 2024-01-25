using UnityEngine;

[CreateAssetMenu(fileName ="AttackScriptableObject", menuName ="Player/PlayerAttack")]
public class PlayerAttackScriptableObject : ScriptableObject
{
    [SerializeField] GameObject attackPrefab;
    public GameObject AttackPrefab { get => attackPrefab; private set => attackPrefab = value; }
    [SerializeField] float damage;
    public float Damage { get => damage; private set => damage = value; }
    [SerializeField] float speed;
    public float Speed { get => speed; private set => speed = value; }
    [SerializeField] float cooldown;
    public float Cooldown { get => cooldown; private set => cooldown = value; }
    [SerializeField] int penetrate;
    public int Penetrate { get => penetrate; private set => penetrate = value; }
    [SerializeField] int level;
    public int Level { get => level; private set => level = value; }
    //maybe not needed?
    [SerializeField] GameObject nextLevelPrefab;
    public GameObject NextLevelPrefab { get => nextLevelPrefab; private set => nextLevelPrefab = value; }
    [SerializeField] Sprite icon;
    public Sprite Icon { get => icon; private set => icon = value; }
    [SerializeField] string attackName;
    public string AttackName { get => attackName; private set => attackName = value; }
    [SerializeField] string attackDescription;
    public string AttackDescription { get => attackDescription; private set => attackDescription = value; }

}
