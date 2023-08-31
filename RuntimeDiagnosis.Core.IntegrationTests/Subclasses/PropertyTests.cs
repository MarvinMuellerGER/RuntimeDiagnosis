using RuntimeDiagnosis.Core.IntegrationTests.Subclasses.Testees;

namespace RuntimeDiagnosis.Core.IntegrationTests.Subclasses;

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
            _testee.ObjectDiagnosis.GetMemberDiagnose<bool>(nameof(_testee.TestProperty));
        Assert.That(memberDiagnose, Is.Not.Null);
        
        Assert.Multiple(() =>
        {
            Assert.That(memberDiagnose!.InputValueDiagnosis.DiagnoseActive.Value, Is.False);
            Assert.That(memberDiagnose.InputValueDiagnosis.OriginalValue.Value, Is.False);
            Assert.That(memberDiagnose.InputValueDiagnosis.DiagnoseValue.Value, Is.False);
            Assert.That(memberDiagnose.InputValueDiagnosis.CurrentValue.Value, Is.False);
            
            Assert.That(memberDiagnose.OutputValueDiagnosis.DiagnoseActive.Value, Is.False);
            Assert.That(memberDiagnose.OutputValueDiagnosis.OriginalValue.Value, Is.False);
            Assert.That(memberDiagnose.OutputValueDiagnosis.DiagnoseValue.Value, Is.False);
            Assert.That(memberDiagnose.OutputValueDiagnosis.CurrentValue.Value, Is.False);
            
            Assert.That(_testee.TestProperty, Is.False);
        });
        
        _testee.TestProperty = true;
        Assert.Multiple(() =>
        {
            Assert.That(memberDiagnose!.InputValueDiagnosis.DiagnoseActive.Value, Is.False);
            Assert.That(memberDiagnose.InputValueDiagnosis.OriginalValue.Value, Is.True);
            Assert.That(memberDiagnose.InputValueDiagnosis.DiagnoseValue.Value, Is.False);
            Assert.That(memberDiagnose.InputValueDiagnosis.CurrentValue.Value, Is.True);
            
            Assert.That(memberDiagnose.OutputValueDiagnosis.DiagnoseActive.Value, Is.False);
            Assert.That(memberDiagnose.OutputValueDiagnosis.OriginalValue.Value, Is.True);
            Assert.That(memberDiagnose.OutputValueDiagnosis.DiagnoseValue.Value, Is.False);
            Assert.That(memberDiagnose.OutputValueDiagnosis.CurrentValue.Value, Is.True);
            
            Assert.That(_testee.TestProperty, Is.True);
        });
        
        memberDiagnose!.InputValueDiagnosis.DiagnoseActive.Value = true;
        Assert.Multiple(() =>
        {
            Assert.That(memberDiagnose.InputValueDiagnosis.DiagnoseActive.Value, Is.True);
            Assert.That(memberDiagnose.InputValueDiagnosis.OriginalValue.Value, Is.True);
            Assert.That(memberDiagnose.InputValueDiagnosis.DiagnoseValue.Value, Is.False);
            Assert.That(memberDiagnose.InputValueDiagnosis.CurrentValue.Value, Is.False);
            
            Assert.That(memberDiagnose.OutputValueDiagnosis.DiagnoseActive.Value, Is.False);
            Assert.That(memberDiagnose.OutputValueDiagnosis.OriginalValue.Value, Is.False);
            Assert.That(memberDiagnose.OutputValueDiagnosis.DiagnoseValue.Value, Is.False);
            Assert.That(memberDiagnose.OutputValueDiagnosis.CurrentValue.Value, Is.False);

            Assert.That(_testee.TestProperty, Is.False);
        });
        
        _testee.TestProperty = false;
        Assert.Multiple(() =>
        {
            Assert.That(memberDiagnose.InputValueDiagnosis.DiagnoseActive.Value, Is.True);
            Assert.That(memberDiagnose.InputValueDiagnosis.OriginalValue.Value, Is.False);
            Assert.That(memberDiagnose.InputValueDiagnosis.DiagnoseValue.Value, Is.False);
            Assert.That(memberDiagnose.InputValueDiagnosis.CurrentValue.Value, Is.False);
            
            Assert.That(memberDiagnose.OutputValueDiagnosis.DiagnoseActive.Value, Is.False);
            Assert.That(memberDiagnose.OutputValueDiagnosis.OriginalValue.Value, Is.False);
            Assert.That(memberDiagnose.OutputValueDiagnosis.DiagnoseValue.Value, Is.False);
            Assert.That(memberDiagnose.OutputValueDiagnosis.CurrentValue.Value, Is.False);

            Assert.That(_testee.TestProperty, Is.False);
        });
        
        memberDiagnose.InputValueDiagnosis.DiagnoseValue.Value = true;
        Assert.Multiple(() =>
        {
            Assert.That(memberDiagnose.InputValueDiagnosis.DiagnoseActive.Value, Is.True);
            Assert.That(memberDiagnose.InputValueDiagnosis.OriginalValue.Value, Is.False);
            Assert.That(memberDiagnose.InputValueDiagnosis.DiagnoseValue.Value, Is.True);
            Assert.That(memberDiagnose.InputValueDiagnosis.CurrentValue.Value, Is.True);
            
            Assert.That(memberDiagnose.OutputValueDiagnosis.DiagnoseActive.Value, Is.False);
            Assert.That(memberDiagnose.OutputValueDiagnosis.OriginalValue.Value, Is.True);
            Assert.That(memberDiagnose.OutputValueDiagnosis.DiagnoseValue.Value, Is.False);
            Assert.That(memberDiagnose.OutputValueDiagnosis.CurrentValue.Value, Is.True);

            Assert.That(_testee.TestProperty, Is.True);
        });
        
        memberDiagnose.InputValueDiagnosis.DiagnoseActive.Value = false;
        Assert.Multiple(() =>
        {
            Assert.That(memberDiagnose.InputValueDiagnosis.DiagnoseActive.Value, Is.False);
            Assert.That(memberDiagnose.InputValueDiagnosis.OriginalValue.Value, Is.False);
            Assert.That(memberDiagnose.InputValueDiagnosis.DiagnoseValue.Value, Is.True);
            Assert.That(memberDiagnose.InputValueDiagnosis.CurrentValue.Value, Is.False);
            
            Assert.That(memberDiagnose.OutputValueDiagnosis.DiagnoseActive.Value, Is.False);
            Assert.That(memberDiagnose.OutputValueDiagnosis.OriginalValue.Value, Is.False);
            Assert.That(memberDiagnose.OutputValueDiagnosis.DiagnoseValue.Value, Is.False);
            Assert.That(memberDiagnose.OutputValueDiagnosis.CurrentValue.Value, Is.False);

            Assert.That(_testee.TestProperty, Is.False);
        });
    }
    
    [Test]
    public void Test_OutputDiagnose()
    {
        var memberDiagnose = 
            _testee.ObjectDiagnosis.GetMemberDiagnose<bool>(nameof(_testee.TestProperty));
        Assert.That(memberDiagnose, Is.Not.Null);
        
        Assert.Multiple(() =>
        {
            Assert.That(memberDiagnose!.InputValueDiagnosis.DiagnoseActive.Value, Is.False);
            Assert.That(memberDiagnose.InputValueDiagnosis.OriginalValue.Value, Is.False);
            Assert.That(memberDiagnose.InputValueDiagnosis.DiagnoseValue.Value, Is.False);
            Assert.That(memberDiagnose.InputValueDiagnosis.CurrentValue.Value, Is.False);
            
            Assert.That(memberDiagnose.OutputValueDiagnosis.DiagnoseActive.Value, Is.False);
            Assert.That(memberDiagnose.OutputValueDiagnosis.OriginalValue.Value, Is.False);
            Assert.That(memberDiagnose.OutputValueDiagnosis.DiagnoseValue.Value, Is.False);
            Assert.That(memberDiagnose.OutputValueDiagnosis.CurrentValue.Value, Is.False);
            
            Assert.That(_testee.TestProperty, Is.False);
        });
        
        _testee.TestProperty = true;
        Assert.Multiple(() =>
        {
            Assert.That(memberDiagnose!.InputValueDiagnosis.DiagnoseActive.Value, Is.False);
            Assert.That(memberDiagnose.InputValueDiagnosis.OriginalValue.Value, Is.True);
            Assert.That(memberDiagnose.InputValueDiagnosis.DiagnoseValue.Value, Is.False);
            Assert.That(memberDiagnose.InputValueDiagnosis.CurrentValue.Value, Is.True);
            
            Assert.That(memberDiagnose.OutputValueDiagnosis.DiagnoseActive.Value, Is.False);
            Assert.That(memberDiagnose.OutputValueDiagnosis.OriginalValue.Value, Is.True);
            Assert.That(memberDiagnose.OutputValueDiagnosis.DiagnoseValue.Value, Is.False);
            Assert.That(memberDiagnose.OutputValueDiagnosis.CurrentValue.Value, Is.True);
            
            Assert.That(_testee.TestProperty, Is.True);
        });
        
        memberDiagnose!.OutputValueDiagnosis.DiagnoseActive.Value = true;
        Assert.Multiple(() =>
        {
            Assert.That(memberDiagnose.InputValueDiagnosis.DiagnoseActive.Value, Is.False);
            Assert.That(memberDiagnose.InputValueDiagnosis.OriginalValue.Value, Is.True);
            Assert.That(memberDiagnose.InputValueDiagnosis.DiagnoseValue.Value, Is.False);
            Assert.That(memberDiagnose.InputValueDiagnosis.CurrentValue.Value, Is.True);

            Assert.That(memberDiagnose.OutputValueDiagnosis.DiagnoseActive.Value, Is.True);
            Assert.That(memberDiagnose.OutputValueDiagnosis.OriginalValue.Value, Is.True);
            Assert.That(memberDiagnose.OutputValueDiagnosis.DiagnoseValue.Value, Is.False);
            Assert.That(memberDiagnose.OutputValueDiagnosis.CurrentValue.Value, Is.False);
            
            Assert.That(_testee.TestProperty, Is.False);
        });
        
        _testee.TestProperty = false;
        Assert.Multiple(() =>
        {
            Assert.That(memberDiagnose.InputValueDiagnosis.DiagnoseActive.Value, Is.False);
            Assert.That(memberDiagnose.InputValueDiagnosis.OriginalValue.Value, Is.False);
            Assert.That(memberDiagnose.InputValueDiagnosis.DiagnoseValue.Value, Is.False);
            Assert.That(memberDiagnose.InputValueDiagnosis.CurrentValue.Value, Is.False);
            
            Assert.That(memberDiagnose.OutputValueDiagnosis.DiagnoseActive.Value, Is.True);
            Assert.That(memberDiagnose.OutputValueDiagnosis.OriginalValue.Value, Is.False);
            Assert.That(memberDiagnose.OutputValueDiagnosis.DiagnoseValue.Value, Is.False);
            Assert.That(memberDiagnose.OutputValueDiagnosis.CurrentValue.Value, Is.False);
            
            Assert.That(_testee.TestProperty, Is.False);
        });
        
        memberDiagnose.OutputValueDiagnosis.DiagnoseValue.Value = true;
        Assert.Multiple(() =>
        {
            Assert.That(memberDiagnose.InputValueDiagnosis.DiagnoseActive.Value, Is.False);
            Assert.That(memberDiagnose.InputValueDiagnosis.OriginalValue.Value, Is.False);
            Assert.That(memberDiagnose.InputValueDiagnosis.DiagnoseValue.Value, Is.False);
            Assert.That(memberDiagnose.InputValueDiagnosis.CurrentValue.Value, Is.False);

            Assert.That(memberDiagnose.OutputValueDiagnosis.DiagnoseActive.Value, Is.True);
            Assert.That(memberDiagnose.OutputValueDiagnosis.OriginalValue.Value, Is.False);
            Assert.That(memberDiagnose.OutputValueDiagnosis.DiagnoseValue.Value, Is.True);
            Assert.That(memberDiagnose.OutputValueDiagnosis.CurrentValue.Value, Is.True);
            
            Assert.That(_testee.TestProperty, Is.True);
        });
        
        memberDiagnose.OutputValueDiagnosis.DiagnoseActive.Value = false;
        Assert.Multiple(() =>
        {
            Assert.That(memberDiagnose.InputValueDiagnosis.DiagnoseActive.Value, Is.False);
            Assert.That(memberDiagnose.InputValueDiagnosis.OriginalValue.Value, Is.False);
            Assert.That(memberDiagnose.InputValueDiagnosis.DiagnoseValue.Value, Is.False);
            Assert.That(memberDiagnose.InputValueDiagnosis.CurrentValue.Value, Is.False);

            Assert.That(memberDiagnose.OutputValueDiagnosis.DiagnoseActive.Value, Is.False);
            Assert.That(memberDiagnose.OutputValueDiagnosis.OriginalValue.Value, Is.False);
            Assert.That(memberDiagnose.OutputValueDiagnosis.DiagnoseValue.Value, Is.True);
            Assert.That(memberDiagnose.OutputValueDiagnosis.CurrentValue.Value, Is.False);
            
            Assert.That(_testee.TestProperty, Is.False);
        });
    }
    
    [Test]
    public void Test_InputAndOutputDiagnoseAtTheSameTime()
    {
        var memberDiagnose = 
            _testee.ObjectDiagnosis.GetMemberDiagnose<bool>(nameof(_testee.TestProperty));
        Assert.That(memberDiagnose, Is.Not.Null);
        
        Assert.Multiple(() =>
        {
            Assert.That(memberDiagnose!.InputValueDiagnosis.DiagnoseActive.Value, Is.False);
            Assert.That(memberDiagnose.InputValueDiagnosis.OriginalValue.Value, Is.False);
            Assert.That(memberDiagnose.InputValueDiagnosis.DiagnoseValue.Value, Is.False);
            Assert.That(memberDiagnose.InputValueDiagnosis.CurrentValue.Value, Is.False);
            
            Assert.That(memberDiagnose.OutputValueDiagnosis.DiagnoseActive.Value, Is.False);
            Assert.That(memberDiagnose.OutputValueDiagnosis.OriginalValue.Value, Is.False);
            Assert.That(memberDiagnose.OutputValueDiagnosis.DiagnoseValue.Value, Is.False);
            Assert.That(memberDiagnose.OutputValueDiagnosis.CurrentValue.Value, Is.False);
            
            Assert.That(_testee.TestProperty, Is.False);
        });
        
        _testee.TestProperty = true;
        memberDiagnose!.InputValueDiagnosis.DiagnoseActive.Value = true;
        _testee.TestProperty = false;
        memberDiagnose.InputValueDiagnosis.DiagnoseValue.Value = true;
        memberDiagnose.OutputValueDiagnosis.DiagnoseActive.Value = true;
        memberDiagnose.OutputValueDiagnosis.DiagnoseValue.Value = false;
        Assert.Multiple(() =>
        {
            Assert.That(memberDiagnose.InputValueDiagnosis.DiagnoseActive.Value, Is.True);
            Assert.That(memberDiagnose.InputValueDiagnosis.OriginalValue.Value, Is.False);
            Assert.That(memberDiagnose.InputValueDiagnosis.DiagnoseValue.Value, Is.True);
            Assert.That(memberDiagnose.InputValueDiagnosis.CurrentValue.Value, Is.True);
            
            Assert.That(memberDiagnose.OutputValueDiagnosis.DiagnoseActive.Value, Is.True);
            Assert.That(memberDiagnose.OutputValueDiagnosis.OriginalValue.Value, Is.True);
            Assert.That(memberDiagnose.OutputValueDiagnosis.DiagnoseValue.Value, Is.False);
            Assert.That(memberDiagnose.OutputValueDiagnosis.CurrentValue.Value, Is.False);

            Assert.That(_testee.TestProperty, Is.False);
        });
        
        memberDiagnose.OutputValueDiagnosis.DiagnoseValue.Value = true;
        memberDiagnose.InputValueDiagnosis.DiagnoseActive.Value = false;
        Assert.Multiple(() =>
        {
            Assert.That(memberDiagnose.InputValueDiagnosis.DiagnoseActive.Value, Is.False);
            Assert.That(memberDiagnose.InputValueDiagnosis.OriginalValue.Value, Is.False);
            Assert.That(memberDiagnose.InputValueDiagnosis.DiagnoseValue.Value, Is.True);
            Assert.That(memberDiagnose.InputValueDiagnosis.CurrentValue.Value, Is.False);
            
            Assert.That(memberDiagnose.OutputValueDiagnosis.DiagnoseActive.Value, Is.True);
            Assert.That(memberDiagnose.OutputValueDiagnosis.OriginalValue.Value, Is.False);
            Assert.That(memberDiagnose.OutputValueDiagnosis.DiagnoseValue.Value, Is.True);
            Assert.That(memberDiagnose.OutputValueDiagnosis.CurrentValue.Value, Is.True);

            Assert.That(_testee.TestProperty, Is.True);
        });
    }
}