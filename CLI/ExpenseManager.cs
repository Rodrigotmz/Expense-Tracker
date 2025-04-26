using expense_tracker.Model;
using Spectre.Console;

namespace expense_tracker.CLI
{
    public class ExpenseManager
    {
        private JsonData<Item> json;
        private int nextId = 1;
        public ExpenseManager()
        {
            json = new JsonData<Item>();
        }
        public async Task<int> AddExpense(string description, decimal amount)
        {
            var dataList = await json.GetJsonData();
            if (dataList.Count > 0)
            {
                nextId = dataList.Max(p => p.id) + 1;
            }
            dataList.Add(new Item(nextId, description, amount));
            await json.SaveJasonData(dataList);
            return nextId;
        }
        public async Task DeleteExpense(int id)
        {
            var dataList = await json.GetJsonData();
            var expense = dataList.FirstOrDefault(x => x.id == id);
            if (expense == null)
            {
                AnsiConsole.MarkupLine($"[red]Expense not found (ID: {id})[/]");
            }
            dataList.Remove(expense);
            await json.SaveJasonData(dataList);
            AnsiConsole.MarkupLine("[red]Expense deleted successfully[/]");
        }
        public async Task Summary(int? month = 0)
        {
            var dataList = await json.GetJsonData();
            if (month == 0)
            {
                var sum = dataList.Sum(p => p.amount);
                AnsiConsole.MarkupLine($"[green]Total expenses: {sum}[/]");
            }
            else
            {
                var monthConv = GetMonthName(month);
                var filterMonth = dataList.Where(p => GetNameMonth(p.date) == monthConv).ToList();
                var sumMonth = filterMonth.Sum(p => p.amount);
                AnsiConsole.MarkupLine($"[green]Total expenses for {monthConv}: ${sumMonth}[/]");
            }
            
        }

        public async Task List()
        {
            var dataList = await json.GetJsonData();
            if (dataList.Count == 0)
            {
                AnsiConsole.MarkupLine("[red]No expenses found[/]");
                return;
            }
            var table = new Table();
            table.AddColumn("[#fca311]ID[/]");
            table.AddColumn("[#fca311]Date[/]");
            table.AddColumn("[#fca311]Description[/]");
            table.AddColumn("[#fca311]Amount[/]");
            foreach (var item in dataList)
            {
                table.AddRow($"[green]{item.id.ToString()}[/]", $"[green]{item.date?.ToString("yyy-MM-dd")}[/]", $"[green]{item.description}[/]", $"[green]${item.amount}[/]");
            }
            AnsiConsole.Write(table);
        }


        private string GetNameMonth(DateTime? date) => date?.ToString("MMMM");


        private string GetMonthName(int? month)
        {
            DateTime date = new DateTime(DateTime.Now.Year, (int)month, 1);
            return date.ToString("MMMM");
        }
    }
}
