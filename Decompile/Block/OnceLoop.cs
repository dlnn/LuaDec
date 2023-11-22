﻿using LuaDec.Decompile.Statement;
using LuaDec.Parser;

namespace LuaDec.Decompile.Block
{
    public class OnceLoop : ContainerBlock
    {

        public OnceLoop(LFunction function, int begin, int end)
             : base(function, begin, end, CloseType.None, -1, 0)
        {
        }

        public override int scopeEnd()
        {
            return end - 1;
        }

        public override bool breakable()
        {
            return true;
        }

        public override bool isUnprotected()
        {
            return false;
        }

        public override int getLoopback()
        {
            return begin;
        }

        public override void Write(Decompiler d, Output output)
        {
            output.WriteLine("repeat");
            output.Indent();
            WriteSequence(d, output, statements);
            output.Dedent();
            output.WriteString("until true");
        }

    }

}
