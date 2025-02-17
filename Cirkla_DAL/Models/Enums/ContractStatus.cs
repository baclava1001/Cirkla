namespace Cirkla_DAL.Models.Enums;

public enum ContractStatus
{
    None,
    Pending,
    Accepted,
    Denied,
    Cancelled,
    Active, // Changes automatically of no user cancels
    Late, // Changes automatically if Owner hasn't marked contract 'Completed'
    Completed,
    Problem,
    Archived // Changes automatically 48 hours after a contract was completed
}
