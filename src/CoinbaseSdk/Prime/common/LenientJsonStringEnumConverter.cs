/*
 * Copyright 2026-present Coinbase Global, Inc.
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 * http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

namespace CoinbaseSdk.Prime.Common
{
  using System.Text.Json;
  using System.Text.Json.Serialization;

  /// <summary>
  /// A <see cref="JsonConverterFactory"/> for enum types that returns <c>null</c> (for nullable
  /// fields) or skips (for non-nullable fields) when an unrecognized string value is encountered,
  /// rather than throwing a <see cref="JsonException"/>. This matches the behavior of the Java SDK
  /// which uses <c>READ_UNKNOWN_ENUM_VALUES_AS_NULL</c>.
  /// </summary>
  /// <typeparam name="T">The enum type to convert.</typeparam>
  public class LenientJsonStringEnumConverter<T> : JsonConverterFactory
    where T : struct, Enum
  {
    /// <inheritdoc/>
    public override bool CanConvert(Type typeToConvert)
    {
      return typeToConvert == typeof(T) || typeToConvert == typeof(T?);
    }

    /// <inheritdoc/>
    public override JsonConverter? CreateConverter(Type typeToConvert, JsonSerializerOptions options)
    {
      if (typeToConvert == typeof(T?))
      {
        return new NullableEnumConverter();
      }

      return new NonNullableEnumConverter();
    }

    private sealed class NullableEnumConverter : JsonConverter<T?>
    {
      public override T? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
      {
        if (reader.TokenType == JsonTokenType.Null)
        {
          return null;
        }

        if (reader.TokenType == JsonTokenType.String)
        {
          var str = reader.GetString();
          if (str != null && Enum.TryParse<T>(str, ignoreCase: false, out var result))
          {
            return result;
          }

          return null;
        }

        if (reader.TokenType == JsonTokenType.Number && reader.TryGetInt32(out var intValue))
        {
          return (T)(object)intValue;
        }

        return null;
      }

      public override void Write(Utf8JsonWriter writer, T? value, JsonSerializerOptions options)
      {
        if (value.HasValue)
        {
          writer.WriteStringValue(value.Value.ToString());
        }
        else
        {
          writer.WriteNullValue();
        }
      }
    }

    private sealed class NonNullableEnumConverter : JsonConverter<T>
    {
      public override T Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
      {
        if (reader.TokenType == JsonTokenType.String)
        {
          var str = reader.GetString();
          if (str != null && Enum.TryParse<T>(str, ignoreCase: false, out var result))
          {
            return result;
          }

          return default;
        }

        if (reader.TokenType == JsonTokenType.Number && reader.TryGetInt32(out var intValue))
        {
          return (T)(object)intValue;
        }

        return default;
      }

      public override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options)
      {
        writer.WriteStringValue(value.ToString());
      }
    }
  }
}
