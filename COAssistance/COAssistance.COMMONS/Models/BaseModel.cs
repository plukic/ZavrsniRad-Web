namespace COAssistance.COMMONS.Models
{
    public class BaseModel<TKey>
    {
        public virtual TKey Id { get; set; }
    }
}