using System.Collections;
using UnityEngine;

public class ClassUnit : MonoBehaviour
{
    private ClassSkill skill;
    public UnitData unitData;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private float time = 0.1f;
    // public GameObject currentTargetCity = null;
    // Biến trạng thái
    public int CurrentHp;
    public int CurrentSpeed;
    public int CurrentAtk;
    public int NumberBlock;
    public bool isSelected;
    public string BranchUnit,NameUnit,TypeUnit;
    // Biến gốc (nếu bạn cần giữ)
    public int Atk, Def, Hp, Charge, Speed, RangeAtk, Mass, MaxTurnSpeed, MaxTurnAtk, totalDame;
    private void Start()
    {
        animator =gameObject.GetComponent<Animator>();
        spriteRenderer =gameObject.GetComponent<SpriteRenderer>();
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


    public void Move(Vector3 target, System.Action onComplete = null) 
    {
        if (CurrentSpeed > 0)
        {
            StartCoroutine(MoveRule(transform.position, target, onComplete));
            CurrentSpeed--;
        }
        StartCoroutine(delaytime());
    }
    private IEnumerator MoveRule(Vector3 StartPosition, Vector3 target, System.Action onComplete)
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

        // ✅ Gọi callback nếu có
        onComplete?.Invoke();
    }

    public IEnumerator delaytime()
    {
        yield return new WaitForSeconds(time);
    }

    public void Attack(GameObject target)
    {
        if (CheckFlipx(target))
        {
            spriteRenderer.flipX = true;
            animator.SetTrigger("Counter");

        }
        else
        {
            spriteRenderer.flipX = false;
            animator.SetTrigger("Atk");
        }
        var unitTarget = target.GetComponent<ClassUnit>();
        if (CurrentAtk > 0)
        {
            CurrentSpeed--;
            CurrentAtk--;
            // var skills = GetComponent<ClassSkill>();
            if (skill != null)
            {
                skill.TriggerEffect(gameObject, target);
            }
            unitTarget.TakeDamage(TotalDame());
            if (unitTarget.CurrentHp > 0 &&  unitTarget.isEnemyInCounterAtkRange(gameObject)==true)
            {
                unitTarget.CounterAtk(this.gameObject);
            }
        }
    }

    public void TakeDamage(int damage)
    {
        int reduced = Mathf.RoundToInt(damage * DefenseRate());
        int finalDame = damage - reduced;
        CurrentHp -= finalDame;

        if (CurrentHp <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void CounterAtk(GameObject target)
    {
        if (CheckFlipx(target))
        {
            spriteRenderer.flipX = true;
            animator.SetTrigger("Counter");

        }
        else
        {
            spriteRenderer.flipX = false;
            animator.SetTrigger("Atk");
        }
        ClassUnit unitTarget = target.GetComponent<ClassUnit>();
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
    

    public bool isEnemyInCounterAtkRange(GameObject target)
    {
        Vector2 myPos = transform.position;
        Vector2 targetPos = target.transform.position;

        // Chuyển sang số ô (giả định mỗi ô là 100 đơn vị)
        int dx = Mathf.Abs(Mathf.RoundToInt((targetPos.x - myPos.x) / 100f));
        int dy = Mathf.Abs(Mathf.RoundToInt((targetPos.y - myPos.y) / 100f));

        int manhattanDistance = dx + dy;

        return manhattanDistance <= RangeAtk;
    }
    public bool CheckFlipx(GameObject target)
    {
        Vector3 myPos = transform.position;
        Vector3 targetPos = target.transform.position;

        return targetPos.x <= myPos.x && targetPos.y <= myPos.y;
    }

}