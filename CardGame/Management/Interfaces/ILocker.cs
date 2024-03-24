namespace SamMRoberts.CardGame.Management
{
    public interface ILocker<ILockable>
    {
        void Lock(ILockable obj);
        void Unlock(ILockable obj);
    }
}