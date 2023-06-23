using System.ComponentModel;
using System.Diagnostics;
using RuntimeDiagnosis.Core.ObjectDiagnose.MemberDiagnose.DirectionValue;
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

    private event EventHandler? OriginalOutputValueMightChanged
    {
        add => InputValue.CurrentValue.ValueChangedUnified += value;
        remove => InputValue.CurrentValue.ValueChangedUnified -= value;
    }

    public MemberDiagnose(in IObjectDiagnose<TOwnerType> objectDiagnose,
        in string memberName, 
        Action<IMemberDiagnose> invokeOwnerPropertyChanged,
        Func<TMemberValueType?> getOriginalOutputValue,
        Action<TMemberValueType?> setCurrentInputValue)
    {
        _invokeOwnerPropertyChanged = invokeOwnerPropertyChanged;
        ObjectDiagnose = objectDiagnose;
        MemberName = memberName;
        InputValue = new InputValue<TOwnerType, TMemberValueType?>(this, setCurrentInputValue);
        OutputValue = new OutputValue<TOwnerType, TMemberValueType?>(this, InvokeOwnerPropertyChanged,
            getOriginalOutputValue,
            originalOutputValueMightChangedHandler => 
                OriginalOutputValueMightChanged += originalOutputValueMightChangedHandler);
    }
    
    public override string ToString() =>
        $"{GetType().GetNameWithoutGenericArity()} for {MemberName} of " +
        $"{ObjectDiagnose.GetOwnerTypeString()}";

    public string ToCurrentValueString() =>
        $"{InputValue.ToCurrentValueString()}, {OutputValue.ToCurrentValueString()}";
    
    private void InvokeOwnerPropertyChanged() =>
        _invokeOwnerPropertyChanged(this);
}