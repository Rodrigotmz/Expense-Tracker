using expense_tracker.CLI;
using System.CommandLine;
using Spectre.Console;

var manager = new ExpenseManager();
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


addCommand.SetHandler(async (description, amount) =>
{
    int id = await manager.AddExpense(description, amount);
    AnsiConsole.MarkupLine($"[green]Expense added successfully (ID: {id})[/]");
}, addDescriptionOption, addAmountOption);

listCommand.SetHandler(async () =>
{
    await manager.List();
});

summaryCommand.SetHandler(async (month) =>
{
    
    if (month > 0) {
        await manager.Summary(month);
    }
    else
    {
        await manager.Summary();
    }

}, summaryOption);
deleteCommand.SetHandler(async (id) =>
{
    await manager.DeleteExpense(id);
}, deleteOption);


//Comandos agregados al rootCommand
var rootCommand = new RootCommand("Expense Tracker CLI")
{
    addCommand,
    listCommand,
    summaryCommand,
    deleteCommand
};
//string[] arg = { "summary", "--month", "3"};
//string[] arg = { "list"};
await rootCommand.InvokeAsync(args);