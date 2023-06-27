using System.Diagnostics;
using System.Runtime.CompilerServices;
using RuntimeDiagnosis.Core.ObjectDiagnose.MemberDiagnose;
using RuntimeDiagnosis.Core.ObjectDiagnose.MemberDiagnose.DirectionValue.Kit;
using RuntimeDiagnosis.Kit;

namespace RuntimeDiagnosis.Core.ObjectDiagnose;
 
[DebuggerDisplay("{ToString()}")]
public class ObjectDiagnose<TOwnerType> : IObjectDiagnose<TOwnerType>
    where TOwnerType : IDiagnosableObject
{
    private readonly Action<string> _invokeOwnerPropertyChanged;

    IDiagnosableObject IObjectDiagnose.Owner => Owner;
    
    public TOwnerType Owner { get; }

    public Type OwnerBaseType => Owner.GetType().BaseType ?? Owner.GetType();

    [DebuggerDisplay($"{nameof(MemberDiagnoses)} for {{GetOwnerTypeString()}}")]
    public IEnumerable<IMemberDiagnose> MemberDiagnoses { get; internal set; } = null!;

    internal ObjectDiagnose(TOwnerType owner, Action<string> invokePropertyChanged)
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
    
    public TMemberValueType? GetCurrentOutputMemberValue<TMemberValueType>(
        in Func<TMemberValueType?> getMemberValue,
        [CallerMemberName] string memberName = "")
    {
        var memberDiagnose = GetMemberDiagnose<TMemberValueType>(memberName);
        return memberDiagnose == null ? getMemberValue() : memberDiagnose.OutputValue.Value;
    }
    
    public void SetOriginalInputMemberValue<TMemberValueType>(in Action<TMemberValueType?> setMemberValue, 
        in TMemberValueType? value, [CallerMemberName] string memberName = "")
    {
        var memberDiagnose = GetMemberDiagnose<TMemberValueType?>(memberName);
        if (memberDiagnose == null)
        {
            setMemberValue(value);
            return;
        }
        memberDiagnose.InputValue.Value = value;
    }

    public IMemberDiagnose CreateMemberDiagnosis<TMemberValueType>(
        in string memberName, 
        IEnumerable<DirectionValueDefinition> inputCallerDefinitions, 
        IEnumerable<DirectionValueDefinition> outputCallerDefinitions,
        Func<TMemberValueType?> getMemberValue,
        Action<TMemberValueType?> setMemberValue) =>
        new MemberDiagnose<TOwnerType, TMemberValueType>(
                this, memberName, inputCallerDefinitions, outputCallerDefinitions,
                InvokeOwnerPropertyChanged, getMemberValue, setMemberValue);

    private void InvokeOwnerPropertyChanged(IMemberDiagnose memberDiagnose) =>
        _invokeOwnerPropertyChanged(memberDiagnose.MemberName);
}