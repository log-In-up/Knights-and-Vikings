namespace Entity.Interfaces
{
    internal interface IEntityState
    {
        /// <summary>
        /// The method that is responsible for "action" based on AI observation and deliberation.
        /// </summary>
        void Act();

        /// <summary>
        /// The method that is responsible for closing the state of the AI.
        /// </summary>
        void Close();

        /// <summary>
        /// The method that is responsible for opening the state of the AI.
        /// </summary>
        void Initialize();

        /// <summary>
        /// The method that is responsible for "observing" the state of the game.
        /// </summary>
        void Sense();

        /// <summary>
        /// The method that is responsible for "thinking" of its own state.
        /// </summary>
        void Think();

        /// <summary>
        /// The default method calls first <see cref="Sense()"/>, then <see cref="Think()"/> and then <see cref="Act()"/>.
        /// </summary>
        void Update()
        {
            Sense();
            Think();
            Act();
        }
    }
}