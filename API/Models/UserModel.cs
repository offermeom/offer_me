using System.ComponentModel.DataAnnotations;
namespace API.Models;
public class User
{
    [Key]
    // ** Primary key
    public int ID { get; }
    public string? Name { get; set; }
    public string? Number { get; set; }
    public string? Mail { get; set; }
    public string? Password { get; set; }
    public string? GSTIN { get; set; }
    public DateTime Date { get; }
}