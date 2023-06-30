using System.Diagnostics;
using System.Linq.Expressions;
using RuntimeDiagnosis.Core.ObjectDiagnosis.MemberDiagnosis.DirectionValueDiagnosis;
using RuntimeDiagnosis.Core.ObjectDiagnosis.MemberDiagnosis.DirectionValueDiagnosis.Kit;
using RuntimeDiagnosis.Core.ObjectDiagnosis.MemberDiagnosis.Kit;
using RuntimeDiagnosis.Kit;

namespace RuntimeDiagnosis.Core.ObjectDiagnosis.MemberDiagnosis;

[DebuggerDisplay("{ToString()} ({ToCurrentValueString()})")]
public class MemberDiagnosis<TOwnerType, TMemberValueType> : IMemberDiagnosis<TOwnerType, TMemberValueType?>
    where TOwnerType : IDiagnosableObject
{
    private readonly MemberAccessor<TMemberValueType> _memberAccessor;
    private readonly Action<IMemberDiagnosis> _invokeOwnerPropertyChanged;

    TMemberValueType? IMemberDiagnosis<TMemberValueType?>.MemberValue
    {
        get => _memberAccessor.Value;
        set => _memberAccessor.Value = value;
    }
    
    IObjectDiagnosis IMemberDiagnosis.ObjectDiagnosis => ObjectDiagnosis;

    public IObjectDiagnosis<TOwnerType> ObjectDiagnosis { get; }
    
    public string MemberName { get; }
    IInputValueDiagnosis IMemberDiagnosis.InputValueDiagnosis => InputValueDiagnosis;

    IInputValueDiagnosis<TMemberValueType?> IMemberDiagnosis<TMemberValueType?>.InputValueDiagnosis => InputValueDiagnosis;

    public IInputValueDiagnosis<TOwnerType, TMemberValueType?> InputValueDiagnosis { get; }

    IOutputValueDiagnosis IMemberDiagnosis.OutputValueDiagnosis => OutputValueDiagnosis;

    IOutputValueDiagnosis<TMemberValueType?> IMemberDiagnosis<TMemberValueType?>.OutputValueDiagnosis => OutputValueDiagnosis;

    public IOutputValueDiagnosis<TOwnerType, TMemberValueType?> OutputValueDiagnosis { get; }

    IEnumerable<IDirectionValueDiagnosis> IMemberDiagnosis.DirectionValues => DirectionValues;
    
    IEnumerable<IDirectionValueDiagnosis<TMemberValueType?>> IMemberDiagnosis<TMemberValueType?>.DirectionValues => 
        DirectionValues;

    public IEnumerable<IDirectionValueDiagnosis<TOwnerType, TMemberValueType?>> DirectionValues { get; }

    public MemberDiagnosis(in IObjectDiagnosis<TOwnerType> objectDiagnosis,
        in string memberName,
        Expression<Func<TMemberValueType?>> memberExpression,
        IEnumerable<DirectionValueDefinition> inputCallerDefinitions,
        IEnumerable<DirectionValueDefinition> outputCallerDefinitions,
        Action<IMemberDiagnosis> invokeOwnerPropertyChanged)
    {
        ObjectDiagnosis = objectDiagnosis;
        MemberName = memberName;
        _memberAccessor = new MemberAccessor<TMemberValueType>(memberExpression);
        _invokeOwnerPropertyChanged = invokeOwnerPropertyChanged;
        
        InputValueDiagnosis = new InputValueDiagnosis<TOwnerType, TMemberValueType?>(
            this, inputCallerDefinitions);
        OutputValueDiagnosis = new OutputValueDiagnosis<TOwnerType, TMemberValueType?>(
            this, outputCallerDefinitions, InvokeOwnerPropertyChanged,
            inputValueChanged => 
                InputValueDiagnosis.CurrentValue.ValueChangedUnified += inputValueChanged);
        
        DirectionValues = new IDirectionValueDiagnosis<TOwnerType, TMemberValueType?>[]
        {
            InputValueDiagnosis,
            OutputValueDiagnosis
        };
    }
    
    public override string ToString() =>
        $"{GetType().GetNameWithoutGenericArity()} for {MemberName} of " +
        $"{ObjectDiagnosis.GetOwnerTypeString()}";

    public string ToCurrentValueString() =>
        $"{InputValueDiagnosis.ToCurrentValueString()}, {OutputValueDiagnosis.ToCurrentValueString()}";
    
    private void InvokeOwnerPropertyChanged() =>
        _invokeOwnerPropertyChanged(this);
}