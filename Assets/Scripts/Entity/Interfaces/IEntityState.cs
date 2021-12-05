internal interface IEntityState
{
    void Act();
    void Close();
    void Initialize();
    void Sense();
    void Think();
    void Update()
    {
        Sense();
        Think();
        Act();
    }
}