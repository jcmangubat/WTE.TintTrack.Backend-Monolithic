namespace WTE.TintTrack.Business.DataImporter.Models;

public class CSVContact
{
    [CsvColumn("code")]
    public long Code { get; set; }

    [CsvColumn("name")]
    public string Name { get; set; }

    [CsvColumn("firstname")]
    public string FirstName { get; set; }

    [CsvColumn("lastname")]
    public string LastName { get; set; }

    [CsvColumn("company")]
    public string Company { get; set; }

    [CsvColumn("phone")]
    public string Phone { get; set; }

    [CsvColumn("phone2")]
    public string Phone2 { get; set; }

    [CsvColumn("email")]
    public string Email { get; set; }

    [CsvColumn("address")]
    public string Address { get; set; }

    [CsvColumn("address2")]
    public string Address2 { get; set; }

    [CsvColumn("city")]
    public string City { get; set; }

    [CsvColumn("state")]
    public string State { get; set; }

    [CsvColumn("zipcode")]
    public string ZipCode { get; set; }

    [CsvColumn("vehicle")]
    public string Vehicle { get; set; }

    [CsvColumn("vehicle_year")]
    public int? VehicleYear { get; set; }

    [CsvColumn("vehicle_make")]
    public string VehicleMake { get; set; }

    [CsvColumn("vehicle_model")]
    public string VehicleModel { get; set; }

    [CsvColumn("type")]
    public string Type { get; set; }

    [CsvColumn("tags")]
    public string Tags { get; set; }

    [CsvColumn("campaign")]
    public string Campaign { get; set; }

    [CsvColumn("message")]
    public string Message { get; set; }

    [CsvColumn("custom_fields")]
    public string CustomFields { get; set; }

    [CsvColumn("scheduled_at")]
    public DateTime? ScheduledAt { get; set; }

    [CsvColumn("scheduled_in")]
    public string ScheduledIn { get; set; }

    [CsvColumn("first_note_at")]
    public DateTime? FirstNoteAt { get; set; }

    [CsvColumn("first_note_in")]
    public string FirstNoteIn { get; set; }

    [CsvColumn("created_at")]
    public DateTime CreatedAt { get; set; }

    [CsvColumn("created_by")]
    public string CreatedBy { get; set; }
}
