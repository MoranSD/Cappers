namespace Utils.StateMachine
{
    public interface IPayloadedEnterableState<T>
    {
        void Enter(T payload);
    }
}
