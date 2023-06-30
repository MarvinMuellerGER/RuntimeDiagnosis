using System.Diagnostics;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using RuntimeDiagnosis.Core.ObjectDiagnosis.MemberDiagnosis;
using RuntimeDiagnosis.Core.ObjectDiagnosis.MemberDiagnosis.DirectionValueDiagnosis.Kit;
using RuntimeDiagnosis.Core.ObjectDiagnosis.MemberDiagnosis.Kit;
using RuntimeDiagnosis.Kit;

namespace RuntimeDiagnosis.Core.ObjectDiagnosis;
 
[DebuggerDisplay("{ToString()}")]
public class ObjectDiagnosis<TOwnerType> : IObjectDiagnosis<TOwnerType>
    where TOwnerType : IDiagnosableObject
{
    private readonly Action<string> _invokeOwnerPropertyChanged;

    IDiagnosableObject IObjectDiagnosis.Owner => Owner;
    
    public TOwnerType Owner { get; }

    public Type OwnerBaseType => Owner.GetType().BaseType ?? Owner.GetType();

    [DebuggerDisplay($"{nameof(MemberDiagnoses)} for {{GetOwnerTypeString()}}")]
    public IEnumerable<IMemberDiagnosis> MemberDiagnoses { get; }

    public ObjectDiagnosis(TOwnerType owner,
        Action<string> invokePropertyChanged,
        params Func<ObjectDiagnosis<TOwnerType>, IMemberDiagnosis>[] createMemberDiagnoseActions)
    {
        Owner = owner;
        MemberDiagnoses = createMemberDiagnoseActions
            .Select(createMemberDiagnose => createMemberDiagnose(this)).ToList();
        _invokeOwnerPropertyChanged = invokePropertyChanged;
        ObjectDiagnosesManager.AddNewObjectDiagnose(this);
    }

    public override string ToString() =>
        $"{typeof(ObjectDiagnosis<TOwnerType>).GetNameWithoutGenericArity()} for {GetOwnerTypeString()}";

    public string GetOwnerTypeString() =>
        $"{OwnerBaseType.Name} object";

    public IMemberDiagnosis? GetMemberDiagnose([CallerMemberName] string memberName = "") =>
        MemberDiagnoses.FirstOrDefault(md => md.MemberName == memberName);

    IMemberDiagnosis<TMemberValueType?>? IObjectDiagnosis.GetMemberDiagnose<TMemberValueType>(
        string memberName) where TMemberValueType : default =>
        GetMemberDiagnose(memberName) as IMemberDiagnosis<TMemberValueType?>;

    public IMemberDiagnosis<TOwnerType, TMemberValueType?>? GetMemberDiagnose<TMemberValueType>(
        [CallerMemberName] string memberName = "") =>
        GetMemberDiagnose(memberName) as MemberDiagnosis<TOwnerType, TMemberValueType?>;
    
    public TMemberValueType? GetCurrentOutputMemberValue<TMemberValueType>(
        in Expression<Func<TMemberValueType?>> memberExpression,
        [CallerMemberName] string memberName = "")
    {
        var memberDiagnose = GetMemberDiagnose<TMemberValueType>(memberName);
        return memberDiagnose == null ? 
            new MemberAccessor<TMemberValueType>(memberExpression).Value : 
            memberDiagnose.OutputValueDiagnosis.Value;
    }
    
    public void SetOriginalInputMemberValue<TMemberValueType>(in Expression<Func<TMemberValueType?>> memberExpression, 
        in TMemberValueType? value, [CallerMemberName] string memberName = "")
    {
        var memberDiagnose = GetMemberDiagnose<TMemberValueType?>(memberName);
        if (memberDiagnose == null)
        {
            new MemberAccessor<TMemberValueType>(memberExpression).Value = value;
            return;
        }
        memberDiagnose.InputValueDiagnosis.Value = value;
    }

    public IMemberDiagnosis CreateMemberDiagnosis<TMemberValueType>(
        in string memberName,
        Expression<Func<TMemberValueType?>> memberExpression, 
        IEnumerable<DirectionValueDefinition> inputCallerDefinitions, 
        IEnumerable<DirectionValueDefinition> outputCallerDefinitions) =>
        new MemberDiagnosis<TOwnerType, TMemberValueType>(
                this, memberName, memberExpression, inputCallerDefinitions, outputCallerDefinitions,
                InvokeOwnerPropertyChanged);

    private void InvokeOwnerPropertyChanged(IMemberDiagnosis memberDiagnosis) =>
        _invokeOwnerPropertyChanged(memberDiagnosis.MemberName);
}