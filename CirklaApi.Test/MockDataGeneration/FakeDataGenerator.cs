using Cirkla_DAL.Models;
using Bogus;

namespace Test.CirklaApi.MockDataGeneration;

public static class FakeDataGenerator
{
    // Instances of Faker that hold the rules for generating a certain type of fake object
    private static readonly Faker<User> userFaker;
    private static readonly Faker<Item> itemFaker;
    private static readonly Faker<ItemPicture> itemPictureFaker;
    private static readonly Faker<Contract> requestFaker;
    private static readonly Faker<Contract> acceptedContractFaker;
    private static readonly Faker<Contract> deniedContractFaker;
    private static readonly Faker<Contract> completedContractFaker;

    static FakeDataGenerator()
    {
        // Defined randomizer seed to get random but still consistent results
        Randomizer.Seed = new Random(123);

        userFaker = new Faker<User>()
            .RuleFor(u => u.Id, f => Guid.NewGuid().ToString())
            .RuleFor(u => u.FirstName, f => f.Name.FirstName())
            .RuleFor(u => u.LastName, f => f.Name.LastName())
            .RuleFor(u => u.Email, (f, u) => f.Internet.Email(u.FirstName, u.LastName))
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

        requestFaker = CreateContractFaker();
        
        acceptedContractFaker = CreateContractFaker()
            .RuleFor(c => c.AcceptedByOwner, (f, c) => f.Random.Bool() ? f.Date.Between(c.Created, c.StartTime) : (DateTime?)null);
        
        deniedContractFaker = CreateContractFaker()
            .RuleFor(c => c.DeniedByOwner, (f, c) => f.Date.Between(c.Created, c.StartTime));
        
        completedContractFaker = CreateContractFaker()
            .RuleFor(c => c.StartTime, f => DateTime.UtcNow.AddDays(-f.Random.Int(7, 21)))
            .RuleFor(c => c.EndTime, (f, c) => c.StartTime.AddDays(f.Random.Int(5, 15)) < DateTime.UtcNow
                ? c.StartTime.AddDays(f.Random.Int(5, 15))
                : DateTime.UtcNow.AddDays(-f.Random.Int(5, 15)));
    }

    // Create a base contract faker (request), that can be reused and modified for different contract states
    private static Faker<Contract> CreateContractFaker()
    {
        return new Faker<Contract>()
            .RuleFor(c => c.Id, f => f.Random.Int(1, 10000))
            .RuleFor(c => c.Item, f => itemFaker.Generate())
            .RuleFor(c => c.Owner, f => userFaker.Generate())
            .RuleFor(c => c.Borrower, f => userFaker.Generate())
            .RuleFor(c => c.StartTime, f => DateTime.UtcNow.AddDays(f.Random.Int(7, 21)))
            .RuleFor(c => c.Created, (f, c) => c.StartTime.AddDays(-f.Random.Int(1, 14)))
            .RuleFor(c => c.EndTime, (f, c) => c.StartTime.AddDays(f.Random.Int(5, 15)))
            .RuleFor(c => c.AcceptedByOwner, (DateTime?)null)
            .RuleFor(c => c.DeniedByOwner, (DateTime?)null);
    }

    public enum ContractType
    {
        Request,
        Accepted,
        Denied,
        Completed
    }

    private static readonly Dictionary<ContractType, Func<Contract>> ContractGenerator = new()
    {
        { ContractType.Request, () => requestFaker.Generate() },
        { ContractType.Accepted, () => acceptedContractFaker.Generate() },
        { ContractType.Denied, () => deniedContractFaker.Generate() },
        { ContractType.Completed, () => completedContractFaker.Generate() }
    };

    private static readonly Dictionary<ContractType, Func<int, List<Contract>>> ContractListGenerator = new()
    {
        { ContractType.Request, count => requestFaker.Generate(count) },
        { ContractType.Accepted, count => acceptedContractFaker.Generate(count) },
        { ContractType.Denied, count => deniedContractFaker.Generate(count) },
        { ContractType.Completed, count => completedContractFaker.Generate(count) }
    };

    public static Contract GenerateContract(ContractType type)
    {
        return ContractGenerator[type]();
    }

    public static List<Contract> GenerateContracts(int count, ContractType type)
    {
        return ContractListGenerator[type](count);
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
