using System;
using NUnit.Framework;
using Moq;
using System.Collections.Generic;

[TestFixture]
class Example2Test
{
    private Example2 _example2;
    [SetUp]
    public void SetUp()
    {
        _example2 = new Example2();
    }

    [Test]
    public void Function1()
    {
        _example2.Function1();
        Assert.Fail("autogenerated");
    }

    [Test]
    public void Function2()
    {
        _example2.Function2();
        Assert.Fail("autogenerated");
    }
}