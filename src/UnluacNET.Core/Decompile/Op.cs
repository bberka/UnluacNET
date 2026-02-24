namespace UnluacNET.Core.Decompile;

public sealed class Opcode
{
    // TODO: Optimize method
    public static string CodePointToString(Op opcode, LInstruction code)
    {
        var name = opcode.GetType()
            .Name;

        switch (opcode)
        {
            // A
            case Op.Close:
            case Op.Loadkx:
                return string.Format("{0} {1}", name, code.A);

            // A_B
            case Op.Move:
            case Op.Loadnil:
            case Op.Getupval:
            case Op.Setupval:
            case Op.Unm:
            case Op.Not:
            case Op.Len:
            case Op.Return:
            case Op.Vararg:
                return string.Format("{0} {1} {2}", name, code.A, code.B);

            // A_C
            case Op.Test:
            case Op.Tforloop:
            case Op.Tforcall:
                return string.Format("{0} {1} {2}", name, code.A, code.C);

            // A_B_C
            case Op.Loadbool:
            case Op.Gettable:
            case Op.Settable:
            case Op.Newtable:
            case Op.Self:
            case Op.Add:
            case Op.Sub:
            case Op.Mul:
            case Op.Div:
            case Op.Mod:
            case Op.Pow:
            case Op.Concat:
            case Op.Eq:
            case Op.Lt:
            case Op.Le:
            case Op.Testset:
            case Op.Call:
            case Op.Tailcall:
            case Op.Setlist:
            case Op.Gettabup:
            case Op.Settabup:
                return string.Format("{0} {1} {2} {3}", name, code.A, code.B, code.C);

            // A_Bx
            case Op.Loadk:
            case Op.Getglobal:
            case Op.Setglobal:
            case Op.Closure:
                return string.Format("{0} {1} {2}", name, code.A, code.Bx);

            // A_sBx
            case Op.Forloop:
            case Op.Forprep:
                return string.Format("{0} {1} {2}", name, code.A, code.SBx);

            // Ax
            case Op.Extraarg:
                return string.Format("{0} <Ax>", name);

            // sBx
            case Op.Jmp:
                return string.Format("{0} {1}", name, code.SBx);

            default:
                return name;
        }
    }
}

public enum Op
{
    /*------------------------------------------------------------------------
    name           args    description
    --------------------------------------------------------------------------*/
    Move, /*   A B     R(A) := R(B)                                       */
    Loadk, /*   A Bx    R(A) := Kst(Bx)                                    */
    Loadbool, /*   A B C   R(A) := (Bool)B; if (C) pc++                       */
    Loadnil, /*   A B     R(A) := ... := R(B) := nil                         */
    Getupval, /*   A B     R(A) := UpValue[B]                                 */

    Getglobal, /*   A Bx    R(A) := Gbl[Kst(Bx)]                               */
    Gettable, /*   A B C   R(A) := R(B)[RK(C)]                                */

    Setglobal, /*   A Bx    Gbl[Kst(Bx)] := R(A)                               */
    Setupval, /*   A B     UpValue[B] := R(A)                                 */
    Settable, /*   A B C   R(A)[RK(B)] := RK(C)                               */

    Newtable, /*   A B C   R(A) := {} (size = B,C)                            */

    Self, /*   A B C   R(A+1) := R(B); R(A) := R(B)[RK(C)]                */

    Add, /*   A B C   R(A) := RK(B) + RK(C)                              */
    Sub, /*   A B C   R(A) := RK(B) - RK(C)                              */
    Mul, /*   A B C   R(A) := RK(B) * RK(C)                              */
    Div, /*   A B C   R(A) := RK(B) / RK(C)                              */
    Mod, /*   A B C   R(A) := RK(B) % RK(C)                              */
    Pow, /*   A B C   R(A) := RK(B) ^ RK(C)                              */
    Unm, /*   A B     R(A) := -R(B)                                      */
    Not, /*   A B     R(A) := not R(B)                                   */
    Len, /*   A B     R(A) := length of R(B)                             */

    Concat, /*   A B C   R(A) := R(B).. ... ..R(C)                          */

    Jmp, /*   sBx     pc+=sBx (different in 5.2)                         */

    Eq, /*   A B C   if ((RK(B) == RK(C)) ~= A) then pc++               */
    Lt, /*   A B C   if ((RK(B) <  RK(C)) ~= A) then pc++               */
    Le, /*   A B C   if ((RK(B) <= RK(C)) ~= A) then pc++               */

    Test, /*   A C     if not (R(A) <=> C) then pc++                      */
    Testset, /*   A B C   if (R(B) <=> C) then R(A) := R(B) else pc++        */

    Call, /*   A B C   R(A), ... ,R(A+C-2) := R(A)(R(A+1), ... ,R(A+B-1)) */
    Tailcall, /*   A B C   return R(A)(R(A+1), ... ,R(A+B-1))                 */
    Return, /*   A B     return R(A), ... ,R(A+B-2)      (see note)         */

    Forloop, /*   A sBx   R(A)+=R(A+2);
                   if R(A) <?= R(A+1) then { pc+=sBx; R(A+3)=R(A) }           */
    Forprep, /*   A sBx   R(A)-=R(A+2); pc+=sBx                              */

    Tforloop, /*   A C     R(A+3), ... ,R(A+3+C) := R(A)(R(A+1), R(A+2));
                      if R(A+3) ~= nil then { pc++; R(A+2)=R(A+3); }          */
    Setlist, /*   A B C   R(A)[(C-1)*FPF+i] := R(A+i), 1 <= i <= B           */

    Close, /*   A       close all variables in the stack up to (>=) R(A)   */
    Closure, /*   A Bx    R(A) := closure(KPROTO[Bx], R(A), ... ,R(A+n))     */

    Vararg, /*   A B     R(A), R(A+1), ..., R(A+B-1) = vararg               */

    // Lua 5.2 Opcodes
    Loadkx,
    Gettabup,
    Settabup,
    Tforcall,
    Extraarg
}

/*===========================================================================
  Notes:
  (*) In OP_CALL, if (B == 0) then B = top. C is the number of returns - 1,
      and can be 0: OP_CALL then sets `top' to last_result+1, so
      next open instruction (OP_CALL, OP_RETURN, OP_SETLIST) may use `top'.

  (*) In OP_VARARG, if (B == 0) then use actual number of varargs and
      set top (like in OP_CALL with C == 0).

  (*) In OP_RETURN, if (B == 0) then return up to `top'

  (*) In OP_SETLIST, if (B == 0) then B = `top';
      if (C == 0) then next `instruction' is real C

  (*) For comparisons, A specifies what condition the test should accept
      (true or false).

  (*) All `skips' (pc++) assume that next instruction is a jump
===========================================================================*/