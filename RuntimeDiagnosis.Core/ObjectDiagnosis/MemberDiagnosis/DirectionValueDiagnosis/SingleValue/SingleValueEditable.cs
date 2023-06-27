namespace RuntimeDiagnosis.Core.ObjectDiagnosis.MemberDiagnosis.DirectionValueDiagnosis.SingleValue;

public sealed class SingleValueEditable<TOwnerType, TMemberValueType, TValueType> : 
    SingleValueAlwaysEditable<TOwnerType, TMemberValueType?, TValueType?>, 
    ISingleValueEditable<TOwnerType, TMemberValueType?, TValueType?>
    where TOwnerType : IDiagnosableObject
{
    public new bool EditingCurrentlyAllowed
    {
        get => base.EditingCurrentlyAllowed;
        internal set => base.EditingCurrentlyAllowed = value;
    }

    public SingleValueEditable(IDirectionValueDiagnosis<TOwnerType, TMemberValueType?> directionValueDiagnosis, 
        string name, bool editingCurrentlyAllowed = false) :
        base(directionValueDiagnosis, name, editingCurrentlyAllowed)
    { }
}