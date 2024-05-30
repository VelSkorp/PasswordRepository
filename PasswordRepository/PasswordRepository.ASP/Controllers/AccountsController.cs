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
			ViewBag.Message = "Password Repository description page.";
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