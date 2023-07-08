namespace Code.Domain
{
    public readonly struct UserInfo
    {
        public string Name { get; }

        public UserInfo(string name)
        {
            Name = name;
        }
    }
}