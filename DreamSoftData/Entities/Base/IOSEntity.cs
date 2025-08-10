namespace DreamSoftData.Entities.Base
{
    public interface IOSEntity<T>:IEntity<T> where T : notnull
    {
        int AccountId { get; set; }
    }
}
