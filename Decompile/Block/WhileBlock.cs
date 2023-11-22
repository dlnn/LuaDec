﻿using LuaDec.Decompile.Condition;
using LuaDec.Decompile.Expression;
using LuaDec.Parser;
using LuaDec.Decompile.Statement;

namespace LuaDec.Decompile.Block
{
    public abstract class WhileBlock : ContainerBlock
    {

        protected ICondition cond;

        private IExpression condexpr;

        public WhileBlock(LFunction function, ICondition cond, int begin, int end, CloseType closeType, int closeLine)
            : base(function, begin, end, closeType, closeLine, -1)
        {
            this.cond = cond;
        }

        public override void resolve(Registers r)
        {
            condexpr = cond.asExpression(r);
        }

        public override void walk(Walker w)
        {
            w.VisitStatement(this);
            condexpr.walk(w);
            foreach (IStatement statement in statements)
            {
                statement.walk(w);
            }
        }

        public override bool breakable()
        {
            return true;
        }

        public override int getLoopback()
        {
            throw new System.InvalidOperationException();
        }

        public override void print(Decompiler d, Output output)
        {
            output.WriteString("while ");
            condexpr.print(d, output);
            output.WriteString(" do");
            output.WriteLine();
            output.Indent();
            IStatement.printSequence(d, output, statements);
            output.Dedent();
            output.WriteString("end");
        }

    }

}