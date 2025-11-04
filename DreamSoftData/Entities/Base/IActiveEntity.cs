namespace DreamSoftData.Entities.Base;

/// <summary>
/// Marker interface for entities that have an Active field.
/// </summary>
public interface IActiveEntity
{
    bool Active { get; set; }
}
