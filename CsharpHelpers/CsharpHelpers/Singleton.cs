namespace CsharpHelpers
{
    public static class Singleton<T>
        where T : new()
    {
        private static T _instance;
        public static T Instance => _instance == null ? (_instance = new T()) : _instance;
    }
}
