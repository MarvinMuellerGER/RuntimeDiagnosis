using System.Diagnostics;
using System.Linq.Expressions;
using RuntimeDiagnosis.Core.ObjectDiagnosis.MemberDiagnosis.DirectionValueDiagnosis;
using RuntimeDiagnosis.Core.ObjectDiagnosis.MemberDiagnosis.DirectionValueDiagnosis.Kit;
using RuntimeDiagnosis.Core.ObjectDiagnosis.MemberDiagnosis.Kit;
using RuntimeDiagnosis.Kit;

namespace RuntimeDiagnosis.Core.ObjectDiagnosis.MemberDiagnosis;

[DebuggerDisplay("{ToString()} ({ToCurrentValueString()})")]
public sealed class MemberDiagnosis<TOwnerType, TMemberValueType> : IMemberDiagnosisInternal<TOwnerType, TMemberValueType?>
    where TOwnerType : IDiagnosableObject
{
    private readonly IMemberAccessorInternal<TMemberValueType?> _memberAccessor;
    private readonly IInputValueDiagnosisInternal<TOwnerType, TMemberValueType?> _inputValueDiagnosis;
    private readonly IOutputValueDiagnosisInternal<TOwnerType, TMemberValueType?> _outputValueDiagnosis;

    TMemberValueType? IMemberDiagnosis<TMemberValueType?>.MemberValue
    {
        get => _memberAccessor.Value;
        set => _memberAccessor.Value = value;
    }
    
    IObjectDiagnosis IMemberDiagnosis.ObjectDiagnosis => ObjectDiagnosis;

    public IObjectDiagnosis<TOwnerType> ObjectDiagnosis { get; private set; } = null!;

    public string MemberName { get; private set; } = null!;
    
    IInputValueDiagnosis IMemberDiagnosis.InputValueDiagnosis => InputValueDiagnosis;

    IInputValueDiagnosis<TMemberValueType?> IMemberDiagnosis<TMemberValueType?>.InputValueDiagnosis => 
        InputValueDiagnosis;

    public IInputValueDiagnosis<TOwnerType, TMemberValueType?> InputValueDiagnosis => _inputValueDiagnosis;

    IOutputValueDiagnosis IMemberDiagnosis.OutputValueDiagnosis => OutputValueDiagnosis;

    IOutputValueDiagnosis<TMemberValueType?> IMemberDiagnosis<TMemberValueType?>.OutputValueDiagnosis => 
        OutputValueDiagnosis;

    public IOutputValueDiagnosis<TOwnerType, TMemberValueType?> OutputValueDiagnosis => _outputValueDiagnosis;

    IEnumerable<IDirectionValueDiagnosis> IMemberDiagnosis.DirectionValues => DirectionValues;
    
    IEnumerable<IDirectionValueDiagnosis<TMemberValueType?>> IMemberDiagnosis<TMemberValueType?>.DirectionValues => 
        DirectionValues;

    public IEnumerable<IDirectionValueDiagnosis<TOwnerType, TMemberValueType?>> DirectionValues { get; }

    public MemberDiagnosis(in IMemberAccessorInternal<TMemberValueType?> memberAccessor,
        in IInputValueDiagnosisInternal<TOwnerType, TMemberValueType?> inputValueDiagnosis,
        in IOutputValueDiagnosisInternal<TOwnerType, TMemberValueType?> outputValueDiagnosis)
    {
        _memberAccessor = memberAccessor;
        _inputValueDiagnosis = inputValueDiagnosis;
        _outputValueDiagnosis = outputValueDiagnosis;
        
        DirectionValues = new IDirectionValueDiagnosis<TOwnerType, TMemberValueType?>[]
        {
            InputValueDiagnosis,
            OutputValueDiagnosis
        };
    }
    
    public void Initialize(in IObjectDiagnosis<TOwnerType> objectDiagnosis, in string memberName,
        Expression<Func<TMemberValueType?>> memberExpression,
        IEnumerable<DirectionValueDefinition> inputCallerDefinitions, 
        IEnumerable<DirectionValueDefinition> outputCallerDefinitions)
    {
        ObjectDiagnosis = objectDiagnosis;
        MemberName = memberName;
        
        _memberAccessor.Initialize(memberExpression);
        _inputValueDiagnosis.Initialize(this, inputCallerDefinitions);
        _outputValueDiagnosis.Initialize(this, outputCallerDefinitions, 
            inputValueChanged =>
                InputValueDiagnosis.CurrentValue.ValueChangedUnified += inputValueChanged);
    }

    public override string ToString() =>
        $"{this.GetTypeNameWithoutGenericArity()} for {MemberName} of " +
        $"{ObjectDiagnosis.GetOwnerTypeString()}";

    public string ToCurrentValueString() =>
        $"{InputValueDiagnosis.ToCurrentValueString()}, {OutputValueDiagnosis.ToCurrentValueString()}";
}