using Cirkla.ApiClient;

namespace Cirkla_Client.Constants;

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
                message = $"You have requested to borrow {contract.Item.Name} from {ContractStringBuilder.OwnerFullName(contract).Result}.";
            }
            else
            {
                message = $"{ContractStringBuilder.BorrowerFullName(contract).Result} has requested to borrow {contract.Item.Name}.";
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
                message = $"{ContractStringBuilder.OwnerFullName(contract).Result} has accepted your request to borrow {contract.Item.Name}.";
            }
            else
            {
                message = $"You have accepted {ContractStringBuilder.BorrowerFullName(contract).Result}'s request to borrow {contract.Item.Name}.";
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
                message = $"{ContractStringBuilder.OwnerFullName(contract).Result} has denied your request to borrow {contract.Item.Name}.";
            }
            else
            {
                message = $"You have denied {ContractStringBuilder.BorrowerFullName(contract).Result}'s request to borrow {contract.Item.Name}.";
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
            var changedById = lastCancelledChange.ChangedBy.Id;
            var isBorrower = userId == contract.Borrower.Id;
            var isOwner = userId == contract.Owner.Id;
            var isChangedByUser = userId == changedById;

            message = (isBorrower, isOwner, isChangedByUser) switch
            {
                (true, _, true) =>
                    $"You have cancelled your request to borrow {contract.Item.Name} from {ContractStringBuilder.OwnerFullName(contract)}.",
                (_, true, true) =>
                    $"{ContractStringBuilder.BorrowerFullName(contract).Result} has cancelled their request to borrow {contract.Item.Name}.",
                (true, _, false) =>
                    $"{ContractStringBuilder.OwnerFullName(contract).Result} has cancelled your request to borrow {contract.Item.Name}.",
                _ =>
                    $"You have cancelled {ContractStringBuilder.BorrowerFullName(contract).Result}'s request to borrow {contract.Item.Name}."
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
                message = $"Remember to pick up {contract.Item.Name} from {ContractStringBuilder.OwnerFullName(contract).Result} on {contract.StartTime.ToShortDateString()}";
            }
            else
            {
                message = $"{ContractStringBuilder.BorrowerFullName(contract).Result} will pick up {contract.Item.Name} on the {contract.StartTime.ToShortDateString()}.";
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
                message = $"You are currently borrowing {contract.Item.Name} from {ContractStringBuilder.OwnerFullName(contract).Result}";
            }
            else
            {
                message = $"{ContractStringBuilder.BorrowerFullName(contract).Result} is borrowing {contract.Item.Name} from you.";
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
                message = $"Remember to return {contract.Item.Name} to {ContractStringBuilder.OwnerFullName(contract).Result} on {contract.EndTime.ToShortDateString()}";
            }
            else
            {
                message = $"{ContractStringBuilder.BorrowerFullName(contract).Result} will return {contract.Item.Name} on the {contract.EndTime.ToShortDateString()}.";
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
                message = $"Warning! You are late to return {contract.Item.Name} to {ContractStringBuilder.OwnerFullName(contract).Result}.";
            }
            else
            {
                message = $"{ContractStringBuilder.BorrowerFullName(contract).Result} is late to return {contract.Item.Name}.";
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
                message = $"You've now returned {contract.Item.Name} to {ContractStringBuilder.OwnerFullName(contract).Result}.";
            }
            else
            {
                message = $"{ContractStringBuilder.BorrowerFullName(contract).Result} has now returned {contract.Item.Name}.";
            }
        }
        return message;
    }
}