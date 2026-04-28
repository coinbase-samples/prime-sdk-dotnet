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
  /// A JSON converter for enums that returns the default enum value (0) when an unknown string is
  /// encountered, rather than throwing a <see cref="JsonException"/>. Useful when the API may return
  /// new role or status values not yet present in the SDK enum.
  /// </summary>
  /// <typeparam name="T">The enum type to convert.</typeparam>
  public class LenientJsonStringEnumConverter<T> : JsonConverter<T>
    where T : struct, Enum
  {
    /// <inheritdoc/>
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

    /// <inheritdoc/>
    public override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options)
    {
      writer.WriteStringValue(value.ToString());
    }
  }
}
