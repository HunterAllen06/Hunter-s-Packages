namespace HunterAllen.SaveSystem
{
    public interface IDataHandler
    {
        public void Save<T>(T t, string key);
        public T Load<T>(string key);
    }
}