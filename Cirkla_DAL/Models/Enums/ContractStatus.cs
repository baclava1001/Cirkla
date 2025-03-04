namespace Cirkla_DAL.Models.Enums;

public enum ContractStatus
{
    None,
    Pending, // TODO: Owner should be able to suggest a different time for a pending contract (Status: "Pending + date" => "Pending + new date")
    Accepted,
    Denied,
    Cancelled,
    Active, // Borrower should mark the contract as 'Active' when they have received the item
    Late, // Changes automatically if Owner hasn't marked contract 'Completed'
    Completed,
    Problem,
    Archived // Changes automatically 48 hours after a contract was completed
}