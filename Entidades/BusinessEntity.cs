namespace Entidades
{
    public class BusinessEntity
    {
        public int ID { get; private set; }
        public string State { get; private set; } = "Sin Estado";

        public void SetId(int id)
        {
            ID = id;
        }
        public void SetState(string state)
        {
            State = state;
        }
    }
}
