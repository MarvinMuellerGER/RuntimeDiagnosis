using RuntimeDiagnosis.Core.IntegrationTests.Testee;

namespace RuntimeDiagnosis.Core.IntegrationTests;

public class PropertyTests
{
    private TestClassDiagnosable _testee = null!;

    [SetUp]
    public void Setup() => 
        _testee = new();

    [Test]
    public void Test_InputDiagnose()
    {
        var memberDiagnose = 
            _testee.ObjectDiagnose.GetMemberDiagnose<bool>(nameof(_testee.TestProperty));
        Assert.That(memberDiagnose, Is.Not.Null);
        
        Assert.Multiple(() =>
        {
            Assert.That(memberDiagnose!.InputValue.DiagnoseActive.Value, Is.False);
            Assert.That(memberDiagnose.InputValue.OriginalValue.Value, Is.False);
            Assert.That(memberDiagnose.InputValue.DiagnoseValue.Value, Is.False);
            Assert.That(memberDiagnose.InputValue.CurrentValue.Value, Is.False);
            
            Assert.That(memberDiagnose.OutputValue.DiagnoseActive.Value, Is.False);
            Assert.That(memberDiagnose.OutputValue.OriginalValue.Value, Is.False);
            Assert.That(memberDiagnose.OutputValue.DiagnoseValue.Value, Is.False);
            Assert.That(memberDiagnose.OutputValue.CurrentValue.Value, Is.False);
            
            Assert.That(_testee.TestProperty, Is.False);
        });
        
        _testee.TestProperty = true;
        Assert.Multiple(() =>
        {
            Assert.That(memberDiagnose!.InputValue.DiagnoseActive.Value, Is.False);
            Assert.That(memberDiagnose.InputValue.OriginalValue.Value, Is.True);
            Assert.That(memberDiagnose.InputValue.DiagnoseValue.Value, Is.False);
            Assert.That(memberDiagnose.InputValue.CurrentValue.Value, Is.True);
            
            Assert.That(memberDiagnose.OutputValue.DiagnoseActive.Value, Is.False);
            Assert.That(memberDiagnose.OutputValue.OriginalValue.Value, Is.True);
            Assert.That(memberDiagnose.OutputValue.DiagnoseValue.Value, Is.False);
            Assert.That(memberDiagnose.OutputValue.CurrentValue.Value, Is.True);
            
            Assert.That(_testee.TestProperty, Is.True);
        });
        
        memberDiagnose!.InputValue.DiagnoseActive.Value = true;
        Assert.Multiple(() =>
        {
            Assert.That(memberDiagnose.InputValue.DiagnoseActive.Value, Is.True);
            Assert.That(memberDiagnose.InputValue.OriginalValue.Value, Is.True);
            Assert.That(memberDiagnose.InputValue.DiagnoseValue.Value, Is.False);
            Assert.That(memberDiagnose.InputValue.CurrentValue.Value, Is.False);
            
            Assert.That(memberDiagnose.OutputValue.DiagnoseActive.Value, Is.False);
            Assert.That(memberDiagnose.OutputValue.OriginalValue.Value, Is.False);
            Assert.That(memberDiagnose.OutputValue.DiagnoseValue.Value, Is.False);
            Assert.That(memberDiagnose.OutputValue.CurrentValue.Value, Is.False);

            Assert.That(_testee.TestProperty, Is.False);
        });
        
        _testee.TestProperty = false;
        Assert.Multiple(() =>
        {
            Assert.That(memberDiagnose.InputValue.DiagnoseActive.Value, Is.True);
            Assert.That(memberDiagnose.InputValue.OriginalValue.Value, Is.False);
            Assert.That(memberDiagnose.InputValue.DiagnoseValue.Value, Is.False);
            Assert.That(memberDiagnose.InputValue.CurrentValue.Value, Is.False);
            
            Assert.That(memberDiagnose.OutputValue.DiagnoseActive.Value, Is.False);
            Assert.That(memberDiagnose.OutputValue.OriginalValue.Value, Is.False);
            Assert.That(memberDiagnose.OutputValue.DiagnoseValue.Value, Is.False);
            Assert.That(memberDiagnose.OutputValue.CurrentValue.Value, Is.False);

            Assert.That(_testee.TestProperty, Is.False);
        });
        
        memberDiagnose.InputValue.DiagnoseValue.Value = true;
        Assert.Multiple(() =>
        {
            Assert.That(memberDiagnose.InputValue.DiagnoseActive.Value, Is.True);
            Assert.That(memberDiagnose.InputValue.OriginalValue.Value, Is.False);
            Assert.That(memberDiagnose.InputValue.DiagnoseValue.Value, Is.True);
            Assert.That(memberDiagnose.InputValue.CurrentValue.Value, Is.True);
            
            Assert.That(memberDiagnose.OutputValue.DiagnoseActive.Value, Is.False);
            Assert.That(memberDiagnose.OutputValue.OriginalValue.Value, Is.True);
            Assert.That(memberDiagnose.OutputValue.DiagnoseValue.Value, Is.False);
            Assert.That(memberDiagnose.OutputValue.CurrentValue.Value, Is.True);

            Assert.That(_testee.TestProperty, Is.True);
        });
        
        memberDiagnose.InputValue.DiagnoseActive.Value = false;
        Assert.Multiple(() =>
        {
            Assert.That(memberDiagnose.InputValue.DiagnoseActive.Value, Is.False);
            Assert.That(memberDiagnose.InputValue.OriginalValue.Value, Is.False);
            Assert.That(memberDiagnose.InputValue.DiagnoseValue.Value, Is.True);
            Assert.That(memberDiagnose.InputValue.CurrentValue.Value, Is.False);
            
            Assert.That(memberDiagnose.OutputValue.DiagnoseActive.Value, Is.False);
            Assert.That(memberDiagnose.OutputValue.OriginalValue.Value, Is.False);
            Assert.That(memberDiagnose.OutputValue.DiagnoseValue.Value, Is.False);
            Assert.That(memberDiagnose.OutputValue.CurrentValue.Value, Is.False);

            Assert.That(_testee.TestProperty, Is.False);
        });
    }
    
    [Test]
    public void Test_OutputDiagnose()
    {
        var memberDiagnose = 
            _testee.ObjectDiagnose.GetMemberDiagnose<bool>(nameof(_testee.TestProperty));
        Assert.That(memberDiagnose, Is.Not.Null);
        
        Assert.Multiple(() =>
        {
            Assert.That(memberDiagnose!.InputValue.DiagnoseActive.Value, Is.False);
            Assert.That(memberDiagnose.InputValue.OriginalValue.Value, Is.False);
            Assert.That(memberDiagnose.InputValue.DiagnoseValue.Value, Is.False);
            Assert.That(memberDiagnose.InputValue.CurrentValue.Value, Is.False);
            
            Assert.That(memberDiagnose.OutputValue.DiagnoseActive.Value, Is.False);
            Assert.That(memberDiagnose.OutputValue.OriginalValue.Value, Is.False);
            Assert.That(memberDiagnose.OutputValue.DiagnoseValue.Value, Is.False);
            Assert.That(memberDiagnose.OutputValue.CurrentValue.Value, Is.False);
            
            Assert.That(_testee.TestProperty, Is.False);
        });
        
        _testee.TestProperty = true;
        Assert.Multiple(() =>
        {
            Assert.That(memberDiagnose!.InputValue.DiagnoseActive.Value, Is.False);
            Assert.That(memberDiagnose.InputValue.OriginalValue.Value, Is.True);
            Assert.That(memberDiagnose.InputValue.DiagnoseValue.Value, Is.False);
            Assert.That(memberDiagnose.InputValue.CurrentValue.Value, Is.True);
            
            Assert.That(memberDiagnose.OutputValue.DiagnoseActive.Value, Is.False);
            Assert.That(memberDiagnose.OutputValue.OriginalValue.Value, Is.True);
            Assert.That(memberDiagnose.OutputValue.DiagnoseValue.Value, Is.False);
            Assert.That(memberDiagnose.OutputValue.CurrentValue.Value, Is.True);
            
            Assert.That(_testee.TestProperty, Is.True);
        });
        
        memberDiagnose!.OutputValue.DiagnoseActive.Value = true;
        Assert.Multiple(() =>
        {
            Assert.That(memberDiagnose.InputValue.DiagnoseActive.Value, Is.False);
            Assert.That(memberDiagnose.InputValue.OriginalValue.Value, Is.True);
            Assert.That(memberDiagnose.InputValue.DiagnoseValue.Value, Is.False);
            Assert.That(memberDiagnose.InputValue.CurrentValue.Value, Is.True);

            Assert.That(memberDiagnose.OutputValue.DiagnoseActive.Value, Is.True);
            Assert.That(memberDiagnose.OutputValue.OriginalValue.Value, Is.True);
            Assert.That(memberDiagnose.OutputValue.DiagnoseValue.Value, Is.False);
            Assert.That(memberDiagnose.OutputValue.CurrentValue.Value, Is.False);
            
            Assert.That(_testee.TestProperty, Is.False);
        });
        
        _testee.TestProperty = false;
        Assert.Multiple(() =>
        {
            Assert.That(memberDiagnose.InputValue.DiagnoseActive.Value, Is.False);
            Assert.That(memberDiagnose.InputValue.OriginalValue.Value, Is.False);
            Assert.That(memberDiagnose.InputValue.DiagnoseValue.Value, Is.False);
            Assert.That(memberDiagnose.InputValue.CurrentValue.Value, Is.False);
            
            Assert.That(memberDiagnose.OutputValue.DiagnoseActive.Value, Is.True);
            Assert.That(memberDiagnose.OutputValue.OriginalValue.Value, Is.False);
            Assert.That(memberDiagnose.OutputValue.DiagnoseValue.Value, Is.False);
            Assert.That(memberDiagnose.OutputValue.CurrentValue.Value, Is.False);
            
            Assert.That(_testee.TestProperty, Is.False);
        });
        
        memberDiagnose.OutputValue.DiagnoseValue.Value = true;
        Assert.Multiple(() =>
        {
            Assert.That(memberDiagnose.InputValue.DiagnoseActive.Value, Is.False);
            Assert.That(memberDiagnose.InputValue.OriginalValue.Value, Is.False);
            Assert.That(memberDiagnose.InputValue.DiagnoseValue.Value, Is.False);
            Assert.That(memberDiagnose.InputValue.CurrentValue.Value, Is.False);

            Assert.That(memberDiagnose.OutputValue.DiagnoseActive.Value, Is.True);
            Assert.That(memberDiagnose.OutputValue.OriginalValue.Value, Is.False);
            Assert.That(memberDiagnose.OutputValue.DiagnoseValue.Value, Is.True);
            Assert.That(memberDiagnose.OutputValue.CurrentValue.Value, Is.True);
            
            Assert.That(_testee.TestProperty, Is.True);
        });
        
        memberDiagnose.OutputValue.DiagnoseActive.Value = false;
        Assert.Multiple(() =>
        {
            Assert.That(memberDiagnose.InputValue.DiagnoseActive.Value, Is.False);
            Assert.That(memberDiagnose.InputValue.OriginalValue.Value, Is.False);
            Assert.That(memberDiagnose.InputValue.DiagnoseValue.Value, Is.False);
            Assert.That(memberDiagnose.InputValue.CurrentValue.Value, Is.False);

            Assert.That(memberDiagnose.OutputValue.DiagnoseActive.Value, Is.False);
            Assert.That(memberDiagnose.OutputValue.OriginalValue.Value, Is.False);
            Assert.That(memberDiagnose.OutputValue.DiagnoseValue.Value, Is.True);
            Assert.That(memberDiagnose.OutputValue.CurrentValue.Value, Is.False);
            
            Assert.That(_testee.TestProperty, Is.False);
        });
    }
    [Test]
    public void Test_InputAndOutputDiagnoseAtTheSameTime()
    {
        var memberDiagnose = 
            _testee.ObjectDiagnose.GetMemberDiagnose<bool>(nameof(_testee.TestProperty));
        Assert.That(memberDiagnose, Is.Not.Null);
        
        Assert.Multiple(() =>
        {
            Assert.That(memberDiagnose!.InputValue.DiagnoseActive.Value, Is.False);
            Assert.That(memberDiagnose.InputValue.OriginalValue.Value, Is.False);
            Assert.That(memberDiagnose.InputValue.DiagnoseValue.Value, Is.False);
            Assert.That(memberDiagnose.InputValue.CurrentValue.Value, Is.False);
            
            Assert.That(memberDiagnose.OutputValue.DiagnoseActive.Value, Is.False);
            Assert.That(memberDiagnose.OutputValue.OriginalValue.Value, Is.False);
            Assert.That(memberDiagnose.OutputValue.DiagnoseValue.Value, Is.False);
            Assert.That(memberDiagnose.OutputValue.CurrentValue.Value, Is.False);
            
            Assert.That(_testee.TestProperty, Is.False);
        });
        
        _testee.TestProperty = true;
        memberDiagnose!.InputValue.DiagnoseActive.Value = true;
        _testee.TestProperty = false;
        memberDiagnose.InputValue.DiagnoseValue.Value = true;
        memberDiagnose.OutputValue.DiagnoseActive.Value = true;
        memberDiagnose.OutputValue.DiagnoseValue.Value = false;
        Assert.Multiple(() =>
        {
            Assert.That(memberDiagnose.InputValue.DiagnoseActive.Value, Is.True);
            Assert.That(memberDiagnose.InputValue.OriginalValue.Value, Is.False);
            Assert.That(memberDiagnose.InputValue.DiagnoseValue.Value, Is.True);
            Assert.That(memberDiagnose.InputValue.CurrentValue.Value, Is.True);
            
            Assert.That(memberDiagnose.OutputValue.DiagnoseActive.Value, Is.True);
            Assert.That(memberDiagnose.OutputValue.OriginalValue.Value, Is.True);
            Assert.That(memberDiagnose.OutputValue.DiagnoseValue.Value, Is.False);
            Assert.That(memberDiagnose.OutputValue.CurrentValue.Value, Is.False);

            Assert.That(_testee.TestProperty, Is.False);
        });
        
        memberDiagnose.OutputValue.DiagnoseValue.Value = true;
        memberDiagnose.InputValue.DiagnoseActive.Value = false;
        Assert.Multiple(() =>
        {
            Assert.That(memberDiagnose.InputValue.DiagnoseActive.Value, Is.False);
            Assert.That(memberDiagnose.InputValue.OriginalValue.Value, Is.False);
            Assert.That(memberDiagnose.InputValue.DiagnoseValue.Value, Is.True);
            Assert.That(memberDiagnose.InputValue.CurrentValue.Value, Is.False);
            
            Assert.That(memberDiagnose.OutputValue.DiagnoseActive.Value, Is.True);
            Assert.That(memberDiagnose.OutputValue.OriginalValue.Value, Is.False);
            Assert.That(memberDiagnose.OutputValue.DiagnoseValue.Value, Is.True);
            Assert.That(memberDiagnose.OutputValue.CurrentValue.Value, Is.True);

            Assert.That(_testee.TestProperty, Is.True);
        });
    }
}