using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;
using AvaloniaMVVMApplication.Models;
using ReactiveUI;

namespace AvaloniaMVVMApplication.ViewModels
{
    public class AddItemViewModel : ViewModelBase
    {
        public AddItemViewModel()
        {
            //https://avaloniaui.net/docs/tutorial/adding-new-items-2, creates observable stream of bool values
            var okEnabled = this.WhenAnyValue(
                x => x.Description,
                x => !string.IsNullOrWhiteSpace(x));

            Ok = ReactiveCommand.Create(
                () => new TodoItem {Description = Description},
                okEnabled);
            Cancel = ReactiveCommand.Create(() => { });
        }
        string description;

        public string Description
        {
            get => description;
            set => this.RaiseAndSetIfChanged(ref description, value);
        }

        public ReactiveCommand<Unit, TodoItem> Ok { get; }
        public ReactiveCommand<Unit, Unit> Cancel { get; }

    }
}
