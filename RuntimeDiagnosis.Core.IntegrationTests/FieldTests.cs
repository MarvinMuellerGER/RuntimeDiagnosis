using RuntimeDiagnosis.Core.IntegrationTests.Testees;

namespace RuntimeDiagnosis.Core.IntegrationTests;

public class FieldTests
{
    private TestClassDiagnosable _testee = null!;

    [SetUp]
    public void Setup() => 
        _testee = new();

    [Test]
    public void Test_InputDiagnose()
    {
        var memberDiagnose = 
            _testee.ObjectDiagnose.GetMemberDiagnose<bool>(nameof(_testee.TestField));
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
            
            Assert.That(_testee.TestField, Is.False);
        });
        
        _testee.TestField = true;
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
            
            Assert.That(_testee.TestField, Is.True);
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

            Assert.That(_testee.TestField, Is.False);
        });
        
        _testee.TestField = false;
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

            Assert.That(_testee.TestField, Is.False);
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

            Assert.That(_testee.TestField, Is.True);
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

            Assert.That(_testee.TestField, Is.False);
        });
    }
    
    [Test]
    public void Test_OutputDiagnose()
    {
        var memberDiagnose = 
            _testee.ObjectDiagnose.GetMemberDiagnose<bool>(nameof(_testee.TestField));
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
            
            Assert.That(_testee.TestField, Is.False);
        });
        
        _testee.TestField = true;
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
            
            Assert.That(_testee.TestField, Is.True);
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
            
            Assert.That(_testee.TestField, Is.False);
        });
        
        _testee.TestField = false;
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
            
            Assert.That(_testee.TestField, Is.False);
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
            
            Assert.That(_testee.TestField, Is.True);
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
            
            Assert.That(_testee.TestField, Is.False);
        });
    }
    
    [Test]
    public void Test_InputAndOutputDiagnoseAtTheSameTime()
    {
        var memberDiagnose = 
            _testee.ObjectDiagnose.GetMemberDiagnose<bool>(nameof(_testee.TestField));
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
            
            Assert.That(_testee.TestField, Is.False);
        });
        
        _testee.TestField = true;
        memberDiagnose!.InputValue.DiagnoseActive.Value = true;
        _testee.TestField = false;
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

            Assert.That(_testee.TestField, Is.False);
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

            Assert.That(_testee.TestField, Is.True);
        });
    }
}