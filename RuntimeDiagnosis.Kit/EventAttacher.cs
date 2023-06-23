namespace RuntimeDiagnosis.Kit;

public static class EventAttacher
{
    public static void AttachToEvent(Action<EventHandler>? attach, EventHandler eventHandler) => 
        attach?.Invoke(eventHandler);
    
    public static void AttachToEvent<TValueType>(
        Action<EventHandler<TValueType>>? attach, EventHandler<TValueType> eventHandler) => 
        attach?.Invoke(eventHandler);
}