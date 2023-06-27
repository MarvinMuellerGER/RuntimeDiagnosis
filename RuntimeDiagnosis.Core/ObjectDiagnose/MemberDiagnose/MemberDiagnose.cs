using System.Diagnostics;
using RuntimeDiagnosis.Core.ObjectDiagnose.MemberDiagnose.DirectionValue;
using RuntimeDiagnosis.Core.ObjectDiagnose.MemberDiagnose.DirectionValue.Kit;
using RuntimeDiagnosis.Kit;

namespace RuntimeDiagnosis.Core.ObjectDiagnose.MemberDiagnose;

[DebuggerDisplay("{ToString()} ({ToCurrentValueString()})")]
public class MemberDiagnose<TOwnerType, TMemberValueType> : IMemberDiagnose<TOwnerType, TMemberValueType?>
    where TOwnerType : IDiagnosableObject
{
    private readonly Action<IMemberDiagnose> _invokeOwnerPropertyChanged;
    IObjectDiagnose IMemberDiagnose.ObjectDiagnose => ObjectDiagnose;

    public IObjectDiagnose<TOwnerType> ObjectDiagnose { get; }
    
    public string MemberName { get; }

    IInputValue IMemberDiagnose.InputValue => InputValue;

    IInputValue<TMemberValueType?> IMemberDiagnose<TMemberValueType?>.InputValue => InputValue;

    public IInputValue<TOwnerType, TMemberValueType?> InputValue { get; }

    IOutputValue IMemberDiagnose.OutputValue => OutputValue;

    IOutputValue<TMemberValueType?> IMemberDiagnose<TMemberValueType?>.OutputValue => OutputValue;

    public IOutputValue<TOwnerType, TMemberValueType?> OutputValue { get; }

    IEnumerable<IDirectionValue> IMemberDiagnose.DirectionValues => DirectionValues;
    
    IEnumerable<IDirectionValue<TMemberValueType?>> IMemberDiagnose<TMemberValueType?>.DirectionValues => 
        DirectionValues;

    public IEnumerable<IDirectionValue<TOwnerType, TMemberValueType?>> DirectionValues { get; }

    public MemberDiagnose(in IObjectDiagnose<TOwnerType> objectDiagnose,
        in string memberName,
        IEnumerable<DirectionValueDefinition> inputCallerDefinitions,
        IEnumerable<DirectionValueDefinition> outputCallerDefinitions,
        Action<IMemberDiagnose> invokeOwnerPropertyChanged,
        Func<TMemberValueType?> getOriginalOutputValue,
        Action<TMemberValueType?> setCurrentInputValue)
    {
        _invokeOwnerPropertyChanged = invokeOwnerPropertyChanged;
        ObjectDiagnose = objectDiagnose;
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
        $"{ObjectDiagnose.GetOwnerTypeString()}";

    public string ToCurrentValueString() =>
        $"{InputValue.ToCurrentValueString()}, {OutputValue.ToCurrentValueString()}";
    
    private void InvokeOwnerPropertyChanged() =>
        _invokeOwnerPropertyChanged(this);
}