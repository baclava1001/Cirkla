using Cirkla.ApiClient;

namespace Cirkla_Client.Services;

public static class ContractStringBuilder
{
    // Dates for contract status changes
    public static async Task<string> RequestDate(Contract contract)
    {
        return contract.StatusChanges.Where(s => s.To == ContractStatus.Pending).LastOrDefault().ChangedAt.ToString();
    }

    public static async Task<string> AcceptedDate(Contract contract)
    {
        return contract.StatusChanges.Where(s => s.To == ContractStatus.Accepted).LastOrDefault().ChangedAt.ToString();
    }

    public static async Task<string> DeniedDate(Contract contract)
    {
        return contract.StatusChanges.Where(s => s.To == ContractStatus.Denied).LastOrDefault().ChangedAt.ToString();
    }

    public static async Task<string> CancelledDate(Contract contract)
    {
        return contract.StatusChanges.Where(s => s.To == ContractStatus.Cancelled).LastOrDefault().ChangedAt.ToString();
    }

    public static async Task<string> ActiveDate(Contract contract)
    {
        return contract.StatusChanges.Where(s => s.To == ContractStatus.Active).LastOrDefault().ChangedAt.ToString();
    }

    public static async Task<string> LateDate(Contract contract)
    {
        return contract.StatusChanges.Where(s => s.To == ContractStatus.Late).LastOrDefault().ChangedAt.ToString();
    }

    public static async Task<string> CompletedDate(Contract contract)
    {
        return contract.StatusChanges.Where(s => s.To == ContractStatus.Completed).LastOrDefault().ChangedAt.ToString();
    }

    public static async Task<string> ProblemDate(Contract contract)
    {
        return contract.StatusChanges.Where(s => s.To == ContractStatus.Problem).LastOrDefault().ChangedAt.ToString();
    }

    public static async Task<string> ArchivedDate(Contract contract)
    {
        return contract.StatusChanges.Where(s => s.To == ContractStatus.Archived).LastOrDefault().ChangedAt.ToString();
    }


    public static async Task<string> BorrowerFullName(Contract contract)
    {
        return contract.Borrower.FirstName + " " + contract.Borrower.LastName;
    }

    public static async Task<string> OwnerFullName(Contract contract)
    {
        return contract.Owner.FirstName + " " + contract.Owner.LastName;
    }


    public static async Task<string> RequestCardContent(Contract contract, string userId)
    {
        string message = "";
        if (contract.StatusChanges.Where(s => s.To == ContractStatus.Pending).LastOrDefault() != null)
        {
            if (userId == contract.Borrower.Id)
            {
                message = $"You have requested to borrow {contract.Item.Name} from {OwnerFullName(contract).Result}.";
            }
            else
            {
                message = $"{BorrowerFullName(contract).Result} has requested to borrow {contract.Item.Name}.";
            }
        }
        return message;
    }


    public static async Task<string> AcceptedCardContent(Contract contract, string userId)
    {
        string message = "";
        if (contract.StatusChanges.Where(s => s.To == ContractStatus.Accepted).LastOrDefault() != null)
        {
            if (userId == contract.Borrower.Id)
            {
                message = $"{OwnerFullName(contract).Result} has accepted your request to borrow {contract.Item.Name}.";
            }
            else
            {
                message = $"You have accepted {BorrowerFullName(contract).Result}'s request to borrow {contract.Item.Name}.";
            }
        }
        return message;
    }

    public static async Task<string> DeniedCardContent(Contract contract, string userId)
    {
        string message = "";
        if (contract.StatusChanges.Where(s => s.To == ContractStatus.Denied).LastOrDefault() != null)
        {
            if (userId == contract.Borrower.Id)
            {
                message = $"{OwnerFullName(contract).Result} has denied your request to borrow {contract.Item.Name}.";
            }
            else
            {
                message = $"You have denied {BorrowerFullName(contract).Result}'s request to borrow {contract.Item.Name}.";
            }
        }
        return message;
    }


    public static async Task<string> CancelledCardContent(Contract contract, string userId)
    {
        string message = "";
        var lastCancelledChange = contract.StatusChanges.Where(s => s.To == ContractStatus.Cancelled).LastOrDefault();

        if (lastCancelledChange != null)
        {
            var changedById = lastCancelledChange?.ChangedBy?.Id;
            var isSystem = changedById == null;
            var isBorrower = userId == contract.Borrower.Id;
            var isOwner = userId == contract.Owner.Id;
            var isChangedByUser = userId == changedById;

            message = (isSystem, isBorrower, isOwner, isChangedByUser) switch
            {

                (false, true, false, true) =>
                    $"You have cancelled your request to borrow {contract.Item.Name} from {OwnerFullName(contract)}.",
                (false, false, true, false) =>
                    $"{contract.Item.Name} cancelled their request to borrow {contract.Item.Name}.",
                (false, false, true, true) =>
                    $"You have cancelled {BorrowerFullName(contract).Result}'s request to borrow {contract.Item.Name}.",
                (false, true, false, false) =>
                    $"{OwnerFullName(contract).Result} has cancelled your request to borrow {contract.Item.Name}.",
                (true, _, _, _) =>
                    "The item wasn't picked up, so the contract was cancelled automatically.",
                _ =>
                    $"You have cancelled {BorrowerFullName(contract).Result}'s request to borrow {contract.Item.Name}."
            };
        }
        return message;
    }


    public static async Task<string> PickupSoonCardContent(Contract contract, string userId)
    {
        string message = "";
        if (contract.StatusChanges.Where(s => s.To == ContractStatus.Accepted).LastOrDefault() != null &&
            contract.StartTime > DateTime.Now)
        {
            if (userId == contract.Borrower.Id)
            {
                message = $"Remember to pick up {contract.Item.Name} from {OwnerFullName(contract).Result} on {contract.StartTime.ToShortDateString()}";
            }
            else
            {
                message = $"{BorrowerFullName(contract).Result} will pick up {contract.Item.Name} on the {contract.StartTime.ToShortDateString()}.";
            }
        }
        return message;
    }


    public static async Task<string> ActiveCardContent(Contract contract, string userId)
    {
        string message = "";
        if (contract.StatusChanges.Where(s => s.To == ContractStatus.Active).LastOrDefault() != null && contract.StartTime < DateTime.Now)
        {
            if (userId == contract.Borrower.Id)
            {
                message = $"You are currently borrowing {contract.Item.Name} from {OwnerFullName(contract).Result}";
            }
            else
            {
                message = $"{BorrowerFullName(contract).Result} is borrowing {contract.Item.Name} from you.";
            }
        }
        return message;
    }


    public static async Task<string> ReturnReminderCardContent(Contract contract, string userId)
    {
        string message = "";
        if (contract.StatusChanges.Where(s => s.To == ContractStatus.Active).LastOrDefault() != null)
        {
            if (userId == contract.Borrower.Id)
            {
                message = $"Remember to return {contract.Item.Name} to {OwnerFullName(contract).Result} on {contract.EndTime.ToShortDateString()}";
            }
            else
            {
                message = $"{BorrowerFullName(contract).Result} will return {contract.Item.Name} on the {contract.EndTime.ToShortDateString()}.";
            }
        }
        return message;
    }


    public static async Task<string> LateCardContent(Contract contract, string userId)
    {
        string message = "";
        if (contract.StatusChanges.Where(s => s.To == ContractStatus.Late).LastOrDefault() != null && contract.EndTime > DateTime.Now)
        {
            if (userId == contract.Borrower.Id)
            {
                message = $"Warning! You are late to return {contract.Item.Name} to {OwnerFullName(contract).Result}.";
            }
            else
            {
                message = $"{BorrowerFullName(contract).Result} is late to return {contract.Item.Name}.";
            }
        }
        return message;
    }


    public static async Task<string> CompletedCardContent(Contract contract, string userId)
    {
        string message = "";
        if (contract.StatusChanges.Where(s => s.To == ContractStatus.Completed).LastOrDefault() != null)
        {
            if (userId == contract.Borrower.Id)
            {
                message = $"You've now returned {contract.Item.Name} to {OwnerFullName(contract).Result}.";
            }
            else
            {
                message = $"{BorrowerFullName(contract).Result} has now returned {contract.Item.Name}.";
            }
        }
        return message;
    }
}