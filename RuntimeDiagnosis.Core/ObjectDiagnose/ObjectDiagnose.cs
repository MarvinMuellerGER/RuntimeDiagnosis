using System.Diagnostics;
using System.Runtime.CompilerServices;
using RuntimeDiagnosis.Core.ObjectDiagnose.MemberDiagnose;
using RuntimeDiagnosis.Core.ObjectDiagnose.MemberDiagnose.DirectionValue.SingleValue;
using RuntimeDiagnosis.Kit;

namespace RuntimeDiagnosis.Core.ObjectDiagnose;

[DebuggerDisplay("{ToString()}")]
public class ObjectDiagnose<TOwnerType> : IObjectDiagnose<TOwnerType>
    where TOwnerType : IDiagnosableObject
{
    private readonly Action<string> _invokeOwnerPropertyChanged;
    
    object IObjectDiagnose.Owner => Owner;
    
    public TOwnerType Owner { get; }

    public Type OwnerBaseType => Owner.GetType().BaseType ?? Owner.GetType();

    [DebuggerDisplay($"{nameof(MemberDiagnoses)} for {{GetOwnerTypeString()}}")]
    public List<IMemberDiagnose> MemberDiagnoses { get; } = new();

    public ObjectDiagnose(TOwnerType owner, Action<string> invokePropertyChanged)
    {
        _invokeOwnerPropertyChanged = invokePropertyChanged;
        Owner = owner;
    }

    public override string ToString() =>
        $"{typeof(ObjectDiagnose<TOwnerType>).GetNameWithoutGenericArity()} for {GetOwnerTypeString()}";

    public string GetOwnerTypeString() =>
        $"{OwnerBaseType.Name} object";

    public IMemberDiagnose? GetMemberDiagnose([CallerMemberName] string memberName = "") =>
        MemberDiagnoses.FirstOrDefault(md => md.MemberName == memberName);

    IMemberDiagnose<TMemberValueType?>? IObjectDiagnose.GetMemberDiagnose<TMemberValueType>(
        string memberName) where TMemberValueType : default =>
        GetMemberDiagnose(memberName) as IMemberDiagnose<TMemberValueType?>;

    public IMemberDiagnose<TOwnerType, TMemberValueType?>? GetMemberDiagnose<TMemberValueType>(
        [CallerMemberName] string memberName = "") =>
        GetMemberDiagnose(memberName) as MemberDiagnose<TOwnerType, TMemberValueType?>;
    
    public TMemberValueType? GetCurrentMemberValue<TMemberValueType>(in TMemberValueType? internalProperty,
        [CallerMemberName] string memberName = "")
    {
        var memberDiagnose = GetMemberDiagnose<TMemberValueType>(memberName);
        return memberDiagnose == null ? internalProperty : memberDiagnose.OutputValue.CurrentValue.Value;
    }
    
    public void SetOriginalMemberValue<TMemberValueType>(in Action<TMemberValueType?> setMemberValue, 
        in TMemberValueType? value, [CallerMemberName] string memberName = "")
    {
        var memberDiagnose = GetMemberDiagnose<TMemberValueType?>(memberName);
        if (memberDiagnose == null)
        {
            setMemberValue(value);
            return;
        }
        (memberDiagnose.InputValue.OriginalValue as 
            SingleValue<TOwnerType, TMemberValueType?, TMemberValueType?>)!.Value = value;
    }

    public void AddMember<TMemberValueType>(
        in string memberName,
        Func<TMemberValueType?> getOriginalOutputValue,
        Action<TMemberValueType?> setCurrentInputValue) =>
        MemberDiagnoses.Add(
            CreateMemberDiagnose(memberName, getOriginalOutputValue, setCurrentInputValue));

    private MemberDiagnose<TOwnerType, TMemberValueType> CreateMemberDiagnose<TMemberValueType>(
        in string memberName,
        Func<TMemberValueType?> getOriginalOutputValue,
        Action<TMemberValueType?> setCurrentInputValue) =>
        new(this, memberName, InvokeOwnerPropertyChanged, getOriginalOutputValue, setCurrentInputValue);

    private void InvokeOwnerPropertyChanged(IMemberDiagnose memberDiagnose) =>
        _invokeOwnerPropertyChanged(memberDiagnose.MemberName);
}