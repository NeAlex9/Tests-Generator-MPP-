﻿using System;
using System.Collections.Generic;

namespace Tests_Generator_lab4_.FilesForTests
{
    public class Example1
    {
        public IEnumerable<int> Interface { get; private set; }

        public Example1(IDisposable s){}

        public void Function1(){}
        public void Function2(){}
    }

    public class Example2
    {
        public IEnumerable<int> Interface { get; private set; }

        public void Function1() { }
        public void Function2() { }
    }
}
