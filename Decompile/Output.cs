﻿using System;

namespace LuaDec.Decompile
{
    public class Output
    {
        private class DefaultOutputProvide : IOutputProvider
        {
            public void WriteByte(byte b)
            {
                Console.Write(b);
            }

            public void WriteLine()
            {
                Console.WriteLine();
            }

            public void WriteString(string s)
            {
                Console.Write(s);
            }
        }

        private int indentLevel = 0;
        private IOutputProvider output;
        private int position = 0;

        public int IndentLevel { get => indentLevel; set => indentLevel = value; }

        public int Position => position;

        public Output() : this(new DefaultOutputProvide())
        {
        }

        public Output(IOutputProvider output)
        {
            this.output = output;
        }

        private void Start()
        {
            if (position == 0)
            {
                for (int i = indentLevel; i != 0; i--)
                {
                    output.WriteString(" ");
                    position++;
                }
            }
        }

        public void Dedent()
        {
            indentLevel -= 2;
        }

        public void Indent()
        {
            indentLevel += 2;
        }

        public void WriteByte(byte b)
        {
            Start();
            output.WriteByte(b);
            position += 1;
        }

        public void WriteLine()
        {
            Start();
            output.WriteLine();
            position = 0;
        }

        public void WriteLine(string s)
        {
            WriteString(s);
            WriteLine();
        }

        public void WriteString(string s)
        {
            Start();
            for (int i = 0; i < s.Length; i++)
            {
                output.WriteByte((byte)s[i]);
            }
            position += s.Length;
        }
    }
}