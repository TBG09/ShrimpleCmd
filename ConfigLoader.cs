using System;
using System.IO;
using System.Text.Json;
using System.Text.Json.Nodes;


namespace ConfigHandler {
public class ConfigLoader
{
    private readonly string _configFilePath = "config.json";
    private JsonObject _configData;

    public ConfigLoader()
    {
        if (File.Exists(_configFilePath))
        {
            try
            {
                string jsonContent = File.ReadAllText(_configFilePath);
                _configData = JsonSerializer.Deserialize<JsonObject>(jsonContent);
                if (_configData == null)
                    throw new Exception("Deserialization returned null");
            }
            catch (Exception)
            {
                Console.WriteLine("Error parsing config.json. Resetting to default...");
                ResetConfig();
            }
        }
        else
        {
            Console.WriteLine("config.json does not exist. Creating a new one...");
            ResetConfig();
        }
    }

    private void ResetConfig()
    {
        _configData = new JsonObject
        {
            ["ModLoading"] = false
        };

        SaveConfig();
    }

    private void SaveConfig()
    {
        string jsonString = JsonSerializer.Serialize(_configData, new JsonSerializerOptions
        {
            WriteIndented = true
        });
        File.WriteAllText(_configFilePath, jsonString);
    }

    public string GetObjectValue(string key)
    {
        if (_configData != null && _configData.ContainsKey(key))
        {
            return _configData[key]?.ToString() ?? "null";
        }
        return $"Object '{key}' does not exist.";
    }

    public void SetValue(string key, string type, string value)
    {
        if (_configData == null)
            throw new InvalidOperationException("Configuration data is not initialized.");

        if (type == "string")
        {
            if (value == null)
            {
                _configData[key] = JsonValue.Create((string?)null);
            }
            else
            {
                _configData[key] = value;
            }
        }
        else if (type == "int")
        {
            if (value == null)
            {
                _configData[key] = JsonValue.Create((int?)null);
            }
            else if (int.TryParse(value, out int intValue))
            {
                _configData[key] = intValue;
            }
            else
            {
                throw new ArgumentException("Invalid value for type 'int'.");
            }
        }
        else
        {
            throw new ArgumentException("Invalid type specified. Only 'string' and 'int' are supported.");
        }

        SaveConfig();
    }


}

}
