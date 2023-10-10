using System.Linq.Expressions;
using JetBrains.Annotations;
using RuntimeDiagnosis.Core.ObjectDiagnosis.MemberDiagnosis.Kit;

namespace RuntimeDiagnosis.Core.UnitTests.ObjectDiagnosis.MemberDiagnosis.Kit;

[TestFixture]
public class MemberAccessorTests
{
    private const string SkipSetup = "SkipSetup";
    private IMemberAccessorInternal<string> _accessor = null!;

    private string TestProperty { get; [UsedImplicitly] set; } = "Test";
    
    
    [SetUp]
    public void SetUp()
    {
        if (CheckForSkipSetup())
            return;

        _accessor = new MemberAccessor<string>();
        _accessor.Initialize(() => TestProperty);
    }

    [Test]
    public void ValueSetter_Getter_ReturnsNotNull()
    {
        // Act
        var result = _accessor.Value;

        // Assert
        Assert.That(result, Is.Not.Null);
    }

    [Test]
    public void ValueSetter_Getter_ReturnsCorrectValue()
    {
        // Act
        var result = _accessor.Value;

        // Assert
        Assert.That(result, Is.EqualTo(TestProperty));
    }

    [Test]
    [TestCase("New Value")]
    [TestCase(null)]
    public void ValueSetter_SetsValue_CorrectlyRetrieved(string newValue)
    {
        // Act
        _accessor.Value = newValue;

        // Assert
        Assert.That(TestProperty, Is.EqualTo(newValue));
    }

    [Test]
    [Category(SkipSetup)]
    public void Initialize_PassNonMemberExpression_ThrowsArgumentException()
    {
        // Arrange
        IMemberAccessorInternal<double> accessor = new MemberAccessor<double>();
        Expression<Func<double>> expression = () => 123.45;

        // Act & Assert
        Assert.Throws<ArgumentException>(() => accessor.Initialize(expression),
            $"{nameof(expression)} must return a field or property");
    }

    private static bool CheckForSkipSetup()
    {
        var categories = TestContext.CurrentContext.Test.Properties["Category"];
        return categories.Contains(SkipSetup);
    }
}