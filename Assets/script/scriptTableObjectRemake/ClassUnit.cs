using UnityEngine;

public class ClassUnit : MonoBehaviour
{
    private ClassSkill skill;
    public UnitData unitData;
    

    // Biến trạng thái
    public int CurrentHp;
    public int CurrentSpeed;
    public int CurrentAtk;
    public int NumberBlock;
    public bool isSelected;

    // Biến gốc (nếu bạn cần giữ)
    public int Atk, Def, Hp, Charge, Speed, RangeAtk, Mass, MaxTurnSpeed, MaxTurnAtk, totalDame;
    private void Start()
    {
        if (unitData != null)
        {
            // Gán các chỉ số gốc
            Atk = unitData.Atk;
            Def = unitData.Def;
            Hp = unitData.Hp;
            Charge = unitData.Charge;
            Speed = unitData.Speed;
            RangeAtk = unitData.RangeAtk;
            Mass = unitData.Mass;
            MaxTurnSpeed = unitData.MaxTurnSpeed;
            MaxTurnAtk = unitData.MaxTurnAtk;

            // Gán chỉ số hiện tại (dùng khi chơi game)
            CurrentHp = Hp;
            CurrentSpeed = MaxTurnSpeed;
            CurrentAtk = MaxTurnAtk;
        }
    }


    public void Move(Vector3 target)
    {
        if (isSelected && CurrentSpeed > 0)
        {
            transform.position = target;
            CurrentSpeed--;
        }
    }

    public void Attack(GameObject target)
    {
        var unitTarget = target.GetComponent<ClassUnit>();
        if (CurrentAtk > 0)
        {
            var skills = gameObject.GetComponent<ClassSkill>();
            skills.TriggerEffect(gameObject, target);
            unitTarget.TakeDamage(TotalDame());
            if (unitTarget.CurrentHp > 0)
            {
                unitTarget.CounterAtk(this.gameObject);
            }

            CurrentAtk--;
        }
    }

    public void TakeDamage(int damage)
    {
        int reduced = Mathf.RoundToInt(damage * DefenseRate());
        int finalDame = damage - reduced;
        CurrentHp -= finalDame;

        if (CurrentHp <= 0)
        {
            Destroy(gameObject); // chết thì xóa
        }
    }

    public void CounterAtk(GameObject target)
    {
        ClassUnit unitTarget = target.GetComponent<ClassUnit>();
        var skills = target.GetComponent<ClassSkill>();
        skills.TriggerEffect(target, gameObject);
        unitTarget.TakeDamage(CounterDame());
    }

    public void ResetTurn()
    {
        CurrentSpeed = MaxTurnSpeed;
        CurrentAtk = MaxTurnAtk;
        NumberBlock = 0;
    }

    //=============================Tính Đame====================//
    public int TotalDame()
    {
        totalDame += Atk + ChargeDame();
        return totalDame;
    }

    public float DefenseRate()
    {
        return Def / (float)(Def + 100);
    }

    public int ChargeDame()
    {
        return (Charge + Mass) * NumberBlock;
    }

    public int CounterDame()
    {
        return Mathf.RoundToInt(Atk * 0.5f + Mass);
    }
}