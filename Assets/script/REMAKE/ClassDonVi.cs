using System;
using System.Collections;
using UnityEngine;

public class ClassDonVi : MonoBehaviour
{
    //kiểm tra đơn vị có đang được chọn để thực hiện các hành động khác.
    public bool isSelected = false;
    //chú thích: tên đơn vị/spear,sword,.../infantry,cavalry,archer,...
    public string NameArmy, TypeArmy, BranchArmy;
    //thông số 
    public int Atk, Def, Hp, Charge, Speed, NumberBlock, RangeAtk;
    // action turn/ turn
    public int MaxTurnSpeed, CurrentSpeed, MaxTurnAtk, CurrentAtk;
    // thời gian giữa mỗi lần di chuyển
    public float Time=0.3f;
    
    private Coroutine CoroutineMove;
    public ClassDonVi( string nameArmy, string typeArmy, string branchArmy, int atk, int def, int hp, 
        int charge, int speed, int maxTurnSpeed, int maxTurnAtk, int rangeAtk)
    {
        NameArmy = nameArmy;
        TypeArmy = typeArmy;
        BranchArmy = branchArmy;
        Atk = atk;
        Def = def;
        Hp = hp;
        RangeAtk = rangeAtk;
        Charge = charge;
        Speed = speed;
        MaxTurnSpeed = maxTurnSpeed;
        CurrentSpeed = MaxTurnSpeed;
        MaxTurnAtk = maxTurnAtk;
        CurrentAtk = MaxTurnAtk;
    }

    public ClassDonVi()
    {
        
    }
    public virtual void Move(Vector3 targetPosition)
    {
        if (CurrentSpeed > 0 )
        {
            if (CoroutineMove != null)
                StopCoroutine(CoroutineMove);

            Vector3 StartPosition = new Vector3(
                Mathf.Round(transform.position.x / 100f) * 100f,
                Mathf.Round(transform.position.y / 100f) * 100f,
                transform.position.z
            );
            CoroutineMove = StartCoroutine(MoveRule(StartPosition, targetPosition));
            CurrentSpeed--;
        }
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
            yield return new WaitForSeconds(Time);
            NumberBlock++;
        }

        for (int i = 0; i < Mathf.Abs(buocY); i++)
        {
            currentPos += new Vector3(0, stepY * 100f, 0);
            transform.position = currentPos;
            yield return new WaitForSeconds(Time);
            NumberBlock++;
        }

        if (NumberBlock < Speed)
        {
            
        }
    }
}
