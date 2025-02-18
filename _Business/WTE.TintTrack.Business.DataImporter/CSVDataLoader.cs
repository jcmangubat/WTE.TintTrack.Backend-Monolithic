using CsvHelper.Configuration;
using CsvHelper;
using System.Globalization;
using System.Reflection;

namespace WTE.TintTrack.Business.DataImporter;

public static class CSVDataLoader
{
    public static List<TCSVDataModel> LoadCSV<TCSVDataModel>(string csvFile) where TCSVDataModel : class, new()
    {
        try
        {
            if (!File.Exists(csvFile))
                throw new FileNotFoundException(csvFile);

            using var reader = new StreamReader(csvFile);
            using var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HeaderValidated = null,
                MissingFieldFound = null,
            });

            var records = new List<TCSVDataModel>();
            var properties = typeof(TCSVDataModel).GetProperties()
                .Where(prop => Attribute.IsDefined(prop, typeof(CsvColumnAttribute)))
                .ToList();

            csv.Read();
            csv.ReadHeader();
            var headerRow = csv.HeaderRecord;

            while (csv.Read())
            {
                var record = new TCSVDataModel();

                foreach (var property in properties)
                {
                    var attribute = property.GetCustomAttribute<CsvColumnAttribute>();
                    if (attribute != null)
                    {
                        try
                        {
                            var columnName = attribute.Name;

                            if (headerRow.Contains(columnName))
                            {
                                var value = csv.GetField(columnName);
                                if (string.IsNullOrWhiteSpace(value))
                                {
                                    // Handle empty or null value for nullable types
                                    if (property.PropertyType.IsNullableType())
                                        property.SetValue(record, null); // Set null for nullable types
                                    else
                                        // Handle non-nullable types, use default values
                                        property.SetValue(record, GetDefaultValue(property.PropertyType));
                                }
                                else
                                {
                                    if (property.PropertyType == typeof(DateTime?) || property.PropertyType == typeof(DateTime))
                                    {
                                        // Try parsing the date string to DateTime?
                                        DateTime? parsedDate = null;
                                        if (DateTime.TryParse(value, out DateTime dateValue))
                                            parsedDate = dateValue;
                                        else
                                        {
                                        }
                                        property.SetValue(record, parsedDate);
                                    }
                                    else if (property.PropertyType == typeof(int?) || property.PropertyType == typeof(int))
                                    {
                                        // Handle Nullable Int (int?)
                                        int? parsedInt = null;
                                        if (int.TryParse(value, out int intValue))
                                            parsedInt = intValue;
                                        else
                                        {
                                        }
                                        property.SetValue(record, parsedInt);
                                    }
                                    else
                                    {
                                        // Convert value when it is not empty
                                        property.SetValue(record, Convert.ChangeType(value, property.PropertyType));
                                    }
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            throw;
                        }
                    }
                }

                records.Add(record);
            }

            return records;
        }
        catch (FileNotFoundException ex)
        {
            throw;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
        return null;
    }

    internal static object GetDefaultValue(Type type)
    {
        return type.IsValueType ? Activator.CreateInstance(type) : null;
    }

    // Extension method to check if a type is nullable
    internal static bool IsNullableType(this Type type)
    {
        return Nullable.GetUnderlyingType(type) != null;
    }
}