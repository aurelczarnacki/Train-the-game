public interface IEntity
{
    bool isDeleted { get; set; }
    void OnMouseDown();
    void EntityDestroy();
}
