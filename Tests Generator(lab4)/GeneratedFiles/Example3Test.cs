using System;
using NUnit.Framework;
using Moq;
using System.Collections.Generic;

[TestFixture]
class Example3Test
{
    private Mock<IEnumerable<int>> _d;
    private Example3 _example3;
    [SetUp]
    public void SetUp()
    {
        _d = new Mock<IEnumerable<int>>();
        _example3 = new Example3(_d.Object);
    }

    [Test]
    public void Function1()
    {
        var a = default (int);
        var b = default (string);
        _example3.Function1(a, b);
        Assert.Fail("autogenerated");
    }

    [Test]
    public void Function2()
    {
        _example3.Function2();
        Assert.Fail("autogenerated");
    }
}