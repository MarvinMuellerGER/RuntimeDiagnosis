using RuntimeDiagnosis.Core.IntegrationTests.Testees;
using RuntimeDiagnosis.Core.ObjectDiagnose.MemberDiagnose.DirectionValue;
using static RuntimeDiagnosis.Core.ObjectDiagnose.MemberDiagnose.DirectionValue.IDirectionValue.ValueDirectionType;

namespace RuntimeDiagnosis.Core.IntegrationTests;

public class JustCalledEventTests
{
    private TestClassDiagnosable _testClassTestee = null!;
    private SecondTestClassDiagnosable _secondTestClassTestee = null!;

    [SetUp]
    public void Setup()
    {
        _testClassTestee = new();
        _secondTestClassTestee = new(_testClassTestee);
    }

    [Test]
    public void Test_InputJustCalledEvent()
    {
        var memberDiagnoseTestProperty = 
            _testClassTestee.ObjectDiagnose.GetMemberDiagnose<bool>(nameof(_testClassTestee.TestProperty));
        Assert.That(memberDiagnoseTestProperty, Is.Not.Null);
        
        var tcs = new TaskCompletionSource<IDirectionValue?>();
        memberDiagnoseTestProperty!.InputValue.JustCalled += (_, caller) => 
            tcs.TrySetResult(caller);

        _secondTestClassTestee.SecondTestProperty = true;
        tcs.Task.Wait(100);
        
        var caller = tcs.Task.Result;
        Assert.That(caller, Is.Not.Null);
        Assert.Multiple(() =>
        {
            Assert.That(caller!.MemberDiagnose.ObjectDiagnose.Owner, Is.EqualTo(_secondTestClassTestee));
            Assert.That(caller.MemberDiagnose.MemberName, Is.EqualTo(nameof(_secondTestClassTestee.SecondTestProperty)));
            Assert.That(caller.ValueDirection, Is.EqualTo(Input));
        });
    }
    
    [Test]
    public void Test_OutputJustCalledEvent()
    {
        var memberDiagnoseTestProperty = 
            _testClassTestee.ObjectDiagnose.GetMemberDiagnose<bool>(nameof(_testClassTestee.TestProperty));
        Assert.That(memberDiagnoseTestProperty, Is.Not.Null);
        
        var tcs = new TaskCompletionSource<IDirectionValue?>();
        memberDiagnoseTestProperty!.OutputValue.JustCalled += (_, caller) => 
            tcs.TrySetResult(caller);

        var secondTestProperty = _secondTestClassTestee.SecondTestProperty;
        tcs.Task.Wait(100);
        
        var caller = tcs.Task.Result;
        Assert.That(caller, Is.Not.Null);
        Assert.Multiple(() =>
        {
            Assert.That(caller!.MemberDiagnose.ObjectDiagnose.Owner, Is.EqualTo(_secondTestClassTestee));
            Assert.That(caller.MemberDiagnose.MemberName, Is.EqualTo(nameof(_secondTestClassTestee.SecondTestProperty)));
            Assert.That(caller.ValueDirection, Is.EqualTo(Output));
        });
    }
}