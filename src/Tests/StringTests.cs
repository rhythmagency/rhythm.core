namespace Tests;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhythm.Core;

/// <summary>
/// Tests for string extension methods.
/// </summary>
[TestClass]
public sealed class StringTests
{

    #region Test Methods

    /// <summary>
    /// Test if camel case works.
    /// </summary>
    [TestMethod]
    public void CamelCase()
    {
        var result = "hello-world-test".ToCamelCase();
        Assert.AreEqual("helloWorldTest", result);
    }

    /// <summary>
    /// Test if pascal case works.
    /// </summary>
    [TestMethod]
    public void PascalCase()
    {
        var result = "hello-world-test".ToPascalCase();
        Assert.AreEqual("HelloWorldTest", result);
    }

    /// <summary>
    /// Test if pascal case works with a single letter.
    /// </summary>
    [TestMethod]
    public void PascalCaseSingleLetter()
    {
        var result = "a".ToPascalCase();
        Assert.AreEqual("A", result);
    }

    #endregion
}
