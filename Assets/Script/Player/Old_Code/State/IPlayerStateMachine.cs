public interface IPlayerStateMachine
{
    public IPlayerStateTemp state {get;}
    public void Init();
    public void Enter(PlayerInputContext context);
    public void Update();
    public void Exit();
}