using System.Collections;
using UnityEngine;

public class ClassUnit : MonoBehaviour
{
    private ClassSkill skill;
    public UnitData unitData;
    private float time = 0.1f;

    // Biến trạng thái
    public int CurrentHp;
    public int CurrentSpeed;
    public int CurrentAtk;
    public int NumberBlock;
    public bool isSelected;
    public string BranchUnit,NameUnit,TypeUnit;
    // Biến gốc (nếu bạn cần giữ)
    public int Atk, Def, Hp, Charge, Speed, RangeAtk, Mass, MaxTurnSpeed, MaxTurnAtk, totalDame=0;
    private void Start()
    {
        if (unitData != null)
        {
            // Gán các chỉ số gốc
            NameUnit = unitData.NameArmy;
            Atk = unitData.Atk;
            Def = unitData.Def;
            Hp = unitData.Hp;
            Charge = unitData.Charge;
            Speed = unitData.Speed;
            RangeAtk = unitData.RangeAtk;
            Mass = unitData.Mass;
            BranchUnit = unitData.BranchArmy;
            TypeUnit = unitData.TypeArmy;
            MaxTurnSpeed = unitData.MaxTurnSpeed;
            MaxTurnAtk = unitData.MaxTurnAtk;
            
            // Gán chỉ số hiện tại (dùng khi chơi game)
            CurrentHp = Hp;
            CurrentSpeed = MaxTurnSpeed;
            CurrentAtk = MaxTurnAtk;
        }

        skill = GetComponent<ClassSkill>();
    }


    public void Move(Vector3 target)
    {
        if (CurrentSpeed > 0)
        {
            StartCoroutine(MoveRule(transform.position, target));
            CurrentSpeed--;
        }
    }

    public void Attack(GameObject target)
    {
        var unitTarget = target.GetComponent<ClassUnit>();
        if (CurrentAtk > 0)
        {
            CurrentSpeed--;
            // var skills = GetComponent<ClassSkill>();
            if (skill != null)
            {
                skill.TriggerEffect(gameObject, target);
            }
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
        // var skills = gameObject.GetComponent<ClassSkill>();
        if (skill != null)
        {
            skill.TriggerEffect(gameObject, target);
        }
        unitTarget.TakeDamage(CounterDame());
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
        totalDame += Mathf.RoundToInt(Atk * 0.5f + Mass);
        return totalDame;
    }
    public void ResetLuot()
    {
        CurrentSpeed = 0;
        CurrentAtk = 0;
        NumberBlock = 0;
    }

    public void KhoiPhucLuot()
    {
        CurrentSpeed = MaxTurnSpeed;
        CurrentAtk = MaxTurnAtk;
    }
    private IEnumerator MoveRule(Vector3 StartPosition, Vector3 target)
    {
        Vector3 currentPos = StartPosition;
        int buocX = Mathf.RoundToInt((target.x - StartPosition.x) / 100f);
        int buocY = Mathf.RoundToInt((target.y - StartPosition.y) / 100f);
        int stepX = (int)Mathf.Sign(buocX);
        int stepY = (int)Mathf.Sign(buocY);

        for (int i = 0; i < Mathf.Abs(buocX); i++)
        {
            currentPos += new Vector3(stepX * 100f, 0, 0);
            transform.position = currentPos;
            yield return new WaitForSeconds(time);
            NumberBlock++;
        }

        for (int i = 0; i < Mathf.Abs(buocY); i++)
        {
            currentPos += new Vector3(0, stepY * 100f, 0);
            transform.position = currentPos;
            yield return new WaitForSeconds(time);
            NumberBlock++;
        }

        
    }
}