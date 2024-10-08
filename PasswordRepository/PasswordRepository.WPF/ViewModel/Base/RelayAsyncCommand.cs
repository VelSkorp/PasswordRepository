﻿using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PasswordRepository.WPF
{
	/// <summary>
	/// A basic async command that runs an Action
	/// </summary>
	public class RelayAsyncCommand : ICommand
	{
		#region Private Members

		/// <summary>
		/// The action to run
		/// </summary>
		private readonly Func<Task> mFunc;

		#endregion

		#region Public Events

		/// <summary>
		/// The event thats fired when the <see cref="CanExecute(object)"/> value has changed
		/// </summary>
		public event EventHandler CanExecuteChanged = (sender, e) => { };

		#endregion

		#region Constructor

		/// <summary>
		/// Default constructor
		/// </summary>
		public RelayAsyncCommand(Func<Task> action)
		{
			mFunc = action;
		}

		#endregion

		#region Command Methods

		/// <summary>
		/// A relay command can always execute
		/// </summary>
		/// <param name="parameter"></param>
		/// <returns></returns>
		public bool CanExecute(object parameter)
		{
			return true;
		}

		/// <summary>
		/// Executes the commands Func
		/// </summary>
		/// <param name="parameter"></param>
		public async void Execute(object parameter)
		{
			await Task.Run(async () => await mFunc());
		}

		#endregion
	}
}