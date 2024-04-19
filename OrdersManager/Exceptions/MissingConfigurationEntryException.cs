namespace OrdersManager.Exceptions
{
    [Serializable]
    public class MissingConfigurationEntryException : Exception
    {
        public MissingConfigurationEntryException() : base() { }
        public MissingConfigurationEntryException(string message) : base(message) { }
    }
}
