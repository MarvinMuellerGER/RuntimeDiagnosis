using RuntimeDiagnosis.Core.IntegrationTests.Subclasses.Testees;
using RuntimeDiagnosis.Core.ObjectDiagnosis.MemberDiagnosis.DirectionValueDiagnosis;
using static RuntimeDiagnosis.Core.ObjectDiagnosis.MemberDiagnosis.DirectionValueDiagnosis.IDirectionValueDiagnosis.ValueDirectionType;

namespace RuntimeDiagnosis.Core.IntegrationTests.Subclasses;

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
            _testClassTestee.ObjectDiagnosis.GetMemberDiagnose<bool>(nameof(_testClassTestee.TestProperty));
        Assert.That(memberDiagnoseTestProperty, Is.Not.Null);
        
        var tcs = new TaskCompletionSource<IDirectionValueDiagnosis?>();
        memberDiagnoseTestProperty!.InputValueDiagnosis.JustCalled += (_, caller) => 
            tcs.TrySetResult(caller);

        _secondTestClassTestee.SecondTestProperty = true;
        tcs.Task.Wait(100);
        
        var caller = tcs.Task.Result;
        Assert.That(caller, Is.Not.Null);
        Assert.Multiple(() =>
        {
            Assert.That(caller!.MemberDiagnosis.ObjectDiagnosis.Owner, Is.EqualTo(_secondTestClassTestee));
            Assert.That(caller.MemberDiagnosis.MemberName, Is.EqualTo(nameof(_secondTestClassTestee.SecondTestProperty)));
            Assert.That(caller.ValueDirection, Is.EqualTo(Input));
        });
    }
    
    [Test]
    public void Test_OutputJustCalledEvent()
    {
        var memberDiagnoseTestProperty = 
            _testClassTestee.ObjectDiagnosis.GetMemberDiagnose<bool>(nameof(_testClassTestee.TestProperty));
        Assert.That(memberDiagnoseTestProperty, Is.Not.Null);
        
        var tcs = new TaskCompletionSource<IDirectionValueDiagnosis?>();
        memberDiagnoseTestProperty!.OutputValueDiagnosis.JustCalled += (_, caller) => 
            tcs.TrySetResult(caller);

        var secondTestProperty = _secondTestClassTestee.SecondTestProperty;
        tcs.Task.Wait(100);
        
        var caller = tcs.Task.Result;
        Assert.That(caller, Is.Not.Null);
        Assert.Multiple(() =>
        {
            Assert.That(caller!.MemberDiagnosis.ObjectDiagnosis.Owner, Is.EqualTo(_secondTestClassTestee));
            Assert.That(caller.MemberDiagnosis.MemberName, Is.EqualTo(nameof(_secondTestClassTestee.SecondTestProperty)));
            Assert.That(caller.ValueDirection, Is.EqualTo(Output));
        });
    }
}