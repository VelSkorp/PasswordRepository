using PasswordRepository.Core;
using System;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace PasswordRepository.ASP
{
	public class AccountsController : Controller
	{
		public async Task<ActionResult> IndexAsync()
		{
			return View("Index", await CoreDI.DataStore.GetAccountsDetailsAsync());
		}

		public ActionResult About()
		{
			ViewBag.Message = "It consists of two user interfaces: an ASP.NET MVC website and a WPF Windows application. Both interfaces interact with a shared C# codebase and use MS SQL Server with Entity Framework 6 (EF6) for data persistence. The application supports the secure hashing of passwords and provides CRUD operations for password management.";
			return View();
		}

		public ActionResult Contact()
		{
			ViewBag.Message = "Vlad Kontsevich";
			return View();
		}

		public ActionResult Create()
		{
			return View("Details");
		}

		public ActionResult Edit(int id, string accountName, DateTime lastUpdated, string comments, string password)
		{
			var account = new Account
			{
				Id = id,
				AccountName = accountName,
				LastUpdated = lastUpdated,
				Comments = comments,
				Password = EncryptionHelper.Decrypt(password)
			};
			return View("Details", account);
		}

		[HttpPost]
		public async Task<ActionResult> SaveAsync(Account account)
		{
			account.Password = EncryptionHelper.Encrypt(account.Password);
			account.LastUpdated = DateTime.Now;

			var isSaved = await CoreDI.DataStore.SaveAccountAsync(account);
			return Json(new { success = isSaved });
		}

		[HttpPost]
		public async Task<ActionResult> DeleteAsync(Account account)
		{
			var isDeleted = await CoreDI.DataStore.DeleteAccountAsync(account);
			return Json(new { success = isDeleted });
		}
	}
}