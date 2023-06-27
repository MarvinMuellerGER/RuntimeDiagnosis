using System.Diagnostics;
using RuntimeDiagnosis.Core.ObjectDiagnosis.MemberDiagnosis.DirectionValue;
using RuntimeDiagnosis.Core.ObjectDiagnosis.MemberDiagnosis.DirectionValue.Kit;
using RuntimeDiagnosis.Kit;

namespace RuntimeDiagnosis.Core.ObjectDiagnosis.MemberDiagnosis;

[DebuggerDisplay("{ToString()} ({ToCurrentValueString()})")]
public class MemberDiagnosis<TOwnerType, TMemberValueType> : IMemberDiagnosis<TOwnerType, TMemberValueType?>
    where TOwnerType : IDiagnosableObject
{
    private readonly Action<IMemberDiagnosis> _invokeOwnerPropertyChanged;
    IObjectDiagnosis IMemberDiagnosis.ObjectDiagnosis => ObjectDiagnosis;

    public IObjectDiagnosis<TOwnerType> ObjectDiagnosis { get; }
    
    public string MemberName { get; }

    IInputValue IMemberDiagnosis.InputValue => InputValue;

    IInputValue<TMemberValueType?> IMemberDiagnosis<TMemberValueType?>.InputValue => InputValue;

    public IInputValue<TOwnerType, TMemberValueType?> InputValue { get; }

    IOutputValue IMemberDiagnosis.OutputValue => OutputValue;

    IOutputValue<TMemberValueType?> IMemberDiagnosis<TMemberValueType?>.OutputValue => OutputValue;

    public IOutputValue<TOwnerType, TMemberValueType?> OutputValue { get; }

    IEnumerable<IDirectionValue> IMemberDiagnosis.DirectionValues => DirectionValues;
    
    IEnumerable<IDirectionValue<TMemberValueType?>> IMemberDiagnosis<TMemberValueType?>.DirectionValues => 
        DirectionValues;

    public IEnumerable<IDirectionValue<TOwnerType, TMemberValueType?>> DirectionValues { get; }

    public MemberDiagnosis(in IObjectDiagnosis<TOwnerType> objectDiagnosis,
        in string memberName,
        IEnumerable<DirectionValueDefinition> inputCallerDefinitions,
        IEnumerable<DirectionValueDefinition> outputCallerDefinitions,
        Action<IMemberDiagnosis> invokeOwnerPropertyChanged,
        Func<TMemberValueType?> getOriginalOutputValue,
        Action<TMemberValueType?> setCurrentInputValue)
    {
        _invokeOwnerPropertyChanged = invokeOwnerPropertyChanged;
        ObjectDiagnosis = objectDiagnosis;
        MemberName = memberName;
        InputValue = new InputValue<TOwnerType, TMemberValueType?>(
            this, inputCallerDefinitions, setCurrentInputValue);
        OutputValue = new OutputValue<TOwnerType, TMemberValueType?>(
            this, outputCallerDefinitions, InvokeOwnerPropertyChanged,
            getOriginalOutputValue,
            inputValueChanged => 
                InputValue.CurrentValue.ValueChangedUnified += inputValueChanged);
        DirectionValues = new IDirectionValue<TOwnerType, TMemberValueType?>[] { InputValue, OutputValue };
    }
    
    public override string ToString() =>
        $"{GetType().GetNameWithoutGenericArity()} for {MemberName} of " +
        $"{ObjectDiagnosis.GetOwnerTypeString()}";

    public string ToCurrentValueString() =>
        $"{InputValue.ToCurrentValueString()}, {OutputValue.ToCurrentValueString()}";
    
    private void InvokeOwnerPropertyChanged() =>
        _invokeOwnerPropertyChanged(this);
}