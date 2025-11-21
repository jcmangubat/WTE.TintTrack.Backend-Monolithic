using CsvHelper.Configuration;
using CsvHelper;
using System.Globalization;
using System.Reflection;

namespace WTE.TintTrack.Business.DataImporter;

/// <summary>
/// Utility class for loading CSV files into strongly-typed models
/// </summary>
public static class CSVDataLoader
{
    /// <summary>
    /// Loads CSV data into a list of strongly-typed models
    /// </summary>
    /// <typeparam name="TCSVDataModel">The model type to deserialize CSV rows into</typeparam>
    /// <param name="csvFile">Path to the CSV file</param>
    /// <returns>List of deserialized models</returns>
    /// <exception cref="FileNotFoundException">Thrown when CSV file is not found</exception>
    /// <exception cref="Exception">Thrown when CSV parsing fails</exception>
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
                            // Log the error with context before re-throwing
                            // Note: Using Console.WriteLine as this is a utility class without logging dependencies
                            Console.Error.WriteLine($"Error processing property '{property.Name}': {ex.Message}");
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
            // Re-throw file not found exceptions as-is
            throw;
        }
        catch (Exception ex)
        {
            // Log the error with full details before re-throwing
            // Note: Using Console.Error for error output as this is a utility class without logging dependencies
            Console.Error.WriteLine($"An error occurred while loading CSV file '{csvFile}': {ex.Message}");
            Console.Error.WriteLine($"Stack trace: {ex.StackTrace}");
            // Re-throw to make error handling explicit
            throw;
        }
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