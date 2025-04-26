using Newtonsoft.Json;

namespace expense_tracker.CLI
{
    public class JsonData<T>
    {
        private string filePath = $@"{Directory.GetCurrentDirectory()}\{typeof(T).Name}.json";
        public async Task<List<T>> GetJsonData()
        {
            List<T> data = new List<T>();
            if (!File.Exists(filePath))
            {
                var jsonData = JsonConvert.SerializeObject(data, Formatting.Indented);
                await File.WriteAllTextAsync(filePath,jsonData);
            }
            else
            {
                var jsonData = await File.ReadAllTextAsync(filePath);
                if (!string.IsNullOrWhiteSpace(jsonData))
                {
                    data = JsonConvert.DeserializeObject<List<T>>(jsonData) ?? new List<T>();
                }
            }
            return data;
        }

        public async Task SaveJasonData(List<T> collection)
        {
            var json = JsonConvert.SerializeObject(collection, Formatting.Indented);
            await File.WriteAllTextAsync(filePath, json);
        }
    }
}
