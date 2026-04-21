using System;
using Newtonsoft.Json;
using UnityEngine;

public class ItemDataConverter : JsonConverter<ItemData>
{
    public override ItemData ReadJson(JsonReader reader, Type objectType, ItemData existingValue, bool hasExistingValue, JsonSerializer serializer)
    {
        if (reader.TokenType == JsonToken.Null) return null;

        string id = reader.Value as string;
        return DataTableManager.ItemTable.Get(id);
    }

    public override void WriteJson(JsonWriter writer, ItemData value, JsonSerializer serializer)
    {
        if (value == null)
        {
            writer.WriteNull();
            return;
        }
        writer.WriteValue(value.Id);
    }
}
