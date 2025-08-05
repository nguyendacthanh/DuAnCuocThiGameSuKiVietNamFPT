using UnityEngine;

public class ClassUnit : MonoBehaviour
{
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

    public void Attack(ClassUnit target)
    {
        if (CurrentAtk > 0)
        {
            target.TakeDamage(TotalDame());
            if (target.CurrentHp > 0)
            {
                target.CounterAtk(this);
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

    public void CounterAtk(ClassUnit target)
    {
        target.TakeDamage(CounterDame());
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
        return totalDame = Atk + ChargeDame();
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