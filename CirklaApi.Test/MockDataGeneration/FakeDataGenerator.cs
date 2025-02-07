using Cirkla_DAL.Models;
using Bogus;

namespace Test.CirklaApi.MockDataGeneration;

public static class FakeDataGenerator
{
    // Instances of Faker hold the rules for generating a certain type of fake object
    private static readonly Faker<User> userFaker;
    private static readonly Faker<Item> itemFaker;
    private static readonly Faker<ItemPicture> itemPictureFaker;
    private static readonly Faker<Contract> requestFaker;
    private static readonly Faker<Contract> acceptedContractFaker;
    private static readonly Faker<Contract> deniedContractFaker;
    private static readonly Faker<Contract> completedContractFaker;
    private static readonly Faker<Contract> requestInvalidStartDateBeforeEndDateFaker;
    private static readonly Faker<Contract> requestInvalidStartDateBeforeCreatedDateFaker;

    // Rules defined in constructor
    static FakeDataGenerator()
    {
        // Defined randomizer seed to get random but still consistent results
        Randomizer.Seed = new Random(123);

        // Valid fakes for User, Item, ItemPicture and Contract
        userFaker = new Faker<User>()
            .RuleFor(u => u.Id, f => Guid.NewGuid().ToString())
            .RuleFor(u => u.FirstName, f => f.Name.FirstName())
            .RuleFor(u => u.LastName, f => f.Name.LastName())
            .RuleFor(u => u.Email, (f, u) => f.Internet.Email(u.FirstName, u.LastName))
            .RuleFor(u => u.UserName, (f, u) => f.Internet.Email(u.FirstName, u.LastName))
            .RuleFor(u => u.Address, f => f.Address.StreetAddress())
            .RuleFor(u => u.ZipCode, f => f.Address.ZipCode())
            .RuleFor(u => u.ProfilePictureURL, f => f.Internet.Avatar());

        itemPictureFaker = new Faker<ItemPicture>()
            .RuleFor(ip => ip.Id, f => f.Random.Int(1, 10000))
            .RuleFor(ip => ip.Url, f => f.Internet.Url())
            .RuleFor(ip => ip.ItemId, f => f.Random.Int(1, 10000));

        itemFaker = new Faker<Item>()
            .RuleFor(i => i.Id, f => f.Random.Int(1, 10000))
            .RuleFor(i => i.Name, f => f.Commerce.ProductName())
            .RuleFor(i => i.Category, f => f.Commerce.Categories(1)[0])
            .RuleFor(i => i.Model, f => f.Commerce.ProductAdjective())
            .RuleFor(i => i.Specifications, f => f.Commerce.ProductDescription())
            .RuleFor(i => i.Description, f => f.Lorem.Sentence())
            .RuleFor(i => i.OwnerId, f => Guid.NewGuid().ToString())
            .RuleFor(i => i.Pictures, f => itemPictureFaker.Generate(3));

        requestFaker = CreateBaseContractFaker()
            .RuleFor(c => c.StartTime, (f, c) => c.Created.AddDays(f.Random.Int(7, 21)))
            .RuleFor(c => c.EndTime, (f, c) => c.StartTime.AddDays(f.Random.Int(5, 15)));

        acceptedContractFaker = CreateBaseContractFaker()
            .RuleFor(c => c.StartTime, f => DateTime.UtcNow.AddDays(f.Random.Int(7, 21)))
            .RuleFor(c => c.EndTime, (f, c) => c.StartTime.AddDays(f.Random.Int(5, 15)))
            .RuleFor(c => c.AcceptedByOwner,
                (f, c) => f.Random.Bool() ? f.Date.Between(c.Created, c.StartTime) : (DateTime?)null);

        deniedContractFaker = CreateBaseContractFaker()
            .RuleFor(c => c.StartTime, f => DateTime.UtcNow.AddDays(f.Random.Int(7, 21)))
            .RuleFor(c => c.EndTime, (f, c) => c.StartTime.AddDays(f.Random.Int(5, 15)))
            .RuleFor(c => c.DeniedByOwner, (f, c) => f.Date.Between(c.Created, c.StartTime));

        completedContractFaker = CreateBaseContractFaker()
            .RuleFor(c => c.StartTime, f => DateTime.UtcNow.AddDays(-f.Random.Int(7, 21)))
            .RuleFor(c => c.EndTime, (f, c) => c.StartTime.AddDays(f.Random.Int(5, 15)) < DateTime.UtcNow
                ? c.StartTime.AddDays(f.Random.Int(5, 15))
                : DateTime.UtcNow.AddDays(-f.Random.Int(5, 15)));

        // Invalid Contract fakes
        requestInvalidStartDateBeforeEndDateFaker = CreateBaseContractFaker()
            .RuleFor(c => c.StartTime, f => DateTime.UtcNow.AddDays(f.Random.Int(7, 21)))
            .RuleFor(c => c.EndTime, (f, c) => c.StartTime.AddDays(-f.Random.Int(1, 5)));

        requestInvalidStartDateBeforeCreatedDateFaker = CreateBaseContractFaker()
            .RuleFor(c => c.Created, f => DateTime.UtcNow.AddDays(f.Random.Int(1, 5))) // Override Created to a future date
            .RuleFor(c => c.StartTime, (f, c) => c.Created.AddDays(-f.Random.Int(1, 5))); // Set StartTime to a date before Created

    }

    // Create a base contract faker, that can be reused for different contract states
    private static Faker<Contract> CreateBaseContractFaker()
    {
        return new Faker<Contract>()
            .RuleFor(c => c.Id, f => f.Random.Int(1, 10000))
            .RuleFor(c => c.Item, f => itemFaker.Generate())
            .RuleFor(c => c.Owner, f => userFaker.Generate())
            .RuleFor(c => c.Borrower, f => userFaker.Generate())
            .RuleFor(c => c.Created, f => DateTime.UtcNow)
            .RuleFor(c => c.AcceptedByOwner, (DateTime?)null)
            .RuleFor(c => c.DeniedByOwner, (DateTime?)null);
    }

    public enum ContractState
    {
        Request,
        Accepted,
        Denied,
        Completed,
        // Invalid types
        RequestInvalidStartDateBeforeEndDate,
        RequestInvalidStartDateBeforeCreatedDate
    }

    private static readonly Dictionary<ContractState, Func<Contract>> ContractGenerator = new()
    {
        { ContractState.Request, () => requestFaker.Generate() },
        { ContractState.Accepted, () => acceptedContractFaker.Generate() },
        { ContractState.Denied, () => deniedContractFaker.Generate() },
        { ContractState.Completed, () => completedContractFaker.Generate() },
        { ContractState.RequestInvalidStartDateBeforeEndDate, () => requestInvalidStartDateBeforeEndDateFaker.Generate() },
        { ContractState.RequestInvalidStartDateBeforeCreatedDate, () => requestInvalidStartDateBeforeCreatedDateFaker.Generate() }
    };

    private static readonly Dictionary<ContractState, Func<int, List<Contract>>> ContractListGenerator = new()
    {
        { ContractState.Request, count => requestFaker.Generate(count) },
        { ContractState.Accepted, count => acceptedContractFaker.Generate(count) },
        { ContractState.Denied, count => deniedContractFaker.Generate(count) },
        { ContractState.Completed, count => completedContractFaker.Generate(count) },
        { ContractState.RequestInvalidStartDateBeforeEndDate, count => requestInvalidStartDateBeforeEndDateFaker.Generate(count) },
        { ContractState.RequestInvalidStartDateBeforeCreatedDate, count => requestInvalidStartDateBeforeCreatedDateFaker.Generate(count) }
    };

    public static Contract GenerateContract(ContractState state)
    {
        return ContractGenerator[state]();
    }

    public static List<Contract> GenerateContracts(ContractState state, int count)
    {
        return ContractListGenerator[state](count);
    }

    public static Item GenerateItem()
    {
        return itemFaker.Generate();
    }

    public static List<Item> GenerateItems(int count)
    {
        return itemFaker.Generate(count);
    }

    public static User GenerateUser()
    {
        return userFaker.Generate();
    }

    public static List<User> GenerateUsers(int count)
    {
        return userFaker.Generate(count);
    }

    public static ItemPicture GenerateItemPicture()
    {
        return itemPictureFaker.Generate();
    }

    public static List<ItemPicture> GenerateItemPictures(int count)
    {
        return itemPictureFaker.Generate(count);
    }
}