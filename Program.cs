using System.CommandLine;

//Opciones de los comandos
var addDescriptionOption =new Option<string>(name: "--description", description: "Add a description")
{
    IsRequired = true,
};
var addAmountOption = new Option<decimal>(name:"--amount", description:"Amount of the expense")
{
    IsRequired = true
};
var deleteOption = new Option<int>(aliases: new[] { "--id", "-i" }, description: "ID of the expense to delete")
{
    IsRequired = true
};
var summaryOption = new Option<int?>(name: "--month", description: "Month to summarize (1-12)")
{
    IsRequired = false
};

//Validaciones personalizadas
// Agregamos un validador personalizado
summaryOption.AddValidator(result =>
{
    var token = result.Tokens.FirstOrDefault()?.Value;

    if (token == null)
    {
        // No pasa nada, porque --month es opcional
        return;
    }
    if (!int.TryParse(token, out int month))
    {
        result.ErrorMessage = "The value for --month must be a number.";
    }
    else if (month < 1 || month > 12)
    {
        result.ErrorMessage = "Month must be between 1 and 12.";
    }
});


//Comandos del CLI
var addCommand = new Command("add", "Add an expense")
{
    addDescriptionOption,
    addAmountOption
};
var listCommand = new Command("list", "List all expenses");
var summaryCommand = new Command("summary", "Show a summary of expenses")
{
    summaryOption
};
var deleteCommand = new Command("delete", "Delete an expense")
{
    deleteOption
};


addCommand.SetHandler((description, amount) =>
{
    Console.WriteLine($"Adding expense with description: {description} and amount: {amount}");
    Console.WriteLine($"Expense added successfully (ID: 1)");
}, addDescriptionOption, addAmountOption);

listCommand.SetHandler(() =>
{
    Console.WriteLine("Listing all expenses...");
    Console.WriteLine("1. Description: Groceries, Amount: 50.00");
    Console.WriteLine("2. Description: Rent, Amount: 1200.00");
});

summaryCommand.SetHandler((month) =>
{
    if (month > 0) {
        Console.WriteLine($"Showing summary of expenses for month: {month}");
    }
    else
    {
        Console.WriteLine("Showing summary of expenses...");
        Console.WriteLine("Total Expenses: 1250.00");
    }

}, summaryOption);
deleteCommand.SetHandler((id) =>
{
    Console.WriteLine($"Deleting expense with ID: {id}");
    Console.WriteLine($"Expense with ID {id} deleted successfully.");
}, deleteOption);


//Comandos agregados al rootCommand
var rootCommand = new RootCommand("Expense Tracker CLI")
{
    addCommand,
    listCommand,
    summaryCommand,
    deleteCommand
};

await rootCommand.InvokeAsync(args);