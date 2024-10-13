using ASI.Basecode.Data.Models;
using ASI.Basecode.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace ASI.Basecode.WebApp.Controllers
{
    public class ExpenseController : Controller
    {
        private readonly IExpenseService _expenseService;
        public ExpenseController(IExpenseService _expenseService)
        {
            this._expenseService = _expenseService;
        }
        public IActionResult Index()
        {
            List<Expense> expenses = _expenseService.GetExpenses();
            expenses = expenses ?? new List<Expense>();
            return View(expenses);
        }
        public IActionResult Create()
        {

            return View();
        }


        [HttpPost]
        public IActionResult Create(Expense expense)
        {
            _expenseService.AddExpense(expense);
            return RedirectToAction("Index", "Expense");
        }

        [HttpPost]
        public IActionResult Edit(Expense expense)
        {

            if (expense != null)
            {
                _expenseService.UpdateExpense(expense);
            }
            return RedirectToAction("Index", "Expense");
        }
        public IActionResult Edit(string id)
        {
            var expense = _expenseService.GetExpenses().FirstOrDefault(b => b.ID == id);
            return View(expense);
        }
        public IActionResult Delete(string id)
        {
            var expense = _expenseService.GetExpenses().FirstOrDefault(b => b.ID == id);
            if (expense != null)
                _expenseService.DeleteExpense(expense);
            return RedirectToAction("Index", "Expense");
        }
    }
}
