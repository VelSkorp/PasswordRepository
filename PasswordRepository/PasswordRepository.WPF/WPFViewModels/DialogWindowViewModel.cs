﻿using System.Windows;
using System.Windows.Controls;

namespace PasswordRepository.WPF
{
	/// <summary>
	/// The View Model for the custom flat window
	/// </summary>
	public class DialogWindowViewModel : WindowViewModel
	{
		#region Public Properties

		/// <summary>
		/// The title of this dialog window
		/// </summary>
		public string Title { get; set; }

		/// <summary>
		/// The content to host inside the dialog
		/// </summary>
		public Control Content { get; set; }

		#endregion

		#region Constructor

		/// <summary>
		/// Default constructor
		/// </summary>
		public DialogWindowViewModel(Window window) : base(window)
		{
			// Make minimum suze smaller
			WindowMinimumHeight = 100;
			WindowMinimumWidth = 250;

			// Maek title bar smaller
			TitleHeight = 30;
		}

		#endregion
	}
}