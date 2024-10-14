public interface IPlayerStateController
{
    public IPlayerStateMachine stateMachine{get;}
    public void Init();
    public void Update();
}